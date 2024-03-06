using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ALS.Aberration
{
	public class LocalController : MonoBehaviour
	{
		public delegate void PlayerWalking(bool isWalking);
		public PlayerWalking PlayerWalkingChanged;

		[SerializeField] InputManager _input;
		[SerializeField] LayerMask _interactableLayerMask;

		[SerializeField] float _movementSpeed = 2f;
		[SerializeField] float _rotateSpeed = 10f;

		bool _isWalking;
		public bool IsWalking
		{
			get => _isWalking;
			set { if (value != _isWalking) PlayerWalkingChanged?.Invoke(value); _isWalking = value; }
		}

		Vector3 _lastInteractDir;


		private void Update()
		{
			Vector2 inputVector = _input.GetMovementVectorNormalized();

			Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
			HandleMovment(inputVector, moveDir);
			//Left over from Kitchen Chaos
			//HandleInteractions(moveDir);
		}

		//private void HandleInteractions(Vector3 moveDir)
		//{
		//	if (moveDir != Vector3.zero) _lastInteractDir = moveDir;

		//	float interactDistance = 2f;
		//	if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit hit, interactDistance, _countersLayerMask))
		//	{
		//		if (hit.transform.TryGetComponent(out ClearCounter clearCounter))
		//		{
		//			clearCounter.Interact();
		//		}
		//	}
		//	else
		//	{
		//		Debug.Log("-");
		//	}
		//}

		private void HandleMovment(Vector2 inputVector, Vector3 moveDir)
		{
			float moveDistance = _movementSpeed * Time.deltaTime;
			float playerRadius = .7f;
			float playerHeight = 2f;
			bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

			if (!canMove)
			{
				Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
				canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
				if (canMove)
				{
					moveDir = moveDirX;
				}
				else
				{
					Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
					canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
					if (canMove)
					{
						moveDir = moveDirZ;
					}
				}
			}

			if (canMove)
			{
				transform.position += moveDir * moveDistance;
			}

			IsWalking = moveDir != Vector3.zero;
			//PlayerWalkingChanged?.Invoke(moveDir != Vector3.zero);

			transform.forward = Vector3.Slerp(transform.forward, moveDir, _rotateSpeed * Time.deltaTime);
			//Debug.Log(inputVector);
		}
	}
}