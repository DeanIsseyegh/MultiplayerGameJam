/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public class Entity : MonoBehaviour
	{
		public virtual void Destroy()
		{
			Object.Destroy(this.gameObject);
		}

		public virtual void Destroy(float delay)
		{
			Object.Destroy(this.gameObject, delay);
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}
}

namespace PixLi
{
#if UNITY_EDITOR
	[CustomEditor(typeof(Entity))]
	[CanEditMultipleObjects]
	public class EntityEditor : Editor
	{
		private void OnEnable()
		{

		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();

#pragma warning disable 0219
			Entity sEntity = this.target as Entity;
#pragma warning restore 0219
		}
	}
#endif
}