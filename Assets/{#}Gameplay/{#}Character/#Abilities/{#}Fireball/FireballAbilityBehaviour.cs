using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[CreateAssetMenu(fileName = "[Fireball Ability Behaviour]", menuName = "[Ability System]/[Fireball Ability Behaviour]", order = 0)]
public class FireballAbilityBehaviour : AbilityBehaviour<Character>
{
	[SerializeField] private Fireball _fireballPrefab;
	public Fireball _FireballPrefab => this._fireballPrefab;

	public override void Apply(Character data)
	{
		Fireball fireball = Object.Instantiate(
			original: this._fireballPrefab, 
			position: data._AbilityInstantiationPoint.position, 
			rotation: data._AbilityInstantiationPoint.rotation
		);
	}
}