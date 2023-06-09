using System;
using JetBrains.Annotations;
using QFramework;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public static class InputReader
    {
        private static NewControls controls;

        public static event Action<Vector2> moveStart;
        public static event Action<Vector2> movePerformed;
        public static event Action          moveEnd;
        public static event Action<Vector2> newPointerPosition;
        public static event Action<Vector2> pointerDelta;
        public static event Action          click;
        public static event Action          clickEnd;
        public static event Action          secondaryClick;
        public static event Action          secondaryClickEnd;
        public static event Action          openBackpack;
        public static event Action          closeBackpack;

        public static Vector2 Move            => controls.InGame.Move.ReadValue<Vector2>();
        public static Vector2 PointerPosition => controls.InGame.PointerPosition.ReadValue<Vector2>();
        public static Vector2 PointerDelta    => controls.InGame.PointerDelta.ReadValue<Vector2>();
        public static bool    Click           => controls.InGame.Click.ReadValue<bool>();
        public static bool    SecondaryClick  => controls.InGame.SecondaryClick.ReadValue<bool>();
        public static bool    OpenBackpack    => controls.InGame.OpenBackpack.ReadValue<bool>();
        public static bool    CloseBackpack   => controls.UI.CloseBackpack.ReadValue<bool>();

        public static void Init()
        {
            controls = new NewControls();

            controls.Enable();
            InputResume();

            controls.InGame.Move.started              += ctx => moveStart?.Invoke(ctx.ReadValue<Vector2>());
            controls.InGame.Move.performed            += ctx => movePerformed?.Invoke(ctx.ReadValue<Vector2>());
            controls.InGame.Move.canceled             += ctx => moveEnd?.Invoke();
            controls.InGame.PointerPosition.performed += ctx => newPointerPosition?.Invoke(ctx.ReadValue<Vector2>());
            controls.InGame.PointerDelta.performed    += ctx => pointerDelta?.Invoke(ctx.ReadValue<Vector2>());
            controls.InGame.Click.started             += ctx => click?.Invoke();
            controls.InGame.Click.canceled            += ctx => clickEnd?.Invoke();
            controls.InGame.SecondaryClick.started    += ctx => secondaryClick?.Invoke();
            controls.InGame.SecondaryClick.canceled   += ctx => secondaryClickEnd?.Invoke();
            controls.InGame.OpenBackpack.started      += ctx => openBackpack?.Invoke();
            controls.UI.CloseBackpack.started         += ctx => closeBackpack?.Invoke();
        }

        public static void InputPause()
        {
            controls.InGame.Disable();
            controls.UI.Enable();
        }
        
        public static void InputResume()
        {
            controls.InGame.Enable();
            controls.UI.Disable();
        }
    }
}