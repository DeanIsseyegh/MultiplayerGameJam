using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class ProgressFillerSlider : ProgressFiller
{
	[SerializeField] private Slider _slider;

	protected override void OnFillChanged(float fill)
	{
		this._slider.normalizedValue = fill;
	}

#if UNITY_EDITOR
	private float previousFill;

	protected override void OnValidate()
	{
		if (this._slider != null && Mathf.Abs(this.previousFill - this.fill) > Mathf.Epsilon)
		{
			this._slider.value = this.fill;

			this.previousFill = this.fill;
		}
	}

	private void Reset()
	{
		this._slider = this.GetComponentInChildren<Slider>();
	}

	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(ProgressFillerSlider))]
	[CanEditMultipleObjects]
	public class ProgressFillerSliderEditor : Editor
	{
#pragma warning disable 0219, 414
		private ProgressFillerSlider _sProgressFillerSlider;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sProgressFillerSlider = this.target as ProgressFillerSlider;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}