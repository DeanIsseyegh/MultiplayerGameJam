///* Created by Max.K.Kimo */

//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//using TMPro;

//using Kimo.Assistance;
//using Kimo.Assistance.UTILITY;

//namespace Kimo.Core
//{
//	public class InventoryItemUserInterfaceView : UserInterfaceView<InventoryItemUserInterfaceView.InventoryItemUserInterfaceViewDisplayData, InventoryItemUserInterfaceView.InventoryItemUserInterfaceViewOutput>, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
//	{
//		[System.Serializable]
//		public class InventoryItemUserInterfaceViewDisplayData : UserInterfaceViewDisplayData
//		{
//		}

//		[System.Serializable]
//		public class InventoryItemUserInterfaceViewOutput : UserInterfaceViewOutput
//		{
//		}

//		public override void Display(InventoryItemUserInterfaceViewDisplayData displayData)
//		{
//			throw new System.NotImplementedException();
//		}

//		[SerializeField] InventoryItemSlotUserInterfaceView _parentSlotView;
//		private InventoryUserInterfaceView _parentInventoryView { get { return this._parentSlotView.ParentView; } }

//		[SerializeField] private Image _itemIconImageField;
//		public Image _ItemIconImageField { get { return this._itemIconImageField; } }

//		[SerializeField] private TextMeshProUGUI _itemQuantityTextField;
//		public TextMeshProUGUI _ItemQuantityTextField { get { return this._itemQuantityTextField; } }

//		public InventoryItem DisplayedItem_ { get; private set; }

//		public void Display(InventoryItem item)
//		{
//			this.DisplayedItem_ = item;

//			this.InitializeLayout();
//			this.UpdateLayout();
//		}

//		public virtual void InitializeLayout()
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			if (this.DisplayedItem_.InventoryItemSharedData_._Stackable)
//				this._itemQuantityTextField.ActivateGameObject();
//			else
//				this._itemQuantityTextField.DeactivateGameObject();

//			this._itemIconImageField.sprite = this.DisplayedItem_.InventoryItemSharedData_._Icon;
//		}

//		public override void UpdateLayout()
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			if (this.DisplayedItem_.InventoryItemSharedData_._Stackable)
//				this._itemQuantityTextField.text = this.DisplayedItem_._Quantity.ToString();
//		}

//		public void Clear()
//		{
//			this.DisplayedItem_ = null;
//		}

//		public virtual void OnPointerEnter(PointerEventData eventData)
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			this._parentInventoryView._ItemTooltipView.Show();
//			this._parentInventoryView._ItemTooltipView.Display(this.DisplayedItem_.InventoryItemSharedData_, this._parentSlotView);

//			this.DisplayedItem_.OnPointerEnter?.Invoke();
//		}

//		public virtual void OnPointerExit(PointerEventData eventData)
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			this._parentInventoryView._ItemTooltipView.Hide();

//			this.DisplayedItem_.OnPointerExit?.Invoke();
//		}

//		public virtual void OnPointerDown(PointerEventData eventData)
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			this.DisplayedItem_.OnPointerDown?.Invoke();
//		}

//		public virtual void OnPointerUp(PointerEventData eventData)
//		{
//			if (this.DisplayedItem_ == null)
//				return;

//			this.DisplayedItem_.OnPointerUp?.Invoke();
//		}

//#if UNITY_EDITOR
//#endif
//	}
//}

//namespace Kimo.Core.UTILITY
//{
//#if UNITY_EDITOR
//	[CustomEditor(typeof(InventoryItemUserInterfaceView))]
//	[CanEditMultipleObjects]
//	public class InventoryItemUserInterfaceViewEditor : UserInterfaceViewEditor<InventoryItemUserInterfaceView>
//	{
//#pragma warning disable 0219, 414
//		private InventoryItemUserInterfaceView _sInventoryItemUserInterfaceView;
//#pragma warning restore 0219, 414

//		protected override void OnEnable()
//		{
//			base.OnEnable();

//			this._sInventoryItemUserInterfaceView = this.target as InventoryItemUserInterfaceView;
//		}
//	}
//#endif
//}