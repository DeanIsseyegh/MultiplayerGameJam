using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Character : Unit
{
	[SerializeField] private PlayerInput _playerInput;
	public PlayerInput _PlayerInput => this._playerInput;

	private InputAction.CallbackContext _onMovementCallbackContext;

	public void OnMovement(InputAction.CallbackContext callbackContext)
	{
		this._onMovementCallbackContext = callbackContext;
	}

	//[SerializeField] private Ability[] _abilities;
	//public Ability[] _Abilities => this._abilities;

	[SerializeField] private Ability<Character> _mainAbility;
	public Ability<Character> _MainAbility => this._mainAbility;

	[SerializeField] private Transform _abilityInstantiationPoint;
	public Transform _AbilityInstantiationPoint => this._abilityInstantiationPoint;

	public void OnMain(InputAction.CallbackContext callbackContext)
	{
		if (callbackContext.started && this._mainAbility._Cooldown._Finished)
		{
			this._mainAbility.Activate(data: this);

			this._mainAbility._Cooldown.Reset();
		}
	}

	private void Update()
	{
		Vector2 movementInput = this._onMovementCallbackContext.ReadValue<Vector2>();
		Vector3 rawMovementInput = new Vector3(x: movementInput.x, y: 0.0f, z: movementInput.y);

		this.Move(
			position: this.transform.position + rawMovementInput
		);
	}

	private void Awake()
	{
		this._playerInput.ActivateInput();
	}

	private void OnDestroy()
	{
		this._playerInput.DeactivateInput();
	}

#if UNITY_EDITOR
	private void Reset()
	{
		this._playerInput = this.GetComponent<PlayerInput>();
	}
#endif
}