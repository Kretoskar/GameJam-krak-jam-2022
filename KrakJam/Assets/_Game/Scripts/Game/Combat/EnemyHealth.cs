using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyHealth : Health
    {
        public Action Death;
        
        public override void GetHit()
        {
            currentHealh--;
            
            if (currentHealh < 0) currentHealh = 0;
            
            if (currentHealh == 0)
            {
                Die();
                return;
            }
        }

        private void Die()
        {
            Death?.Invoke();
        }

        public override void Heal()
        {
            
        }
    }
}