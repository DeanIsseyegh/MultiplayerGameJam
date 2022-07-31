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