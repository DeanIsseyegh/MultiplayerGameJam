using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesTracker : MonoBehaviourSingleton<EnemiesTracker>
{
	private List<Enemy> _aliveEnemies = new List<Enemy>();
	//public List<Enemy> _AliveEnemies => this._aliveEnemies;

	[SerializeField] private UnityEvent _onAllEnemiesDead;
	public UnityEvent _OnAllEnemiesDead => this._onAllEnemiesDead;

	public void Register(Enemy enemy)
	{
		if (!this._aliveEnemies.Contains(item: enemy))
			this._aliveEnemies.Add(item: enemy);
	}

	public void Unregister(Enemy enemy)
	{
		this._aliveEnemies.Remove(item: enemy);

		if (this._aliveEnemies.Count == 0)
			this._onAllEnemiesDead.Invoke();
	}

	public void DestroyAliveEnemies()
	{
		for (int a = this._aliveEnemies.Count - 1; a >= 0; a--)
		{
			this._aliveEnemies[a].Destroy();
		}

		this._aliveEnemies.Clear();
	}
}