using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressFillerMaterial : ProgressFiller
{
	[SerializeField] private MeshRenderer _meshRenderer;

	[HideInInspector]
	[SerializeField] private MaterialPropertyBlock _materialPropertyBlock;

	protected override void OnFillChanged(float fill)
	{
#if UNITY_EDITOR
		if (this._materialPropertyBlock == null)
			this._materialPropertyBlock = new MaterialPropertyBlock();
#endif
		this._meshRenderer.GetPropertyBlock(this._materialPropertyBlock);

		this._materialPropertyBlock.SetFloat("_Fill", fill);

		this._meshRenderer.SetPropertyBlock(this._materialPropertyBlock);
	}

	protected override void Awake()
	{
		base.Awake();

		this._materialPropertyBlock = new MaterialPropertyBlock();
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._meshRenderer = this.GetComponent<MeshRenderer>();

		//this._materialPropertyBlock = new MaterialPropertyBlock();
	}

	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(ProgressFillerMaterial))]
	[CanEditMultipleObjects]
	public class ProgressFillerMaterialEditor : Editor
	{
#pragma warning disable 0219, 414
		private ProgressFillerMaterial _sProgressFillerMaterial;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sProgressFillerMaterial = this.target as ProgressFillerMaterial;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}