using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//? Requires singleton SO.
//TODO: refactor to be a singleton SO.
[CreateAssetMenu(fileName = "[Master Audio Source Control Group]", menuName = "[Audio]/[Master Audio Source Control Group]")]
public class MasterAudioSourceControlGroup : ScriptableObject
{
	[SerializeField] private UnityEvent<float> _onVolumeChange;
	public UnityEvent<float> _OnVolumeChange => this._onVolumeChange;

	//public event Action<float> OnVolumeChange = delegate { };

	[Range(0.0f, 1.0f)]
	[SerializeField] private float _volume = 1.0f;
	public float Volume
	{
		get => this._volume;
		set
		{
			this._volume = Mathf.Clamp01(value: value);

			this._onVolumeChange.Invoke(arg0: this._volume);
		}
	}
}
