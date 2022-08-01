using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

public class FireballAbilityBehaviour : AbilityBehaviour<Character>
{
	public override void Apply(Character entity)
	{
		this.transform.position = entity._AbilityInstantiationPoint.position;
		this.transform.rotation = entity._AbilityInstantiationPoint.rotation;
	}

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

	private void OnTriggerEnter(Collider other)
	{
		HealthFloat healthFloat = other.GetComponentInParent<HealthFloat>();

		if (healthFloat != null)
		{
			healthFloat.Reduce(value: this._damage);

			Object.Destroy(this.gameObject);
		}
	}
}