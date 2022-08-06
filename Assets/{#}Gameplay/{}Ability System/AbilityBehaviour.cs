using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehaviour<T> : ScriptableObject
{
	public abstract void Apply(T data);
}