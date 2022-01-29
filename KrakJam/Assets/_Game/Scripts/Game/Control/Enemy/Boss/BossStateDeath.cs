using System.Collections;
using System.Collections.Generic;
using Game.Control;
using UnityEngine;

public class BossStateDeath : MonoBehaviour, IState
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enter(StateMachine sm)
    {
        Destroy(gameObject);
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
