using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMainMenu : MonoBehaviour
{
    public void Change()
    {
        GameAudio.Instance.ChangeToMainMenu();
    }
}
