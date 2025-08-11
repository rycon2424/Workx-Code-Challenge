using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Challenge.StateMachines
{
    public abstract class State<T> : IState where T : MonoBehaviour
    {
        public T Owner { get; protected set; }

        public State(T owner)
        {
            Owner = owner;
        }

        public virtual void OnStateEnter() { }
        public virtual void OnStateExit() { }
        public virtual void OnStateUpdate() { }

        public virtual void OnStateFixedUpdate() { }
    }
}