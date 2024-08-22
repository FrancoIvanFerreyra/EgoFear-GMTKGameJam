using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum GameState
{
    play,
    pause,
    gameOver,
    win,
    menu
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject UIParent;
    public GameState currentGameState = GameState.menu;
    GameObject currentActiveUI;
    AudioSource source;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Start with main menu UI
        currentActiveUI = GameObject.Find("MainMenu");
        Time.timeScale = 0;
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Pause game during play mode
        if(Input.GetKeyDown(KeyCode.Escape) && currentGameState == GameState.play)
        {
            SetGameState(GameState.pause);
        }
    }

    void SetGameState(GameState newState)
    {
        //Updates current state of the game
        int index = 0;
        switch(newState)
        {
            //Every state has a child index number referring to respective UI state
            case GameState.play:
                index = 1;
                Time.timeScale = 1;
                source.Play();
                break;
            case GameState.pause:
                index = 2;
                Time.timeScale = 0;
                source.Pause();
                break;
            case GameState.win:
                index = 4;
                ShowFinalGold();
                source.Pause();
                Time.timeScale = 0;
                break;
            case GameState.menu:
                index = 0;
                Time.timeScale = 0;
                source.Pause();
                break;
            case GameState.gameOver:
                index = 3;
                Time.timeScale = 0;
                source.Pause();
                break;
        }
        //Activates the gameObject with the corresponding UI
        currentActiveUI.SetActive(false);
        currentActiveUI = UIParent.transform.GetChild(index).gameObject;
        currentActiveUI.SetActive(true);
        currentGameState = newState;
    }

    
    //Public methods to change game state
    public void StartGame()
    {
        SetGameState(GameState.play);
    }
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }
    public void Win()
    {
        SetGameState(GameState.win);
    }
    public void Menu()
    {
        SetGameState(GameState.menu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        //Reloads scene
        SceneManager.LoadScene(0);
        StartGame();
    }

    public void ShowFinalGold() 
    {
        //Gets current gold and shows it on screen
        GameObject win = UIParent.transform.GetChild(4).gameObject;
        win.transform.GetChild(2).GetComponent<TMP_Text>().text = $"{GoldManager.instance.currentGold}";
    }

}
