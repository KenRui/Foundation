using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.PlayerLoop;

namespace XRFramework.Common
{
    public class LWStateMachine<T>
    {
        public delegate void StateFunc();

        private Dictionary<T, State> states = new Dictionary<T, State>();

        private State currentState = null;

        public void Add(T id, StateFunc enter, StateFunc update, StateFunc leave)
        {
            states.Add(id, new State(id, enter, update, leave));
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void SwitchTo(T state)
        {
            Debug.Assert(states.ContainsKey(state),  state.ToString() + " not state");
            var newState = states[state];
            if (currentState != null && currentState.Leave != null)
                currentState?.Leave();

            if (newState != null && newState.Enter != null)
                newState?.Enter();
            
            currentState = newState;
        }


        class State
        {
            private T stateId;
            public StateFunc Enter;
            public StateFunc Update;
            public StateFunc Leave;

            public State(T id, StateFunc enter, StateFunc update, StateFunc leave)
            {
                stateId = id;
                Enter = enter;
                Update = update;
                Leave = leave;
            }
        }
    }
}