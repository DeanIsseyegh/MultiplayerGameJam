///* Created by Max.K.Kimo */

//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace PixLi
//{
//	public class InventoryUserInterfaceView : UserInterfaceView<InventoryUserInterfaceView.InventoryUserInterfaceViewDisplayData, InventoryUserInterfaceView.InventoryUserInterfaceViewOutput>
//	{
//		[System.Serializable]
//		public class InventoryUserInterfaceViewDisplayData : UserInterfaceViewDisplayData
//		{
//		}

//		[System.Serializable]
//		public class InventoryUserInterfaceViewOutput : UserInterfaceViewOutput
//		{
//		}

//		public override void Display(InventoryUserInterfaceViewDisplayData displayData)
//		{
//			throw new System.NotImplementedException();
//		}

//		public enum ClearMode { Partial, Full, Destruction }

//		[SerializeField] private InventoryItemTooltipUserInterfaceView _itemTooltipView;
//		public InventoryItemTooltipUserInterfaceView _ItemTooltipView { get { return this._itemTooltipView; } }

//		[SerializeField] private InventoryItemSlotUserInterfaceView _itemSlotViewPrefab;
//		public InventoryItemSlotUserInterfaceView _ItemSlotViewPrefab { get { return this._itemSlotViewPrefab; } }

//		[SerializeField] private Transform _itemSlotViewsContainer;
//		public Transform _ItemSlotViewsContainer { get { return this._itemSlotViewsContainer; } }

//		private Inventory _displayedInventory;

//		public void Display(Inventory inventory)
//		{
//			this._displayedInventory = inventory;

//			this.InitializeLayout();
//			this.UpdateLayout();
//		}

//		private List<InventoryItemSlotUserInterfaceView> _itemSlotViews = new List<InventoryItemSlotUserInterfaceView>(16);

//		public void InitializeLayout()
//		{
//			if (this._displayedInventory == null)
//				return;

//			this.Clear(ClearMode.Full);

//			for (int i = 0; i < this._displayedInventory._Capacity; i++)
//			{
//				InventoryItemSlotUserInterfaceView itemSlotView = i < this._itemSlotViews.Count ? this._itemSlotViews[i] : null;

//				if (itemSlotView == null)
//				{
//					itemSlotView = Object.Instantiate(this._itemSlotViewPrefab, this._itemSlotViewsContainer);
//					itemSlotView.ParentView = this;

//					this._itemSlotViews.Add(itemSlotView);
//					this._itemSlotViews[i] = itemSlotView;

//					this._displayedInventory.AddItemDisplay(itemSlotView, i);
//				}

//				itemSlotView.Open();
//			}
//		}

//		public override void UpdateLayout()
//		{
//			if (this._displayedInventory == null)
//				return;

//			for (int i = 0; i < this._displayedInventory._Capacity; i++)
//			{
//				this._itemSlotViews[i]._ItemView.UpdateLayout();
//			}
//		}

//		public void Clear(ClearMode clearMode)
//		{
//			if (this._itemSlotViews == null)
//				return;

//			for (int i = 0; i < this._itemSlotViews.Count; i++)
//			{
//				this._itemSlotViews[i].Clear(clearMode);
//			}
//		}

//		private void Update()
//		{
//			if (Input.GetKeyDown(KeyCode.Tab))
//			{
//				this.ToggleVisibility();
//			}
//		}

//#if UNITY_EDITOR
//#endif
//	}
//}

//namespace PixLi
//{
//#if UNITY_EDITOR
//	[CustomEditor(typeof(InventoryUserInterfaceView))]
//	[CanEditMultipleObjects]
//	public class InventoryUserInterfaceViewEditor : UserInterfaceViewEditor<InventoryUserInterfaceView>
//	{
//#pragma warning disable 0219, 414
//		protected InventoryUserInterfaceView sInventoryUserInterfaceView;
//#pragma warning restore 0219, 414

//		protected override void OnEnable()
//		{
//			base.OnEnable();

//			this.sInventoryUserInterfaceView = this.target as InventoryUserInterfaceView;
//		}
//	}
//#endif
//}