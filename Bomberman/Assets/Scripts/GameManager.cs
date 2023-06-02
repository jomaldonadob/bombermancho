using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float speed = 0.0001f;
    public GameObject[] players;
    public GameObject[] Scene;
    public void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    public void Update(){

        //get all objects with tag "Scene"
        Scene = GameObject.FindGameObjectsWithTag("Scene");
        foreach (GameObject Element in Scene)
        {
            Element.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * speed*0.05f;
        }


    }

    public void CheckWinState()
    {
        int aliveCount = 0;
        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                aliveCount++;
            }
        }

        if (aliveCount <= 1)
        {
            Invoke(nameof(NewRound), 3f);

        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
