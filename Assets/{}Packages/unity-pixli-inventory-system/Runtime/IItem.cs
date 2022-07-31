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
	public interface IItem
	{
		void AddToInventory(Inventory inventory);
		void AddToInventory(IInventoryHolder inventoryHolder);
		void AddToInventory(Collider collider);
		void AddToInventory(Collider2D collider2D);

#if UNITY_EDITOR
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(IItem))]
	[CanEditMultipleObjects]
	public class IItemEditor : Editor
	{
#pragma warning disable 0219, 414
		private IItem _sIItem;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sIItem = this.target as IItem;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}