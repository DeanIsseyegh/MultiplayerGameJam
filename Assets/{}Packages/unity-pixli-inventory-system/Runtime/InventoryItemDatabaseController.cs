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
	public class InventoryItemDatabaseController : MonoBehaviourSingleton<InventoryItemDatabaseController>
	{
		private const int DEFAULT_FREE_ITEM_ID = -1;
		private static int s_freeItemId = DEFAULT_FREE_ITEM_ID;

		private Dictionary<int, InventoryItemSharedData> _itemId_inventoryItemSharedDataRelation;

		[SerializeField] private InventoryItemSharedData[] _inventoryItemSharedData;

		public InventoryItemSharedData FetchSharedData(int itemId)
		{
			return this._itemId_inventoryItemSharedDataRelation[itemId];
		}

		private void InitializeDatabase()
		{
			InventoryItemDatabaseController.s_freeItemId = DEFAULT_FREE_ITEM_ID;

			this._itemId_inventoryItemSharedDataRelation = new Dictionary<int, InventoryItemSharedData>(this._inventoryItemSharedData.Length);

			for (int i = 0; i < this._inventoryItemSharedData.Length; i++)
			{
				// Index ItemSharedData
				this._inventoryItemSharedData[i].Id = ++InventoryItemDatabaseController.s_freeItemId;

				this._itemId_inventoryItemSharedDataRelation.Add(this._inventoryItemSharedData[i].Id, this._inventoryItemSharedData[i]);
			}
		}

		protected override void Awake()
		{
			base.Awake();

			this.InitializeDatabase();
		}

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
	[CustomEditor(typeof(InventoryItemDatabaseController))]
	[CanEditMultipleObjects]
	public class InventoryItemDatabaseControllerEditor : Editor
	{
#pragma warning disable 0219, 414
		private InventoryItemDatabaseController _sInventoryItemDatabaseController;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sInventoryItemDatabaseController = this.target as InventoryItemDatabaseController;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}