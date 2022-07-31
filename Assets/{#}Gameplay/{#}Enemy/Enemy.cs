using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
	private Character _targetCharacter;

	private void Update()
	{
		if (this._targetCharacter != null)
			this.Move(position: this._targetCharacter.transform.position);
	}

	private void Start()
	{
		this._targetCharacter = CharactersTracker._Instance.GetClosestCharacter(position: this.transform.position);
	}
}