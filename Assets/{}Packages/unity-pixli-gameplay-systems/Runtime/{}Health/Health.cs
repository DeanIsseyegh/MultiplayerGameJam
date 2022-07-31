/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	public interface IHealth
	{
		void ResetHealth();
	}

	public interface IHealth<TValue> : IHealth
	{
		void Add(TValue value);
		void Reduce(TValue value);
	}

	[RequireComponent(typeof(LivingEntity))]
    public class Health<TValue, TRange> : MonoBehaviour, IHealth<TValue>
		where TValue : IComparable, IComparable<TValue>, IConvertible, IEquatable<TValue>, IFormattable
		where TRange : IRange<TValue>
    {
		private LivingEntity _livingEntity;
		
		[SerializeField] private BoundedValue<TValue, TRange> _healthPoints;
		public BoundedValue<TValue, TRange> _HealthPoints => this._healthPoints;

		public UnityEvent _OnHealthChanged => this._healthPoints._OnValueChanged;
		public UnityEvent<float> _OnHealthChangedRatio => this._healthPoints._OnValueChangedRatio;

		public void Add(TValue value)
		{
			if (!this._livingEntity._Alive)
				this._livingEntity.Resurect();

			this._healthPoints.Add(value);
		}

		public void Reduce(TValue value)
		{
			if (!this._livingEntity._Alive)
				return;
			
			this._healthPoints.Reduce(value);

			if (BoundedValue<TValue, TRange>.s_calculator.Subtract(this._healthPoints.Value_, this._healthPoints._ValueRange._ValueVeryCloseToZero).CompareTo(this._healthPoints._ValueRange.Min) <= 0)
				this._livingEntity.Die();
		}

		public void ResetHealth()
		{
			this._healthPoints.Reset();

			if (!this._livingEntity._Alive && this._healthPoints.Value_.CompareTo(this._healthPoints._ValueRange.Min) > 0)
				this._livingEntity.Resurect();
		}

		private void Awake()
		{
			this._livingEntity = this.GetComponent<LivingEntity>();

			this.ResetHealth();
		}

#if UNITY_EDITOR
		//protected override void OnDrawGizmos()
		//{
		//}
#endif
	}
}