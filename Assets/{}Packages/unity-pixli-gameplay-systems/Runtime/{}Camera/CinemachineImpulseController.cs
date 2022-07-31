using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Cinemachine;
using Cinemachine.Utility;

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class CinemachineImpulseController
{
	[SerializeField] private CinemachineImpulseSource impulseSource;
	public CinemachineImpulseSource ImpulseSource => this.impulseSource;

	public enum ImpulseType { Light, Medium, Heavy }

	[SerializeField] private SignalSourceAsset lightSignal;
	[SerializeField] private SignalSourceAsset mediumSignal;
	[SerializeField] private SignalSourceAsset heavySignal;

	public void GenerateImpulse(ImpulseType impulseType)
	{
		switch (impulseType)
		{
			case ImpulseType.Light:

				this.impulseSource.m_ImpulseDefinition.m_RawSignal = this.lightSignal;

				break;
			case ImpulseType.Medium:

				this.impulseSource.m_ImpulseDefinition.m_RawSignal = this.mediumSignal;

				break;
			case ImpulseType.Heavy:

				this.impulseSource.m_ImpulseDefinition.m_RawSignal = this.heavySignal;

				break;
		}
			
		this.impulseSource.GenerateImpulse();

#if UNITY_EDITOR
		//Object.FindObjectOfType<CinemachineVirtualCamera>()? // - no method or prop for getting extensions.
#endif
	}

#if UNITY_EDITOR
	//protected virtual void OnDrawGizmos()
	//{
	//}
#endif
}

//#if UNITY_EDITOR
//	[CustomEditor(typeof(CinemachineImpulseController))]
//	[CanEditMultipleObjects]
//	public class CinemachineImpulseControllerEditor : MultiSupportEditor
//	{
//#pragma warning disable 0219, 414
//		private CinemachineImpulseController _sCinemachineImpulseController;
//#pragma warning restore 0219, 414

//		protected override void OnEnable()
//		{
//			base.OnEnable();

//			this._sCinemachineImpulseController = this.target as CinemachineImpulseController;
//		}

//		public override void MainDrawGUI()
//		{
//			this.DrawDefaultInspector();
//		}
//	}
//#endif