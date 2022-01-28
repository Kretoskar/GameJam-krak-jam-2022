using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control.Player
{
    public class PlayerStateDeath : MonoBehaviour, IState
    {
        [SerializeField] private GameObject head;
        [SerializeField] private GameObject deathObject;
        
        public void Enter(StateMachine sm)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            head.GetComponent<SpriteRenderer>().enabled = false;
            
            deathObject.SetActive(true);
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }

        public bool Finished => false;
        public int Priority { get; set; }
    }
}