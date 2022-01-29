using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToLastSpeech : MonoBehaviour
{
    public void Change()
    {
        GameAudio.Instance.ChangeToLastSpeech();
    }
}
