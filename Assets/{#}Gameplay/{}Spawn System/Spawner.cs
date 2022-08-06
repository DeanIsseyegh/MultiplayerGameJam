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
	//[SerializeField] private SpawnWave[] _waves;
	//public SpawnWave[] _Waves => this._waves;

	//private int _currrentWaveIndex;

	[SerializeField] private Object _spawnPointProviderObject;
	private IProvider<SpawnPoint> _spawnPointProvider;
	public IProvider<SpawnPoint> _SpawnPointProvider
	{
		get
		{
			if (this._spawnPointProvider == null)
			{
				if (this._spawnPointProviderObject as GameObject != null)
					this._spawnPointProvider = (this._spawnPointProviderObject as GameObject).GetComponent<IProvider<SpawnPoint>>();
				else
					this._spawnPointProvider = this._spawnPointProviderObject as IProvider<SpawnPoint>;
			}

			return this._spawnPointProvider;
		}
	}

	//[SerializeField] private UnityEvent _onAllWavesSpawned;
	//public UnityEvent _OnAllWavesSpawned => this._onAllWavesSpawned;

	private IEnumerator SpawningProcess(IProvider<SpawnPoint> provider, SpawnWave spawnWave, SpawnWave.Data spawnWaveData)
	{
		WaitForSeconds timeBetweenSpawns = new WaitForSeconds(spawnWave._DeltaTimeBetweenSpawns);

		for (int i = 0; i < spawnWaveData._Quantity; i++)
		{
			GameObject spawnedObject = provider.Provide().Spawn(spawnWaveData);

			spawnedObject.GetComponent<IHealth>().ResetHealth();

			yield return timeBetweenSpawns;
		}
	}

	public Coroutine Spawn(IProvider<SpawnPoint> provider, SpawnWave spawnWave, SpawnWave.Data spawnWaveData) => this.StartCoroutine(this.SpawningProcess(provider, spawnWave, spawnWaveData));

	public IEnumerator SpawnWaveProcess(SpawnWave spawnWave)
	{
		yield return new WaitForSeconds(spawnWave._DelayBeforeWaveStartSpawning);

		for (int a = 0; a < spawnWave._Data.Length; a++)
		{
			this.Spawn(this._SpawnPointProvider, spawnWave, spawnWave._Data[a]);
		}

		//this._onAllWavesSpawned.Invoke();
	}

	public void SpawnWave(SpawnWave spawnWave) => this.StartCoroutine(this.SpawnWaveProcess(spawnWave: spawnWave));

	//public void SpawnCurrentWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex]);
	//public void SpawnPreviousWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex - 1]);
	//public void SpawnNextWave() => this.SpawnWave(spawnWave: this._waves[this._currrentWaveIndex + 1]);

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(Spawner))]
	[CanEditMultipleObjects]
	public class SpawnerEditor : MultiSupportEditor
	{
#pragma warning disable 0219, 414
		private Spawner _sSpawner;
#pragma warning restore 0219, 414

		//private SerializedProperty _spawnPointProviderObjectSP;

		private Object _object;

		public override void OnEnable()
		{
			base.OnEnable();

			this._sSpawner = this.target as Spawner;

			//this._spawnPointProviderObjectSP = this.serializedObject.FindProperty(propertyPath: nameof(this._sSpawner._spawnPointProviderObject));
		}

		public override void MainDrawGUI()
		{
			this.DrawDefaultInspector();

			EditorGUILayout.BeginVertical();
			{
				//this._object = EditorGUILayout.ObjectField(obj: this._object, typeof(IProvider<SpawnPoint>), allowSceneObjects: true);

				if (this._sSpawner._spawnPointProviderObject as GameObject != null && (this._sSpawner._spawnPointProviderObject as GameObject).TryGetComponent<IProvider<SpawnPoint>>(out IProvider<SpawnPoint> provider))
				{
					this._sSpawner._spawnPointProvider = provider;
				}
				else if (this._sSpawner._spawnPointProviderObject as ScriptableObject != null && (this._sSpawner._spawnPointProviderObject as IProvider<SpawnPoint>) != null)
				{
					this._sSpawner._spawnPointProvider = (this._sSpawner._spawnPointProviderObject as IProvider<SpawnPoint>);
				}
				else
				{
					this._sSpawner._spawnPointProvider = null;
					this._sSpawner._spawnPointProviderObject = null;
				}
			}
			EditorGUILayout.EndVertical();
		}
	}
#endif
}