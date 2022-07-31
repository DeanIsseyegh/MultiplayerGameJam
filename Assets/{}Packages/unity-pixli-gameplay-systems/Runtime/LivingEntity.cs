using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public class LivingEntity : Entity
	{
		protected bool alive = true;
		public bool _Alive => this.alive;

		public virtual void Resurect()
		{
			this.alive = true;
		}

		[SerializeField] private UnityEvent _onDeath;
		public UnityEvent _OnDeath => this._onDeath;

		public virtual void Die()
		{
			this.alive = false;

			this._onDeath.Invoke();
		}

		public virtual void Die(float delay)
		{
			this.alive = false;

			this._onDeath.Invoke();
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
	[CustomEditor(typeof(LivingEntity))]
	[CanEditMultipleObjects]
	public class LivingEntityEditor : Editor
	{
		private void OnEnable()
		{

		}

		public override void OnInspectorGUI()
		{
			this.DrawDefaultInspector();

#pragma warning disable 0219
			LivingEntity sLivingEntity = this.target as LivingEntity;
#pragma warning restore 0219
		}
	}
#endif
}