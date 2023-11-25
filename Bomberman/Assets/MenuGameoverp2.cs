using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameoverp2 : MonoBehaviour
{
    public void RestartGame()

    {
        SceneManager.LoadScene("Bomberman");
    }

    public void BeginningScreen()

    {
        SceneManager.LoadScene("Menu");
    }


}
