using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using FasTPS;

namespace FasTPS
{
    public class PlayerInput : MonoBehaviour
    {
        [HideInInspector]
        public Vector2 MoveInput;
        [HideInInspector]
        public bool Sprint;
        [HideInInspector]
        public bool Crouch;
        [HideInInspector]
        public bool disableMovement;
        [HideInInspector]
        public bool DropLedge;
        [HideInInspector]
        public bool Jump;

        bool disabled;
        bool enabled;

        PlayerControls Controls;
        CinemachineFreeLook FL;
        AdvancedFootIK IK;
        CharacterMovement controller;
        ClimbBehaviour CB;
        ClimbIK CIK;
        private void Start()
        {
            CIK = GetComponentInChildren<ClimbIK>();
            controller = GetComponentInChildren<CharacterMovement>();
            CB = GetComponentInChildren<ClimbBehaviour>();
            IK = GetComponentInChildren<AdvancedFootIK>();
            FL = GetComponentInChildren<CinemachineFreeLook>();
            Controls = InputManager.inputActions;
            Controls.Enable();
            enabled = true;
            disabled = false;

            Controls.Keyboard.MovementVector.performed += ctx =>
            {
                if(!disableMovement)
                    MoveInput = ctx.ReadValue<Vector2>();
            };
            Controls.Keyboard.MovementVector.canceled += ctx =>
            {
                if (!disableMovement)
                    MoveInput = ctx.ReadValue<Vector2>();
            };
            Controls.Keyboard.Escape.performed += ctx =>
            {
                controller.OpenMenu();
            };
            Controls.Keyboard.Sprint.performed += ctx =>
            {
                Sprint = true;
            };
            Controls.Keyboard.Sprint.canceled += ctx =>
            {
                Sprint = false;
            };
            Controls.Keyboard.Crouch.performed += ctx =>
            {
                if(!CB.climbing)
                Crouch = !Crouch;
            };
            Controls.Keyboard.Jump.performed += ctx =>
            {
                DropLedge = true;
                Jump = true;
                if (!Crouch && !controller.MenuOpen && !disableMovement && controller.isGroundForward)
                    Invoke("LookForClimbSpot", 0.5f);
                    controller.Jump();
            };
            Controls.Keyboard.Jump.canceled += ctx =>
            {
                Jump = false;
            };
            Controls.Keyboard.Cover.performed += ctx =>
            {
                if (controller.Covering && !controller.MenuOpen && !disableMovement)
                    controller.Cover();
            };
            Controls.Keyboard.FallOff.performed += ctx =>
            {
                if(CB.climbing && !CB.waitToStartClimb)
                {
                    CB.InitiateFallOff();
                }
            };
        }

        private void LookForClimbSpot()
        {
            CB.LookForClimbSpot();
        }
        private void Update()
        {
            if (disableMovement && disabled)
            {
                enabled = true;
                disabled = false;
                CIK.enabled = true;
                IK.enabled = false;
                controller.enabled = false;
            }
            else if(!disableMovement && enabled)
            {
                enabled = false;
                disabled = true;
                CIK.enabled = false;
                IK.enabled = true;
                controller.enabled = true;
            }
        }
    }
}
