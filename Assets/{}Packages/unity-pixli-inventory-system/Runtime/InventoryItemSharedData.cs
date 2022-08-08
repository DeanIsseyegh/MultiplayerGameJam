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
	[System.Serializable]
	[CreateAssetMenu(fileName = "[Inventory Item Shared Data]", menuName = "[Inventory System]/[Inventory Item Shared Data]", order = 1)]
	public class InventoryItemSharedData : ScriptableObject
	{
		public int Id { get; set; }

		[SerializeField] private string _name;
		public string _Name => this._name;

		[SerializeField] private bool _stackable;
		public bool _Stackable => this._stackable;

		[SerializeField] private Sprite _icon;
		public Sprite _Icon => this._icon;

		[TextArea]
		[SerializeField] private string _description;
		public string _Description => this._description;
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(InventoryItemSharedData))]
	[CanEditMultipleObjects]
	public class InventoryItemSharedDataEditor : Editor
	{
#pragma warning disable 0219, 414
		private InventoryItemSharedData _sInventoryItemSharedData;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sInventoryItemSharedData = this.target as InventoryItemSharedData;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}