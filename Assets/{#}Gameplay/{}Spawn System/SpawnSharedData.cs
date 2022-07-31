using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "[Spawn Shared Data] name", menuName = "Spawn Shared Data")]
public class SpawnSharedData : ScriptableObject
{
	[SerializeField] private GameObject _prefab;
	public GameObject _Prefab { get { return this._prefab; } }

	[SerializeField] private Vector3 _offset;
	public Vector3 _Offset { get { return this._offset; } }

#if UNITY_EDITOR
	//protected override void OnDrawGizmos()
	//{
	//}

	[CustomEditor(typeof(SpawnSharedData))]
	[CanEditMultipleObjects]
	public class SpawnSharedDataEditor : Editor
	{
#pragma warning disable 0219, 414
		private SpawnSharedData _sSpawnSharedData;
#pragma warning restore 0219, 414

		private void OnEnable()
		{
			this._sSpawnSharedData = this.target as SpawnSharedData;
		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();
		}
	}
#endif
}