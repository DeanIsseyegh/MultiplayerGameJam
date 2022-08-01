using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[System.Serializable]
public class Ability
{
	[SerializeField] private Cooldown _cooldown;
	public Cooldown _Cooldown => this._cooldown;

	[SerializeField] private AbilitySharedData _sharedData;
	public AbilitySharedData _AharedData => this._sharedData;

	//TODO: Rename it to something like Applicator etc. Generalize the behaviour class that can modify target - like: Applicator<Character>.
	[SerializeField] private AbilityBehaviour<Character> _behaviourPrefab;
	public AbilityBehaviour<Character> _BehaviourPrefab => this._behaviourPrefab;

	public void Activate(Character character)
	{
		AbilityBehaviour<Character> abilityBehaviour = Object.Instantiate(original: this._behaviourPrefab);

		abilityBehaviour.Apply(entity: character);
	}
}