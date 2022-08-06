using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[System.Serializable]
public class Ability<T>
{
	[SerializeField] private Cooldown _cooldown;
	public Cooldown _Cooldown => this._cooldown;

	[SerializeField] private AbilitySharedData _sharedData;
	public AbilitySharedData _AharedData => this._sharedData;

	//TODO: Rename it to something like Applicator etc. Generalize the behaviour class that can modify target - like: Applicator<Character>.
	[SerializeField] private AbilityBehaviour<T> _abilityBehaviour;
	public AbilityBehaviour<T> _AbilityBehaviour => this._abilityBehaviour;

	public void Activate(T data) => this._abilityBehaviour.Apply(data: data);
}