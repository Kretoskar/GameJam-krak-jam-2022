using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToLevel1Audio : MonoBehaviour
{
    public void Change()
    {
        GameAudio.Instance.ChangeToLevel1();
    }
}
