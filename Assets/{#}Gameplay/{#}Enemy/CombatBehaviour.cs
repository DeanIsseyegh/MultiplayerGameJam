using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class CombatBehaviour : MonoBehaviour
{
	private Enemy _enemy;
	
	[SerializeField] private float _attackRange = 1.0f;
	public float _AttackRange => this._attackRange;

	[SerializeField] private float _attackDamage = 5.0f;
	public float _AttackDamage => this._attackDamage;

	[SerializeField] private Ability<GameObject> _attackAbility;
	public Ability<GameObject> _AttackAbility => this._attackAbility;

	private void Update()
	{
		if (this._enemy.TargetCharacter_ != null && (this._enemy.TargetCharacter_.transform.position - this.transform.position).sqrMagnitude < this._attackRange * this._attackRange && this._attackAbility._Cooldown._Finished)
		{
			this._attackAbility.Activate(data: this.gameObject);

			this._attackAbility._Cooldown.Reset();
		}
	}

	private void Awake()
	{
		this._enemy = this.GetComponent<Enemy>();
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		GizmosUtility.DrawCombinedSphere(this.transform.position, this._attackRange, Color.red);
	}
#endif
}