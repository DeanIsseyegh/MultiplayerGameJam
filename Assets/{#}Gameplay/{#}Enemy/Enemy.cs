using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
	public Character TargetCharacter_ { get; private set; }

	[SerializeField] private float _rotationSpeed = 5.0f;
	public float _RotationSpeed => this._rotationSpeed;

	[SerializeField] private Transform _abilityInstantiationPoint;
	public Transform _AbilityInstantiationPoint => this._abilityInstantiationPoint;

	private void Update()
	{
		if (this.TargetCharacter_ != null)
		{
			this.Move(position: this.TargetCharacter_.transform.position);

			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(this.TargetCharacter_.transform.position - this.transform.position), t: this._rotationSpeed * Time.deltaTime);
		}
	}

	private void Start()
	{
		EnemiesTracker._Instance.Register(enemy: this);

		this.TargetCharacter_ = CharactersTracker._Instance.GetClosestCharacter(position: this.transform.position);
	}

	public override void Die()
	{
		base.Die();

		EnemiesTracker._Instance.Unregister(enemy: this);
	}

	public override void Resurect()
	{
		base.Resurect();

		EnemiesTracker._Instance.Register(enemy: this);
	}
}