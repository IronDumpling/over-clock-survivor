using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UnityEngine.SceneManagement;

public class GameManager : NoDestroyMonoSingleton<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {

    }

    public void StartLevel()
    {
        
    }

    public void RestartGame()
    {

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        //Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        //Time.timeScale = 1;
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
