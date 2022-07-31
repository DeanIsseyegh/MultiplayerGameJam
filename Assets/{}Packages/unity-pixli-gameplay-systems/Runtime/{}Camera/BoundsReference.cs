/* Created by Pixel.Lifetime */

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;

using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Random = PixLi.RandomDistribution.Random;

namespace PixLi
{
	public class BoundsReference : MonoBehaviour
	{
		[SerializeField] private Bounds _bounds;

		private void CacheCorrectedBounds()
		{
			this._cachedBounds = new Bounds(
				center: this.transform.position + this._bounds.center,
				size: this._bounds.size
			);
		}

		private Bounds _cachedBounds;
		public Bounds Bounds
		{
			get => this._cachedBounds;
			set
			{
				this._bounds = value;

				this.CacheCorrectedBounds();
			}
		}

		public void Place(Vector3 position, Bounds bounds)
		{
			this.transform.position = position;
			this.Bounds = bounds;
		}

		private void Awake()
		{
			this.CacheCorrectedBounds();
		}

#if UNITY_EDITOR
		public void DrawBoundsGizmos()
		{
			Gizmos.color = Color.cyan * 0.3f;

			Gizmos.DrawCube(
				center: this.transform.position + this._bounds.center,
				size: this._bounds.size
			);

			Gizmos.color = Color.cyan;

			Gizmos.DrawWireCube(
				center: this.transform.position + this._bounds.center,
				size: this._bounds.size
			);
		}

		private void OnDrawGizmosSelected()
		{
			this.DrawBoundsGizmos();
		}

		[
			CustomEditor(
				inspectedType: typeof(BoundsReference),
				editorForChildClasses: true
			),
			CanEditMultipleObjects
		]
		public class BoundsReferenceEditor : Editor
		{
			private BoundsReference _tBoundsReference;

			private VisualElement _root;
			private VisualTreeAsset _visualTree;

			private const string EDITOR_TEMPLATE_FILE_NAME = "BoundsReferenceEditorTemplate.uxml";
			private const string EDITOR_STYLES_FILE_NAME = "BoundsReferenceEditorStyles.uss";

			public override VisualElement CreateInspectorGUI()
			{
				this._root.Clear();

				this._root.Add(
					new IMGUIContainer(() =>
					{
						using (EditorGUI.ChangeCheckScope changeCheckScope = new EditorGUI.ChangeCheckScope())
						{
							this.DrawDefaultInspector();

							if (changeCheckScope.changed)
							{
							}
						}
					})
				);

				this._visualTree?.CloneTree(this._root);
			
				return this._root;
			}

			public void OnEnable()
			{
				this._tBoundsReference = (BoundsReference)this.target;

				string editorVisualsDirectoryPath = Path.Combine("Editor", "Visuals");

				string scriptFileDirectoryPath = PathUtility.GetScriptFileDirectoryPath(this._tBoundsReference);

				this._root = new VisualElement();

				//this._visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
				//	Path.Combine(
				//		scriptFileDirectoryPath, 
				//		editorVisualsDirectoryPath,
				//		EDITOR_TEMPLATE_FILE_NAME
				//	)
				//);

				//StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
				//	Path.Combine(
				//		scriptFileDirectoryPath,
				//		editorVisualsDirectoryPath,
				//		EDITOR_STYLES_FILE_NAME
				//	)
				//);
			
				//this._root.styleSheets.Add(styleSheet);
			}
		}
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
#endif
}