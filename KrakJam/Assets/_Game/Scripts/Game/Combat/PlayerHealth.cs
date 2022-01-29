using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Combat
{
    public class PlayerHealth : Health
    {
        [SerializeField] private List<GameObject> hearts;

        public Action Death;
        
        public override void GetHit()
        {
           // return;
            
            currentHealh--;
            
            if (currentHealh < 0) currentHealh = 0;
            
            if (currentHealh == 0)
            {
                HideHeart(currentHealh);
                Die();
                return;
            }

            HideHeart(currentHealh);
        }

        private void Die()
        {
            Death?.Invoke();
        }

        public override void Heal()
        {
            currentHealh++;

            if (currentHealh > maxHealth)
            {
                currentHealh = maxHealth;
                return;
            }

            ShowHeart(currentHealh - 1);
        }

        public void HealAll()
        {
            while (currentHealh < maxHealth)
            {
                Heal();
            }
        }

        private void HideHeart(int index)
        {
            hearts[index].SetActive(false);
        }

        private void ShowHeart(int index)
        {
            hearts[index].SetActive(true);
        }
    }
}
