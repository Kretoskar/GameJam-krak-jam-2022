using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Control
{
    public class StateMachine : MonoBehaviour
    {
        [Header("READONLY FIELDS")] [SerializeField] private string state = "";
        [SerializeField] protected List<string> asynchronousStatesNames = null;

        protected List<IAsynchronousState> asynchronousStates;

        protected IState currentState;

        protected int ratioTotal;
        protected Dictionary<IState, Vector2> statesWIthPriorities;

        protected void ChangeState(IState newState)
        {
            if (currentState != null)
                if (!currentState.Finished || currentState == newState)
                    return;

            currentState?.Exit();
            currentState = newState;
            state = newState.GetType().Name;
            newState.Enter(this);
        }
        
        protected void ForceChangeState(IState newState)
        {
            if (currentState != null)
                if (currentState == newState)
                    return;

            currentState?.Exit();
            currentState = newState;
            state = newState.GetType().Name;
            newState.Enter(this);
        }
        
        protected void ChangeOrRestartState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            state = newState.GetType().Name;
            newState.Enter(this);
        }

        protected void StartAsynchronousState(IAsynchronousState asyncState)
        {
            if (asynchronousStates == null) asynchronousStates = new List<IAsynchronousState>();

            asynchronousStates.Add(asyncState);
            asynchronousStatesNames.Add(asyncState.GetType().Name);

            asyncState.Enter();
        }

        protected void SelectStateRandomlyWithPriority()
        {
            int choosenStateIndex = Random.Range(0, ratioTotal);

            foreach (var state in statesWIthPriorities)
            {
                if (choosenStateIndex >= state.Value.x && choosenStateIndex <= state.Value.y)
                    ChangeState(state.Key);
            }
        }
    }
}