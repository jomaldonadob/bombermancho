using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameover : MonoBehaviour
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
