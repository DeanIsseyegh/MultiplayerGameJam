using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
	[System.Serializable]
	private struct Stage
	{
		[SerializeField] private SpawnWave[] _waves;
		public SpawnWave[] _Waves => this._waves;

		public int NextWaveIndex { get; set; }
	}

	[SerializeField] private Spawner _spawner;
	public Spawner _Spawner => this._spawner;

	[SerializeField] private EnemiesTracker _enemiesTracker;
	public EnemiesTracker _EnemiesTracker => this._enemiesTracker;

	[SerializeField] private Stage[] _stages;
	//public Stage[] _Stages => this._stages;

	public int CurrentStageIndex_ { get; private set; }

	[SerializeField] private View _gameOverView;
	public View _GameOverView => this._gameOverView;

	public void GameOver()
	{
		this._enemiesTracker.DestroyAliveEnemies();

		Time.timeScale = 0.0f;

		this._gameOverView.Show();
	}

	[SerializeField] private UnityEvent _onStageWon;
	public UnityEvent _OnStageWon => this._onStageWon;

	[SerializeField] private View _winStageView;
	public View _WinStageView => this._winStageView;

	public void WinStage()
	{
		this._onStageWon.Invoke();

		this._winStageView.Show();
	}

	public void StartNextWave()
	{
		Stage stage = this._stages[this.CurrentStageIndex_];

		if (stage.NextWaveIndex < stage._Waves.Length)
			this._spawner.SpawnWave(spawnWave: stage._Waves[stage.NextWaveIndex++]);
		else
			this.WinStage();
	}

	[SerializeField] private UnityEvent _onStageStart;
	public UnityEvent _OnStageStart => this._onStageStart;

	public void StartStage(int stageIndex)
	{
		Time.timeScale = 1.0f;

		this.CurrentStageIndex_ = stageIndex;

		this.StartNextWave();

		this._onStageStart.Invoke();
	}

	public void StartCurrentStage() => this.StartStage(stageIndex: this.CurrentStageIndex_);
	public void StartNextStage() => this.StartStage(stageIndex: ++this.CurrentStageIndex_);
}