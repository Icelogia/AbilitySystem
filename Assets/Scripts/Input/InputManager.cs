using System;
using UnityEngine;

namespace ShatteredIceStudio.Input
{
    public class InputManager : Singleton<InputManager>
    {
        public Action OnCastStart;
        public Action OnCastEnd;

        private PlayerInput playerInput;

        private Camera mainCamera;

        protected override void Awake()
        {
            base.Awake();

            playerInput = new PlayerInput();
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            playerInput.Enable();

            playerInput.Gameplay.Cast.performed += Cast_performed;
            playerInput.Gameplay.Cast.canceled += Cast_canceled;
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }

        private void Cast_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnCastStart?.Invoke();
        }

        private void Cast_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            OnCastEnd?.Invoke();
        }

        public Vector2 GetMouseAim()
        {
            var mousePosition = playerInput.Gameplay.AimMousePosition.ReadValue<Vector2>();
            var mouseWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0f));
            return mousePosition;
        }
    }
}
