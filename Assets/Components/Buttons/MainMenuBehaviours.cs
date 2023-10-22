using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenuBehaviours : MonoBehaviour
{
    [SerializeField]
    private GameObject options;

    public void StartGame()
    {
       SceneManager.LoadScene("LevelDesign", LoadSceneMode.Single);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuUI", LoadSceneMode.Single);
        }
    }

    public void CloseCreds () 
    {
        SceneManager.LoadScene("MainMenuUI", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BacktoStart() 
    {
        SceneManager.LoadScene("MainMenuUI", LoadSceneMode.Single);
    }

    public void BacktoGame()
    {
        SceneManager.UnloadSceneAsync("GroceryList");
        Time.timeScale = 1f;

        MainMenuSwitch mainMenuSwitch = GameObject.FindFirstObjectByType<MainMenuSwitch>();
        if (!ReferenceEquals(mainMenuSwitch, null))
        {
            mainMenuSwitch.isEscaped = false;
        }
    }

    public void Options()
    {
        options.SetActive(true);
    }
}
