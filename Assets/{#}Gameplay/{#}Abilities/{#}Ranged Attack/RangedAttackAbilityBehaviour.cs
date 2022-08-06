using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Ranged Attack Ability Behaviour]", menuName = "[Ability System]/[Ranged Attack Ability Behaviour]", order = 0)]
public class RangedAttackAbilityBehaviour : AbilityBehaviour<GameObject>
{
	[SerializeField] private GameObject _projectilePrefab;
	public GameObject _ProjectilePrefab => this._projectilePrefab;

	public override void Apply(GameObject data)
	{
		Enemy enemy = data.GetComponent<Enemy>();

		Object.Instantiate(
			original: this._projectilePrefab,
			position: enemy._AbilityInstantiationPoint.position,
			rotation: enemy._AbilityInstantiationPoint.rotation
		);
	}
}