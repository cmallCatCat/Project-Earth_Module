using System;
using Make_It_Flow.Scripts.Core;
using Make_It_Flow.Scripts.Core.Event_System.Input_Manager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Extends
{
    public class InputManager_Innovation : InputManager
    {
        public KeyCode pointerKey          = KeyCode.Mouse0;
        public KeyCode secondaryPointerKey = KeyCode.Mouse1;
        public KeyCode secondKey           = KeyCode.LeftShift;
        [SerializeField]
        private MFSelectEnum _selectType;
        public override MFSelectEnum SelectType => _selectType;

        public override Vector3 ScreenPointerPosition => Mouse.current.position.value;
        public override bool    PointerKeyPressed     => Mouse.current.leftButton.isPressed;
        public override bool    PointerKeyDown        => Mouse.current.leftButton.wasPressedThisFrame;
        public override bool    PointerKeyUp          => Mouse.current.leftButton.wasReleasedThisFrame;

        public override bool SecondaryPointerKeyPressed => Mouse.current.rightButton.isPressed;
        public override bool SecondaryPointerKeyDown    => Mouse.current.rightButton.wasPressedThisFrame;
        public override bool SecondaryPointerKeyUp      => Mouse.current.rightButton.wasReleasedThisFrame;

        private bool KeyPressed(KeyCode key)
        {
            return Keyboard.current[Enum.Parse<Key>(key.ToString())].isPressed;
        }

        private bool KeyDown(KeyCode key)
        {
            return Keyboard.current[Enum.Parse<Key>(key.ToString())].wasPressedThisFrame;
        }

        private bool KeyUp(KeyCode key)
        {
            return Keyboard.current[Enum.Parse<Key>(key.ToString())].wasReleasedThisFrame;
        }

        public override bool SecondKeyPressed => Keyboard.current.leftShiftKey.isPressed;

        public override Vector3 GetCanvasPointerPosition(CanvasManager canvasManager)
        {
            if (canvasManager.canvasRenderMode == RenderMode.ScreenSpaceOverlay)
            {
                return ScreenPointerPosition;
            }
            else
            {
                Camera  mainCamera  = canvasManager.mainCamera;
                Vector3 screenPoint = ScreenPointerPosition;
                screenPoint.z = canvasManager.transform.position.z - mainCamera.transform.position.z;
                return mainCamera.ScreenToWorldPoint(screenPoint);
            }
        }
    }
}