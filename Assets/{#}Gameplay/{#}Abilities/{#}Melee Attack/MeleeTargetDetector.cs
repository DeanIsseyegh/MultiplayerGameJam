using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MeleeTargetDetector : MonoBehaviour
{
	[SerializeField] private BoxCollider _collider;
	public BoxCollider _Collider => this._collider;

	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask => this._layerMask;

	[SerializeField] private QueryTriggerInteraction _queryTriggerInteraction;
	public QueryTriggerInteraction _QueryTriggerInteraction => this._queryTriggerInteraction;

	public Collider[] GetCollidersInRange()
	{
		Collider[] colliders = Physics.OverlapBox(
			center: this._collider.transform.position + this._collider.center,
			halfExtents: this._collider.size,
			orientation: this._collider.transform.rotation,
			layerMask: this._layerMask,
			queryTriggerInteraction: this._queryTriggerInteraction
		);

		if (colliders.Contains(this._collider))
			Debug.Log("Contains self.");

		return colliders;
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._collider = this.GetComponent<BoxCollider>();
	}
#endif
}