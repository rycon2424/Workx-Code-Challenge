using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.StateMachine
{
    public interface IState
    {
        void OnStateEnter();
        void OnStateUpdate();
        void OnStateExit();
        void OnStateFixedUpdate();
    }
}