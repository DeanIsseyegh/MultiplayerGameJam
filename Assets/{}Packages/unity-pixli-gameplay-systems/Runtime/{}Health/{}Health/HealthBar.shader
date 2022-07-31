Shader "UI/HealthBar"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Fill("Fill", float) = 0
		_ScaleX("Scale X", float) = 1.0
		_ScaleY("Scale Y", float) = 1.0
	}
	SubShader
	{
		Tags { "Queue" = "Overlay" }
		LOD 100

		Pass
		{
			ZTest Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#pragma multi_compile_instancing


			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				// If you need instance data in the fragment shader, uncomment next line
				//UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			uniform float _ScaleX;
			uniform float _ScaleY;

			UNITY_INSTANCING_BUFFER_START(Props)
			UNITY_DEFINE_INSTANCED_PROP(float, _Fill)
			UNITY_INSTANCING_BUFFER_END(Props)

			vertexOutput vert(appdata input) {
				vertexOutput o;
				UNITY_SETUP_INSTANCE_ID(input);
				// If you need instance data in the fragment shader, uncomment next line
				// UNITY_TRANSFER_INSTANCE_ID(input, o);

				float fill = UNITY_ACCESS_INSTANCED_PROP(Props, _Fill);

				o.pos = UnityObjectToClipPos(input.vertex);

				// generate UVs from fill level (assumed texture is clamped)
				o.uv = input.uv;
				o.uv.x += 0.5 - fill;

				// billboard mesh towards camera
				o.pos = mul(UNITY_MATRIX_P,
					mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
					+ float4(input.vertex.x, input.vertex.y, 0.0, 0.0)
					* float4(_ScaleX, _ScaleY, 1.0, 1.0));

				return o;
			}

			fixed4 frag(vertexOutput i) : SV_Target {

				// Could access instanced data here too like:
				// UNITY_SETUP_INSTANCE_ID(i);
				// UNITY_ACCESS_INSTANCED_PROP(Props, _Foo);
				// But, remember to uncomment lines flagged above

				return tex2D(_MainTex, i.uv);
			}
			ENDCG
		}
	}
}