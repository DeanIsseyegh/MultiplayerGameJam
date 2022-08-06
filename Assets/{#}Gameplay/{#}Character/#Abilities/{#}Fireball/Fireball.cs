using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	[SerializeField] private float _movementSpeed = 5;
	public float _MovementSpeed => this._movementSpeed;

	[SerializeField] private float _damage = 10.0f;
	public float _Damage => this._damage;

	private void Update()
	{
		this.transform.Translate(Vector3.forward * this._movementSpeed * Time.deltaTime, Space.Self);
	}

	private void Awake()
	{
		Object.Destroy(this.gameObject, t: 10.0f);
	}

	[SerializeField] private LayerMask _layerMask;
	public LayerMask _LayerMask => this._layerMask;

	private void OnTriggerEnter(Collider other)
	{
		if (this._layerMask.Contains(other))
		{
			HealthFloat healthFloat = other.GetComponentInParent<HealthFloat>();

			if (healthFloat != null)
			{
				healthFloat.Reduce(value: this._damage);

				Object.Destroy(this.gameObject);
			}
		}
	}
}