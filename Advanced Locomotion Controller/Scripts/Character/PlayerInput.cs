using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
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
        [HideInInspector]
        public bool Interact;
        [HideInInspector]
        public bool Power;
        [HideInInspector]
        public float Zoom;
        static public bool isKeyboardAndMouse = true;
        [HideInInspector]
        public bool disableJump;

        bool disabled;
        bool _enabled;

        PlayerControls Controls;
        CinemachineFreeLook FL;
        AdvancedFootIK IK;
        CharacterMovement controller;
        ClimbBehaviour CB;
        ClimbIK CIK;
        InputAction lastAction;
         
        private void Start()
        {
            CIK = GetComponentInChildren<ClimbIK>();
            controller = GetComponentInChildren<CharacterMovement>();
            CB = GetComponentInChildren<ClimbBehaviour>();
            IK = GetComponentInChildren<AdvancedFootIK>();
            FL = GetComponentInChildren<CinemachineFreeLook>();
            Controls = InputManager.inputActions;
            Controls.Enable();
            _enabled = true;
            disabled = false;
            disableJump = false;

            Controls.Keyboard.MovementVector.performed += ctx =>
            {
                if(!disableMovement)
                    MoveInput = ctx.ReadValue<Vector2>();
                    lastAction =  Controls.Keyboard.MovementVector;
            };
            Controls.Keyboard.MovementVector.canceled += ctx =>
            {
                if (!disableMovement)
                    MoveInput = ctx.ReadValue<Vector2>();
                    lastAction =  Controls.Keyboard.MovementVector;
            };
            Controls.Keyboard.Escape.performed += ctx =>
            {
                controller.OpenMenu();
                lastAction =  Controls.Keyboard.Escape;
            };
            Controls.Keyboard.Sprint.performed += ctx =>
            {
                Sprint = true;
                lastAction =  Controls.Keyboard.Sprint;
            };
            Controls.Keyboard.Sprint.canceled += ctx =>
            {
                Sprint = false;
                lastAction =  Controls.Keyboard.Sprint;
            };
            Controls.Keyboard.Crouch.performed += ctx =>
            {
                if(!CB.climbing)
                Crouch = !Crouch;
                lastAction =  Controls.Keyboard.Crouch;
            };
            Controls.Keyboard.Jump.performed += ctx =>
            {
                if (!disableJump)
                {
                    lastAction = Controls.Keyboard.Jump;
                    DropLedge = true;
                    Jump = true;
                    if (!Crouch && !controller.MenuOpen && !disableMovement && controller.isGroundForward)
                        Invoke("LookForClimbSpot", 0.5f);
                    controller.Jump();
                }
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
                lastAction =  Controls.Keyboard.FallOff;
                if(CB.climbing && !CB.waitToStartClimb)
                {
                    CB.InitiateFallOff();
                }
            };
            Controls.Keyboard.Interact.performed += ctx =>
            {
                lastAction =  Controls.Keyboard.Interact;
                Interact = true;
            };
            Controls.Keyboard.Interact.canceled += ctx =>
            {
                Interact = false;
            };
            Controls.Mouse.Power.performed += ctx =>
            {
                lastAction =  Controls.Mouse.Power;
                Power = true;
            };
            Controls.Mouse.Power.canceled += ctx =>
            {
                Power = false;
            };


        }

        private void LookForClimbSpot()
        {
            CB.LookForClimbSpot();
        }
        private void Update()
        {
    
            if (lastAction != null && lastAction.activeControl != null) {
                isKeyboardAndMouse = lastAction.activeControl.device.description.deviceClass.Equals("Keyboard") || lastAction.activeControl.device.description.deviceClass.Equals("Mouse");
            }

            Zoom = Controls.Mouse.Zoom.ReadValue<float>();

            if (disableMovement && disabled)
            {
                _enabled = true;
                disabled = false;
                CIK.enabled = true;
                IK.enabled = false;
                MoveInput = Vector2.zero;
                controller.enabled = false;
            }
            else if(!disableMovement && _enabled)
            {
                _enabled = false;
                disabled = true;
                CIK.enabled = false;
                IK.enabled = true;
                controller.enabled = true;
                MoveInput = Vector2.zero;
            }
        }

        public void disableIK(bool state){
            IK.enabled = state;
        }
    }
}
