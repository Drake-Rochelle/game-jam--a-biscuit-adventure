using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[SerializeField] private GameEventSO pickupCheck;
		[SerializeField] private GameEventSO showMenu;
		[SerializeField] private GameEventSO fade;
		[SerializeField] private GameEventSO click;
		[SerializeField] private GameObject scene;
        [Space]
        [Space]
        [Space]
        public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool analogMovement;
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
        private bool isFocused = true;

        public void OnShowmenu(InputValue value)
		{
            if (value.Get<float>() > 0.5f)
            {
                showMenu.RaiseEvent(this, null);
            }
        }

		public void OnPickup(InputValue value)
		{
			click.RaiseEvent(this, null);
			if (value.Get<float>()>0.5f)
			{
				pickupCheck.RaiseEvent(this, null);
			}
		}

		public void OnScenechange()
		{
			fade.RaiseEvent(this, (object)scene);
		}

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
        }
        void OnApplicationFocus(bool focus)
        {
            isFocused = focus;
        }
        void Update()
        {
            bool shouldHideCursor = Time.timeScale != 0f && isFocused;
            Cursor.visible = !shouldHideCursor;
            Cursor.lockState = shouldHideCursor ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
