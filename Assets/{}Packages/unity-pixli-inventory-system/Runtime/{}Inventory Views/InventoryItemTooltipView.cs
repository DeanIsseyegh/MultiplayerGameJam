///* Created by Max.K.Kimo */

//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Events;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace PixLi
//{
//	public class InventoryItemTooltipUserInterfaceView : UserInterfaceView<InventoryItemTooltipUserInterfaceView.InventoryItemTooltipUserInterfaceViewDisplayData, InventoryItemTooltipUserInterfaceView.InventoryItemTooltipUserInterfaceViewOutput>
//	{
//		[System.Serializable]
//		public class InventoryItemTooltipUserInterfaceViewDisplayData : UserInterfaceViewDisplayData
//		{
//		}

//		[System.Serializable]
//		public class InventoryItemTooltipUserInterfaceViewOutput : UserInterfaceViewOutput
//		{
//		}

//		public override void Display(InventoryItemTooltipUserInterfaceViewDisplayData displayData)
//		{
//			throw new System.NotImplementedException();
//		}

//		[SerializeField] private TextMeshProUGUI _descriptionTextField;

//		[SerializeField] private Vector2 _offset;

//		public void Display(InventoryItemSharedData inventoryItemSharedData, InventoryItemSlotUserInterfaceView inventoryItemSlotView)
//		{
//			this._descriptionTextField.text = inventoryItemSharedData._Description;

//			this.rectTransform.anchoredPosition = inventoryItemSlotView._RectTransform.anchoredPosition + this._offset;
//		}

//#if UNITY_EDITOR
//#endif
//	}
//}

//namespace PixLi
//{
//#if UNITY_EDITOR
//	[CustomEditor(typeof(InventoryItemTooltipUserInterfaceView))]
//	[CanEditMultipleObjects]
//	public class InventoryItemTooltipUserInterfaceViewEditor : UserInterfaceViewEditor<InventoryItemTooltipUserInterfaceView>
//	{
//#pragma warning disable 0219, 414
//		private InventoryItemTooltipUserInterfaceView _sInventoryItemTooltipUserInterfaceView;
//#pragma warning restore 0219, 414

//		protected override void OnEnable()
//		{
//			base.OnEnable();

//			this._sInventoryItemTooltipUserInterfaceView = this.target as InventoryItemTooltipUserInterfaceView;
//		}
//	}
//#endif
//}