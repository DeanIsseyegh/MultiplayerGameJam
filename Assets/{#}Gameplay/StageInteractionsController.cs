using System.Collections;
using System.Collections.Generic;
using PixLi;
using UnityEngine;

public class StageInteractionsController : MonoBehaviourSingleton<StageInteractionsController>
{
	[SerializeField] private Interactable[] _interactables;
	public Interactable[] _Interactables => this._interactables;

	private int _activeInteractableIndex;

	public void ActivateNextStageInteraction()
	{
		this._interactables[++this._activeInteractableIndex].gameObject.SetActive(true);
	}
}