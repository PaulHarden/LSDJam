using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool piss;
		public bool pause;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
				LookInput(value.Get<Vector2>());
		}
		public void OnMove(InputValue value) => MoveInput(value.Get<Vector2>());
		public void OnJump(InputValue value) => JumpInput(value.isPressed);
		public void OnSprint(InputValue value) => SprintInput(value.isPressed);
		public void OnInteract(InputValue value) => InteractInput(value.isPressed);
		public void OnPiss(InputValue value) => PissInput(value.isPressed);
		public void OnPause(InputValue value) => PauseInput(value.isPressed);
#endif

		public void LookInput(Vector2 newLookDirection) => look = newLookDirection;
		public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
		public void JumpInput(bool newJumpState) => jump = newJumpState;
		public void SprintInput(bool newSprintState) => sprint = newSprintState;
		public void InteractInput(bool newInteractState) => interact = newInteractState;
		public void PissInput(bool newPissState) => piss = newPissState;
		public void PauseInput(bool newPauseState) => pause = newPauseState;
		private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);
		public void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
	
}