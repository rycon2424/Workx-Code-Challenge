using Challenge.World.Interactables;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Challenge.Player
{
    public class PlayerPickupHandler : MonoBehaviour
    {
        private List<IPickUpAble> pickupsInRange = new List<IPickUpAble>();

        // Trigger Events

        private void OnTriggerEnter(Collider other)
        {
            IPickUpAble pickup = other.GetComponent<IPickUpAble>();

            if (pickup != null)
                pickupsInRange.Add(pickup);
        }

        private void OnTriggerExit(Collider other)
        {
            IPickUpAble pickup = other.GetComponent<IPickUpAble>();

            if (pickup != null)
                pickupsInRange.Remove(pickup);
        }

        // Input Events
        public void PickUpItem(InputAction.CallbackContext context)
        {
            if (pickupsInRange.Count > 0)
            {
                IPickUpAble pickup = pickupsInRange[0];

                pickup.OnPickedUp();

                pickupsInRange.Remove(pickup);
            }
        }
    }
}