using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.StateMachines
{
    public interface IState
    {
        void OnStateEnter();
        void OnStateUpdate();
        void OnStateExit();
        void OnStateFixedUpdate();
    }
}