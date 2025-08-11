using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Challenge.StateMachines;
using Challenge.Player.States;

namespace Challenge.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [Header("Player Stats")]
        public float speed = 2;

        private PlayerInput playerInput;
        private StateMachine playerStateMachine;
        private CharacterController playerCharacterController;
        private PlayerPickupHandler playerPickupHandler;

        private void Start()
        {
            playerCharacterController = GetComponent<CharacterController>();
            playerPickupHandler = GetComponent<PlayerPickupHandler>();

            SetupInput();
            SetupStateMachine();
        }

        void SetupInput()
        {
            playerInput = new PlayerInput();
            playerInput.Enable();
        }

        void SetupStateMachine()
        {
            playerStateMachine = new StateMachine();

            Locomotion locomotionState = new Locomotion(this);
            InventoryViewing inventorystate = new InventoryViewing(this);

            playerStateMachine.AddState(locomotionState, inventorystate);
            playerStateMachine.SwitchState(locomotionState);
        }

        private void Update()
        {
            playerStateMachine.StateUpdate();
        }

        // Getters / Setters
        public PlayerInput PlayerInput
        {
            get { return playerInput; }
        }

        public StateMachine PlayerStateMachine
        {
            get { return playerStateMachine; }
        }

        public PlayerPickupHandler PlayerPickupHandler
        {
            get { return playerPickupHandler; }
        }

        public CharacterController PlayerCharacterController
        {
            get { return playerCharacterController; }
        }
    }
}