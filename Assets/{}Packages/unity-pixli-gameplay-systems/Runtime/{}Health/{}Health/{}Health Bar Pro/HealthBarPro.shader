Shader "Example/HealthBarPro"
{
	Properties
	{
		_Fill("Fill", Range(0,1)) = 0.0

		_ScaleX("Scale X", float) = 1.0
		_ScaleY("Scale Y", float) = 1.0

		_Offset("Offset", Vector) = (0.0, 0.0, 0.0, 0.0)

		[Space(10)]
		[KeywordEnum(Circle,Box, Rhombus)] _Shape("Shape",int) = 0
		_lowLifeThreshold("Low Life Threshold", Range(0,1)) = 0.2

		[Space(10)]
		_waveAmp("Fill Wave Amplitude", float) = 0.01
		_waveFreq("Fill Wave Frequency", float) = 8

		[Space(10)]
		_startColor("Fill Start Color", Color) = (1,1,1,1)
		_endColor("Fill End Color", Color) = (1,1,1,1)
		_startThreshold("Start Threshold", Range(0,1)) = 0
		_endThreshold("End Threshold", Range(0,1)) = 1

		[Space(10)]
		_backgroundColor("Background Color", Color) = (0,0,0,0.25)
		_borderWidth("Border Width", Range(0,0.4)) = 0
		_borderColor("Border Color", Color) = (0.1,0.1,0.1,1)
	}

		SubShader
		{
			Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

			Blend SrcAlpha OneMinusSrcAlpha

			Pass
			{
				ZTest Off

				HLSLPROGRAM

				#pragma multi_compile _SHAPE_CIRCLE _SHAPE_BOX _SHAPE_RHOMBUS

				#pragma vertex vert
				#pragma fragment frag

				//#pragma multi_compile_instancing

				#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
				//#include "UnityCG.cginc"
				#include "ShaderFunctions.hlsl"

				// write properties here for the shader to be SRP Batcher compatible 
				CBUFFER_START(UnityPerMaterial)
					float _Fill;
					float4 _backgroundColor, _borderColor, _startColor, _endColor;
					float3 _Offset;
					float _lowLifeThreshold, _startThreshold, _endThreshold, _borderWidth;
					float _waveAmp, _waveFreq;
				CBUFFER_END

				//UNITY_INSTANCING_BUFFER_START(Props)
				//UNITY_DEFINE_INSTANCED_PROP(float, _Fill)
				//UNITY_INSTANCING_BUFFER_END(Props)

				uniform float _ScaleX;
				uniform float _ScaleY;

				struct Attributes
				{
					float4 positionOS : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct Varyings
				{
					float4 positionHCS : SV_POSITION;
					float4 positionOS : TEXCOORD1;
					float2 uv : TEXCOORD0;
					//UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				//This is a replacement for the old 'UnityObjectToClipPos()'
				float4 ObjectToClipPos(float3 pos)
				{
					return mul(UNITY_MATRIX_VP, mul(UNITY_MATRIX_M, float4 (pos, 1)));
				}

				Varyings vert(Attributes IN)
				{
					Varyings OUT;

					OUT.positionHCS = ObjectToClipPos(IN.positionOS);// mul(UNITY_MATRIX_MVP, IN.positionOS); // TransformObjectToHClip(IN.positionOS.xyz);
					OUT.uv = IN.uv;
					OUT.positionOS = IN.positionOS;

					// Billboard.
					OUT.positionHCS = mul(UNITY_MATRIX_P,
						mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) + float4(_Offset.x, _Offset.y, _Offset.z, 0.0) //float4(_Offset.x, _Offset.y, _Offset.z, 1.0)
						+ float4(IN.positionOS.x, IN.positionOS.y, 0.0, 0.0)
						* float4(_ScaleX, _ScaleY, 1.0, 1.0));

					return OUT;
				}

				float4 frag(Varyings IN) : SV_Target
				{
					//UNITY_SETUP_INSTANCE_ID(IN);

					float fill = _Fill;// float fill = UNITY_ACCESS_INSTANCED_PROP(Props, _Fill);

					float3 _objectScale = GetObjectScale(); // TODO: Pass as uniform

					// leave some margin space
					float minScale = min(_objectScale.x, _objectScale.y);
					float margin = minScale * 0.1;

					// we 'elongate' instead of 'scaling' SDF to keep euclidean distance (so we can apply antialias easily)
					float3 _shapeElongation = (_objectScale - minScale) / 2;

					// Apply elongation operation to fragment position
					float3 p = (IN.positionOS) * _objectScale;
					float3 q = Elongate(p, _shapeElongation);

					// CONTAINER
					float halfSize = minScale / 2 - margin;

					#if _SHAPE_CIRCLE
					float healthBarSDF = CircleSDF(q, halfSize);
					#endif

					#if _SHAPE_BOX
					float healthBarSDF = BoxSDF(q, halfSize);
					#endif

					#if _SHAPE_RHOMBUS
					float healthBarSDF = RhombusSDF(q, float2(halfSize, halfSize));
					#endif

					float healthBarMask = GetSmoothMask(healthBarSDF);

					// LIQUID/FILLER
					// min(sin) term is used to decrease effect of wave near 0 and 1.
					float waveOffset = _waveAmp * cos(_waveFreq*(IN.uv.x + _Time.y*0.5f)) * min(2 * sin(PI * fill), 1);
					float marginNormalizedY = margin / _objectScale.y;
					float borderNormalizedY = _borderWidth / _objectScale.y;
					float fillOffset = marginNormalizedY + borderNormalizedY;

					float fillSDF = IN.uv.y - (lerp(fillOffset - 0.01f, 1 - fillOffset, fill) + waveOffset);
					float fillMask = GetSmoothMask(fillSDF);

					float t = clamp(InverseLerp(_startThreshold, _endThreshold, fill), 0, 1);
					float4 fillColor = lerp(_startColor, _endColor, t);

					// BORDER 
					float borderSDF = healthBarSDF + _borderWidth;
					float borderMask = 1 - GetSmoothMask(borderSDF);

					// Get final color by combining masks
					float4 outColor = healthBarMask * (fillMask * (1 - borderMask) * fillColor + (1 - fillMask) * (1 - borderMask) * _backgroundColor + borderMask * _borderColor);

					// Highlight center
					outColor *= float4(2 - healthBarSDF / (minScale / 2).xxx, 1);

					// Add flash effect on low life
					if (fill < _lowLifeThreshold)
					{
						float flash = 0.05 * cos(6 * _Time.y);
						outColor.xyz += flash;
					}
					return outColor;
				}
				ENDHLSL
			}
		}
}