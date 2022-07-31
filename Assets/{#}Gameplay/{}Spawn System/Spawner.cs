using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif

using PixLi;

public class Spawner : MonoBehaviour
{
	[SerializeField] private SpawnWave[] _waves;
	public SpawnWave[] _Waves => this._waves;

	private int _currrentWaveIndex;

	[SerializeField] private SpawnPoint _enemiesSpawnPoint;

	//[SerializeField] private UnityEvent _onAllWavesSpawned;
	//public UnityEvent _OnAllWavesSpawned => this._onAllWavesSpawned;

	private IEnumerator SpawningProcess(SpawnPoint spawnPoint, SpawnWave spawnWave)
	{
		WaitForSeconds timeBetweenSpawns = new WaitForSeconds(spawnWave._DeltaTimeBetweenSpawns);

		for (int i = 0; i < spawnWave._Quantity; i++)
		{
			GameObject spawnedObject = spawnPoint.Spawn(spawnWave);

			spawnedObject.GetComponent<IHealth>().ResetHealth();

			yield return timeBetweenSpawns;
		}
	}

	public Coroutine Spawn(SpawnPoint spawnPoint, SpawnWave spawnWave) => this.StartCoroutine(this.SpawningProcess(spawnPoint, spawnWave));

	public IEnumerator SpawnWaveProcess(SpawnWave spawnWave)
	{
		yield return new WaitForSeconds(spawnWave._DelayBeforeWaveStartSpawning);

		yield return this.Spawn(this._enemiesSpawnPoint, spawnWave);

		//this._onAllWavesSpawned.Invoke();
	}

	public void SpawnWave(SpawnWave spawnWave) => this.StartCoroutine(this.SpawnWaveProcess(spawnWave: spawnWave));

	public void SpawnCurrentWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex]);
	public void SpawnPreviousWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex - 1]);
	public void SpawnNextWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex + 1]);

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(Spawner))]
	[CanEditMultipleObjects]
	public class SpawnerEditor : Editor
	{
#pragma warning disable 0219, 414
		private Spawner _sSpawner;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sSpawner = this.target as Spawner;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}