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
	public abstract class Item<TInventoryItemSharedData> : MonoBehaviour, IItem, IInteractable<Collider>, IInteractable<Collider2D>
	where TInventoryItemSharedData : InventoryItemSharedData
	{
		[SerializeField] protected TInventoryItemSharedData inventoryItemSharedData;
		public TInventoryItemSharedData _InventoryItemSharedData => this.inventoryItemSharedData;

		[SerializeField] protected UnityEvent onInventoryItemPointerDown;

		public void AddToInventory(Inventory inventory)
		{
			if (inventory.Add(new InventoryItem(this.inventoryItemSharedData.Id, this.onInventoryItemPointerDown.Invoke)))
				Object.Destroy(this.gameObject);
		}

		public void AddToInventory(IInventoryHolder inventoryHolder) => this.AddToInventory(inventory: inventoryHolder._Inventory);
		public void AddToInventory(Collider collider) => this.AddToInventory(inventoryHolder: collider.GetComponent<IInventoryHolder>());
		public void AddToInventory(Collider2D collider2D) => this.AddToInventory(inventoryHolder: collider2D.GetComponent<IInventoryHolder>());

		public virtual void Interact(Collider component) => this.AddToInventory(collider: component);
		public virtual void Interact(Collider2D component) => this.AddToInventory(collider2D: component);
		public virtual void Interact(Object @object) => this.AddToInventory(inventoryHolder: @object as IInventoryHolder);

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}

	public class Item : Item<InventoryItemSharedData>
	{
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
	[CustomEditor(typeof(Item))]
	[CanEditMultipleObjects]
	public class ItemEditor : Editor
	{
#pragma warning disable 0219, 414
		private Item _sItem;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sItem = this.target as Item;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}