using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	PlayerMapping _inputActions;

	private void Awake()
	{
		_inputActions = new PlayerMapping();
		_inputActions.PlayerInput.Enable();
	}

	public Vector2 GetMovementVectorNormalized()
	{
		Vector2 inputVector = _inputActions.PlayerInput.Move.ReadValue<Vector2>();
		// Can Debug the input
		return inputVector;
	}
}
