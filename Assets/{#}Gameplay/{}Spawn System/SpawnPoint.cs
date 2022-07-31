using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class SpawnPointBase : MonoBehaviour
{
	public abstract GameObject Spawn(SpawnWave spawnWave);

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}
#endif
}

public class SpawnPoint : SpawnPointBase
{
	public override GameObject Spawn(SpawnWave spawnWave)
	{
		GameObject spawnedObject = Object.Instantiate(spawnWave._SpawnSharedData._Prefab, this.transform.position + spawnWave._SpawnSharedData._Offset, Quaternion.identity, this.transform);
			
		return spawnedObject;
	}

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(SpawnPoint))]
	[CanEditMultipleObjects]
	public class SpawnPointEditor : Editor
	{
#pragma warning disable 0219, 414
		private SpawnPoint _sSpawnPoint;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sSpawnPoint = this.target as SpawnPoint;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}