using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public abstract class Health : MonoBehaviour
    {
        public abstract void GetHit();
        public abstract void Heal();
    }

}