using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected int maxHealth = 3;
        
        protected int currentHealh;

        private void Awake()
        {
            currentHealh = maxHealth;
        }
        
        public abstract void GetHit();
        public abstract void Heal();
    }

}