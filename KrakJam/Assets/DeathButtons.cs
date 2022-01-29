using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathButtons : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameAudio.Instance.ChangeToLevel1();
            SceneManager.LoadScene(2);
        }       
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            GameAudio.Instance.ChangeToMainMenu();
            SceneManager.LoadScene(1);
        }
    }
}
