using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.StateMachines
{
    public interface IState
    {
        public void OnStateEnter();
        public void OnStateUpdate();
        public void OnStateExit();
        public void OnStateFixedUpdate();
    }
}