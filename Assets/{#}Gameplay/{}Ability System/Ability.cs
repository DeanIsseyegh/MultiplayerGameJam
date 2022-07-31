using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

[CreateAssetMenu(fileName = "[Ability]", menuName = "[Ability System]/[Ability]", order = 0)]
public class Ability : ScriptableObject
{
	[SerializeField] private Cooldown _cooldown;
	public Cooldown _Cooldown => this._cooldown;
}