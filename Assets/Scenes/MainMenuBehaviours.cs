using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuBehaviours : MonoBehaviour
{
    public string startSceneName = "FirstLevel";
    public string creditSceneName = "Credits";

    public void Start()
    {
        Button startButton = GameObject.Find("StartButton").GetComponent<Button>();
        Button optionsButton = GameObject.Find("OptionsButton").GetComponent<Button>();
        Button creditsButton = GameObject.Find("CreditsButton").GetComponent<Button>();
        Button quitButton = GameObject.Find("QuitButton").GetComponent<Button>();

        // Attach click event listeners to the buttons
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(Options);
        creditsButton.onClick.AddListener(Credits);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startSceneName, LoadSceneMode.Single);
    }

    public void Options()
    {

    }

    public void Credits()
    {
        SceneManager.LoadScene(creditSceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
