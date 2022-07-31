using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Spawn Wave]", menuName = "[Spawn Wave]")]
public class SpawnWave : ScriptableObject
{
	[SerializeField] private SpawnSharedData _spawnSharedData;
	public SpawnSharedData _SpawnSharedData => this._spawnSharedData;

	[Min(0)]
	[SerializeField] private int _quantity = 5;
	public int _Quantity => this._quantity;

	[SerializeField] private float _deltaTimeBetweenSpawns = 2.0f;
	public float _DeltaTimeBetweenSpawns => this._deltaTimeBetweenSpawns;

	[SerializeField] private float _delayBeforeWaveStartSpawning = 5.0f;
	public float _DelayBeforeWaveStartSpawning => this._delayBeforeWaveStartSpawning;
}