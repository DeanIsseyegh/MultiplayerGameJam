using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[CreateAssetMenu(fileName = "[Melee Atack Ability Behaviour]", menuName = "[Ability System]/[Melee Atack Ability Behaviour]", order = 0)]
public class MeleeAttackAbilityBehaviour : AbilityBehaviour<GameObject>
{
	public override void Apply(GameObject data)
	{
		MeleeTargetDetector meleeTargetDetector = data.GetComponentInChildren<MeleeTargetDetector>(includeInactive: true);

		Collider[] colliders = meleeTargetDetector.GetCollidersInRange();

		for (int a = 0; a < colliders.Length; a++)
		{
			HealthFloat healthFloat = colliders[a].GetComponentInParent<HealthFloat>();

			if (healthFloat != null)
			{
				healthFloat.Reduce(value: data.GetComponent<CombatBehaviour>()._AttackDamage);
			}
		}
	}
}