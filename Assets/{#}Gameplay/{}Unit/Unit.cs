using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Unit : LivingEntity
{
	[SerializeField] protected UnitStats unitStats;
	public UnitStats _UnitStats => this.unitStats;

	[SerializeField] protected NavMeshAgent navMeshAgent;
	public NavMeshAgent _NavMeshAgent => this.navMeshAgent;

	public void Move(Vector3 position)
	{
		this.navMeshAgent.SetDestination(target: position);
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this.navMeshAgent = this.GetComponent<NavMeshAgent>();
	}
#endif
}