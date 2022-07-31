using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

/// <summary>
/// Settings for the hierarchy.
/// Reload-proof Singleton :)
/// </summary>
[CreateAssetMenu(fileName = HierarchySettings.DEFAULT_HIERARCHY_SETTINGS_NAME, menuName = "Hierarchy/Settings", order = 1000)]
public class HierarchySettings : ScriptableObjectSingleton<HierarchySettings>
{
	private const string DEFAULT_HIERARCHY_SETTINGS_NAME = "[Hierarchy Settings] Default";

	//TODO: Not sure why this boolean cannot be inside `#if UNITY_EDITOR`. `A scripted object (probably HierarchySettings?) has a different serialization layout when loading. (Read 52 bytes but expected 56 bytes)` when it is inside.
	[HelpBox("Whether Hierarchy additives should work or not.")]
	[SerializeField] private bool _hierarchyEnabled;
	public bool HierarchyEnabled => this._hierarchyEnabled;

#if UNITY_EDITOR
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(HierarchySettings))]
[CanEditMultipleObjects]
public class HierarchySettingsEditor : MultiSupportEditor
{
#pragma warning disable 0219, 414
	private HierarchySettings _sHierarchySettings;
#pragma warning restore 0219, 414

	public override void OnEnable()
	{
		base.OnEnable();

		this._sHierarchySettings = this.target as HierarchySettings;
	}

	public override void MainDrawGUI()
	{
		this.DrawDefaultInspector();
	}
}
#endif