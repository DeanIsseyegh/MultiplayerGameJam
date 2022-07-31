///* Created by Max.K.Kimo */

//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.UI;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//namespace PixLi
//{
//	[RequireComponent(typeof(Image))]
//	public class InventoryItemSlotUserInterfaceView : UserInterfaceView<InventoryItemSlotUserInterfaceView.InventoryItemSlotUserInterfaceViewDisplayData, InventoryItemSlotUserInterfaceView.InventoryItemSlotUserInterfaceViewOutput>
//	{
//		[System.Serializable]
//		public class InventoryItemSlotUserInterfaceViewDisplayData : UserInterfaceViewDisplayData
//		{
//		}

//		[System.Serializable]
//		public class InventoryItemSlotUserInterfaceViewOutput : UserInterfaceViewOutput
//		{
//		}

//		public override void Display(InventoryItemSlotUserInterfaceViewDisplayData displayData)
//		{
//			throw new System.NotImplementedException();
//		}

//		public InventoryUserInterfaceView ParentView { get; set; }

//		[SerializeField] private InventoryItemUserInterfaceView _itemView;
//		public InventoryItemUserInterfaceView _ItemView { get { return this._itemView; } }

//		public void Clear(InventoryUserInterfaceView.ClearMode clearMode)
//		{
//			switch (clearMode)
//			{
//				case InventoryUserInterfaceView.ClearMode.Partial:

//					this._itemView.Close();

//					break;
//				case InventoryUserInterfaceView.ClearMode.Full:

//					this.Close();

//					break;
//				case InventoryUserInterfaceView.ClearMode.Destruction:

//					Object.Destroy(this.gameObject);

//					break;
//			}
//		}

//#if UNITY_EDITOR
//#endif
//	}
//}

//namespace PixLi
//{
//#if UNITY_EDITOR
//	[CustomEditor(typeof(InventoryItemSlotUserInterfaceView))]
//	[CanEditMultipleObjects]
//	public class InventoryItemSlotUserInterfaceViewEditor : UserInterfaceViewEditor<InventoryItemSlotUserInterfaceView>
//	{
//#pragma warning disable 0219, 414
//		private InventoryItemSlotUserInterfaceView _sInventoryItemSlotUserInterfaceView;
//#pragma warning restore 0219, 414

//		protected override void OnEnable()
//		{
//			base.OnEnable();

//			this._sInventoryItemSlotUserInterfaceView = this.target as InventoryItemSlotUserInterfaceView;
//		}
//	}
//#endif
//}