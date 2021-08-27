using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject lives;
    public GameObject coins;
    public GameObject coins_two;
    // Update is called once per frame
    void Update()
    {
        EndGame();
    }
    void EndGame()
    {
        if (lives.transform.childCount == 0)
        {
            // if it is 0, take them to a game over screen
            //
            SceneManager.LoadScene("EndGame");
        }
        if (coins.transform.childCount == 0 && coins_two.transform.childCount == 0)
        {
            // if it is 0, take them to a game over screen
            //
            SceneManager.LoadScene("WinGame");
        }
    }
}
