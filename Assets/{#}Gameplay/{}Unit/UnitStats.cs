using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Unit Stats]", menuName = "[Unit]/[Unit Stats]", order = 0)]
public class UnitStats : ScriptableObject
{
	[SerializeField] private float _maxHealth = 100.0f;
	public float _MaxHealth => this._maxHealth;
}