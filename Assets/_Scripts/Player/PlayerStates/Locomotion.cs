using Challenge.StateMachines;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Challenge.Player.States
{
    public class Locomotion : State<PlayerBehaviour>, IState
    {
        private Vector2 currentInput;

        public Locomotion(PlayerBehaviour actor) : base(actor)
        {

        }

        public override void OnStateEnter()
        {
            Owner.PlayerInput.Player.Movement.performed += MovementPerformed;
            Owner.PlayerInput.Player.Movement.canceled += MovementPerformed;

            Owner.PlayerInput.Player.Interact.started += Owner.PlayerPickupHandler.PickUpItem;

            Owner.PlayerInput.Player.Inventory.started += OpenInventory;
        }

        public override void OnStateExit()
        {
            currentInput = Vector2.zero;

            Owner.PlayerInput.Player.Movement.performed -= MovementPerformed;
            Owner.PlayerInput.Player.Movement.canceled += MovementPerformed;

            Owner.PlayerInput.Player.Interact.started -= Owner.PlayerPickupHandler.PickUpItem;

            Owner.PlayerInput.Player.Inventory.started -= OpenInventory;
        }

        public override void OnStateUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            Vector3 movementDirection = new Vector3(currentInput.x, 0, currentInput.y);

            Owner.PlayerCharacterController.Move(movementDirection * Owner.speed * Time.deltaTime);
        }

        // Input Handler(s)
        private void MovementPerformed(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                currentInput = Vector2.zero;
            }
            else
            {
                currentInput = context.ReadValue<Vector2>();
            }
        }

        private void OpenInventory(InputAction.CallbackContext context)
        {
            Owner.PlayerStateMachine.SwitchState(typeof(InventoryViewing));
        }
    }
}