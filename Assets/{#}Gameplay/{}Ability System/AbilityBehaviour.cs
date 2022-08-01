using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehaviour<TEntity> : MonoBehaviour
{
	public abstract void Apply(TEntity entity);
}