using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersTracker : MonoBehaviourSingleton<CharactersTracker>
{
	[SerializeField] private Character[] _characters;
	public Character[] _Characters => this._characters;

	public Character GetClosestCharacter(Vector3 position)
	{
		Character character = this._characters[0];

		for (int a = 1; a < this._characters.Length; a++)
		{
			if ((this._characters[a].transform.position - position).sqrMagnitude < (character.transform.position - position).sqrMagnitude)
				character = this._characters[a];
		}

		return character;
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._characters = Object.FindObjectsOfType<Character>();
	}
#endif
}