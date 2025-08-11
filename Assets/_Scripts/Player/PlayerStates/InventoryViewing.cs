using Challenge.Inventory;
using Challenge.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Challenge.Player.States
{
    public class InventoryViewing : State<PlayerBehaviour>, IState
    {
        public InventoryViewing(PlayerBehaviour actor) : base(actor)
        {

        }

        public override void OnStateEnter()
        {
            InventoryManager.Singleton.OpenCloseInventory(true);

            Owner.PlayerInput.Player.Inventory.started += CloseInventory;
        }

        public override void OnStateExit()
        {
            InventoryManager.Singleton.OpenCloseInventory(false);

            Owner.PlayerInput.Player.Inventory.started -= CloseInventory;
        }

        private void CloseInventory(InputAction.CallbackContext context)
        {
            Owner.PlayerStateMachine.SwitchState(typeof(Locomotion));
        }
    }
}
