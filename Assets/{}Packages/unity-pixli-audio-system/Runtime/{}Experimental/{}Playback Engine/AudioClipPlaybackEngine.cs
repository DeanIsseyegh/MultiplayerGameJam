using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioClipPlaybackEngine : MonoBehaviour
{
	[SerializeField] private AudioClip _audioClip;
	public AudioClip _AudioClip => this._audioClip;

	[SerializeField] private AudioSourcePlayer _audioSourcePlayer;
	public AudioSourcePlayer _AudioSourcePlayer => this._audioSourcePlayer;

	private AudioSourceController _activeAudioSourceController;

	private IEnumerator PlayProcess()
	{
		this._activeAudioSourceController = this._audioSourcePlayer.AquireAudioSourceController();
		
		do
		{
			this._activeAudioSourceController.Play(
				audioClip: this._audioClip
			);

			yield return new WaitForSecondsRealtime(this._audioClip.length);
		}
		while (this._loop);

		this._onStoppedPlaying.Invoke();

		ObjectPool._Instance.Release(this._activeAudioSourceController);
	}

	public void Play() => this.StartCoroutine(this.PlayProcess());

	[SerializeField] private bool _playOnStart;
	public bool _PlayOnStart => this._playOnStart;

	[SerializeField] private bool _loop;
	public bool _Loop => this._loop;

	[SerializeField] private UnityEvent _onStoppedPlaying;
	public UnityEvent _OnStoppedPlaying => this._onStoppedPlaying;

	private IEnumerator Start()
	{
		if (this._playOnStart)
		{
			yield return new WaitForEndOfFrame();

			this.Play();

			//? How to do it without calling `Play` each iteration?
			//? Why is it not recommended in this cae?
			//!? Because you are modifything a state of an entity that has been pooled.
			//!? It sure seems an easier and a better choice, but it may lead to unexpected bugs like something playing again when it shouldn't have.

			//TODO: You should actually have some kind of system that reverses pulled objects back to prototype state.

			//audioSourceController.AudioSource.loop = this._loop;

			//audioSourceController.Play(
			//	audioClip: this._audioClip
			//);

			//yield return new WaitForSecondsRealtime(this._audioClip.length);
		}
	}

	private void OnDestroy()
	{
		if (this._activeAudioSourceController != null)
		{
			this._activeAudioSourceController.Stop();

			ObjectPool._Instance.Release(this._activeAudioSourceController);
		}
	}
}
