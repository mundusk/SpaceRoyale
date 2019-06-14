using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    [SerializeField] float shopLoadDelay = 3f;
    [SerializeField] float gameOverLoadDelay = 3f;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void GameOver()
    {
        StartCoroutine(DelayGameOverScreen());
    }

    public void PlayerDied()
    {
        FindObjectOfType<InGameUIController>().GameOver();
        GameOver();
    }

    public void LoadShop()
    {
        //Delay loading the shop to give the player a chance to collect last minute coins
        StartCoroutine(DelayShopLoad());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RoundCleared()
    {
        //TODO: Disable attack damage during shop load wait period
        FindObjectOfType<InGameUIController>().SurvivedRound();
        FindObjectOfType<PlayerController>().SavePlayerStats();
        LoadShop();
    }

    public void NextRound()
    {
        FindObjectOfType<GameSessionController>().Round += 1;
        LoadLevel();
    }

    public void Retry()
    {
        FindObjectOfType<GameSessionController>().ResetValues();
        LoadLevel();
    }

    IEnumerator DelayShopLoad()
    {
        yield return new WaitForSeconds(shopLoadDelay);
        SceneManager.LoadScene("Shop");
    }

    IEnumerator DelayGameOverScreen()
    {
        yield return new WaitForSeconds(gameOverLoadDelay);
        SceneManager.LoadScene("GameOver");
    }
}
