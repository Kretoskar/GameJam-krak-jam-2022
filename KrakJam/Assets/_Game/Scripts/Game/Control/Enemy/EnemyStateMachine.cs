using System.Collections;
using System.Collections.Generic;
using Game.Control;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    
    void Update()
    {
        currentState?.Execute();    
    }
}
