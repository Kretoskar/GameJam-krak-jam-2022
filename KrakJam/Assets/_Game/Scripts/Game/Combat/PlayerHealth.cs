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
        [SerializeField] private int maxHealth = 3;
        [SerializeField] private List<GameObject> hearts;

        private int currentHealh;

        private void Awake()
        {
            currentHealh = maxHealth;
        }

        public override void GetHit()
        {
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
            print("u dead");
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
