using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
   [SerializeField] float delayTime = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        FindObjectOfType<GameStatus>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine ( WaitAndLoad() );
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds( delayTime );
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
