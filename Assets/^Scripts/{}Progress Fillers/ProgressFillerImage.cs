using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ProgressFillerImage : ProgressFiller
{
	[SerializeField] private Image _image;

	protected override void OnFillChanged(float fill)
	{
		this._image.fillAmount = fill;
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._image = this.GetComponent<Image>();
		if (this._image != null)
		{
			this._image.type = Image.Type.Filled;
		}
	}

	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(ProgressFillerImage))]
	[CanEditMultipleObjects]
	public class ProgressFillerImageEditor : Editor
	{
#pragma warning disable 0219, 414
		private ProgressFillerImage _sProgressFillerImage;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sProgressFillerImage = this.target as ProgressFillerImage;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}