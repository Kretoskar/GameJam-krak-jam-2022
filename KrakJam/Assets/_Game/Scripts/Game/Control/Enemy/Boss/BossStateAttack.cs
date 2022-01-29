using System.Collections;
using System.Collections.Generic;
using Game.Control;
using UnityEngine;

public class BossStateAttack : MonoBehaviour, IState
{
    public void Enter(StateMachine sm)
    {
        print("attacking");
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }

    public bool Finished => true;
    public int Priority { get; set; }
}
