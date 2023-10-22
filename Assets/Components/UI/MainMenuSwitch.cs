using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSwitch : MonoBehaviour
{
    public bool isEscaped = false;

    public void Update()
    {
        //To access settings
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ReferenceEquals(GameObject.FindGameObjectWithTag("Player"), null)) return;
            if (!isEscaped)
            {
                SceneManager.LoadScene("GroceryList", LoadSceneMode.Additive);
                Time.timeScale = 0f;
                isEscaped = true;
            }
            else
            {
                SceneManager.UnloadSceneAsync("GroceryList");
                Time.timeScale = 1f;
                isEscaped = false;
            }
        }
    }
}
