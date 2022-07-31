/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public class BuildingGameAsset : MonoBehaviour
	{
		[SerializeField] private float _height = 2f;
		public float _Height => this._height;

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(BuildingGameAsset))]
	[CanEditMultipleObjects]
	public class BuildingGameAssetEditor : Editor
	{
#pragma warning disable 0219
		private BuildingGameAsset _sBuildingGameAsset;
#pragma warning restore 0219

		private void OnEnable()
		{
			this._sBuildingGameAsset = target as BuildingGameAsset;
		}

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
		}
	}
#endif
}