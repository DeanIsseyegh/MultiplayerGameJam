///* Created by Max.K.Kimo */

//using System.Collections;
//using System.Collections.Generic;

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Events;

//#if UNITY_EDITOR
//using UnityEditor;
//#endif

//using Kimo.Assistance;
//using Kimo.Assistance.UTILITY;

//using TMPro;

//namespace Kimo.Core.Systems
//{
//	public class BuildingGameAssetConstructor : MonoBehaviour
//	{
//		[AssetIconDisplay]
//		[SerializeField] private BuildingGameAsset _roofGameAsset;
		
//		[ReorderableList]
//		[SerializeField] private BuildingGameAssetFloorDataArray _floorData;

//		[AssetIconDisplay]
//		[SerializeField] private BuildingGameAsset _foundationGameAsset;
		
//		[Assistance.Range(0, 26)]
//		[SerializeField] private int _floorQuantity = 3;

//		public void Construct()
//		{
//			this.GetComponentSecure<MeshFilter>().sharedMesh = null;
//			this.transform.DestroyChildrenImmediateOfType<BuildingGameAsset>();

//			float heightLevel = 0f;

//			if (this._foundationGameAsset != null)
//			{
//				Instantiate(this._foundationGameAsset, this.transform.position, Quaternion.identity, this.transform);

//				heightLevel += this._foundationGameAsset._Height;
//			}

//			if (this._floorData != null && this._floorData.Value.Length > 0)
//			{
//				for (int i = 0; i < this._floorQuantity; i++)
//				{
//					int j = i % this._floorData.Value.Length;
//					if (this._floorData.Value[j]._FloorGameAsset != null)
//					{
//						Instantiate(this._floorData.Value[j]._FloorGameAsset, this.transform.position + Vector3.up * heightLevel, Quaternion.identity, this.transform);
//						heightLevel += this._floorData.Value[j]._FloorGameAsset._Height;
//					}
//				}
//			}

//			if (this._roofGameAsset != null)
//			{
//				Instantiate(this._roofGameAsset, this.transform.position + Vector3.up * heightLevel, Quaternion.identity, this.transform);

//				heightLevel += this._roofGameAsset._Height;
//			}
//		}

//		[System.Serializable]
//		public class BuildingGameAssetFloorData
//		{
//			[AssetIconDisplay]
//			[SerializeField] private BuildingGameAsset _floorGameAsset;
//			public BuildingGameAsset _FloorGameAsset { get { return this._floorGameAsset; } }

//			//public int SelectionScale;
//		}

//		[System.Serializable]
//		public class BuildingGameAssetFloorDataArray
//		{
//			public BuildingGameAssetFloorData[] Value;
//		}

//#if UNITY_EDITOR
//		public bool BuildAutomatically = false;
//		public bool ValidationApprovedEO { get; set; }

//		private void OnValidate()
//		{
//			if (this._floorQuantity < 0)
//			{
//				this._floorQuantity = 0;
//			}

//			this.ValidationApprovedEO = true;
//		}	

//		//protected override void OnDrawGizmos()
//		//{
//		//}
//#endif
//	}
//}

//namespace Kimo.Core.Systems.UTILITY
//{
//#if UNITY_EDITOR
//	[CustomEditor(typeof(BuildingGameAssetConstructor))]
//	[CanEditMultipleObjects]
//	public class BuildingGameAssetConstructorEditor : Editor
//	{
//#pragma warning disable 0219
//		private BuildingGameAssetConstructor _sBuildingGameAssetConstructor;
//#pragma warning restore 0219

//		private void OnEnable()
//		{
//			this._sBuildingGameAssetConstructor = target as BuildingGameAssetConstructor;

//			if (this._sBuildingGameAssetConstructor.BuildAutomatically)
//			{
//				this._sBuildingGameAssetConstructor.Construct();
//			}
//		}

//		public override void OnInspectorGUI()
//		{
//			DrawDefaultInspector();

//			if (this._sBuildingGameAssetConstructor.BuildAutomatically && this._sBuildingGameAssetConstructor.ValidationApprovedEO)
//			{
//				this._sBuildingGameAssetConstructor.Construct();

//				this._sBuildingGameAssetConstructor.ValidationApprovedEO = false;
//			}
//			else if (!this._sBuildingGameAssetConstructor.BuildAutomatically && GUILayout.Button("Build"))
//			{
//				this._sBuildingGameAssetConstructor.Construct();
//			}

//			if (GUILayout.Button("Bake"))
//			{
//				//MeshRenderer meshRenderer = this._sBuildingGameAssetConstructor.GetComponentSafe<MeshRenderer>();

//				//List<Material> materials;
//				//this._sBuildingGameAssetConstructor.GetComponentSafe<MeshFilter>().CombineChildrenMeshes(out materials);
//				//this._sBuildingGameAssetConstructor.GetComponent<MeshFilter>().sharedMesh.name = "Building Mesh";

//				//meshRenderer.sharedMaterials = materials.ToArray();

//				this._sBuildingGameAssetConstructor.gameObject.CombineMeshes();

//				this._sBuildingGameAssetConstructor.transform.DestroyChildrenImmediateOfType<BuildingGameAsset>();
//			}
//		}
//	}
//#endif
//}