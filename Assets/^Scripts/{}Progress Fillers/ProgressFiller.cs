using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ProgressFiller : MonoBehaviour
{
	[SerializeField] private UnityEvent<float> _onFillChanged;
	public UnityEvent<float> _OnFillChanged => this._onFillChanged;

	protected abstract void OnFillChanged(float fill);

	[Range(0f, 1f)]
	[SerializeField] protected float fill = 0.75f;
	public float Fill
	{
		get => this.fill;
		set
		{
			this.fill = Mathf.Clamp01(value);
			this._onFillChanged.Invoke(this.fill);
		}
	}

	protected virtual void Awake()
	{
		this._onFillChanged.AddListener(this.OnFillChanged);
	}

#if UNITY_EDITOR
	protected virtual void OnValidate()
	{
		this._onFillChanged.Invoke(this.fill);
		this.OnFillChanged(this.fill);
	}

	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(ProgressFiller))]
	[CanEditMultipleObjects]
	public class ProgressFillerEditor : Editor
	{
#pragma warning disable 0219, 414
		private ProgressFiller _sProgressFiller;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sProgressFiller = this.target as ProgressFiller;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}