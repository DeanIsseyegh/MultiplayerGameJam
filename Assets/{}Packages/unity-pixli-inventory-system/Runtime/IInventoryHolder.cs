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
	public interface IInventoryHolder
	{
		Inventory _Inventory { get; }

#if UNITY_EDITOR
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(IInventoryHolder))]
	[CanEditMultipleObjects]
	public class IInventoryHolderEditor : Editor
	{
#pragma warning disable 0219, 414
		private IInventoryHolder _sIInventoryHolder;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sIInventoryHolder = this.target as IInventoryHolder;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}