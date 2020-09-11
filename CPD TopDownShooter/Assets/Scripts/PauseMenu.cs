using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public string mainMenuScene;
    public bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    public GameObject pauseMenuWeb;

    public GameObject pauseFirstButton;
    public GameObject pauseFirstButtonWeb;


    public Controls controls;

    public GameObject touchPauseButton;


    bool onWeb = false;

    private void Start()
    {
#if (UNITY_WEBGL)
        onWeb = true;
#endif
        gameIsPaused = false;
        controls = new Controls();
        controls.Enable();
        controls.Player.Pause.performed += Pause_performed;

    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {

        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (onWeb)
        {
            pauseMenuWeb.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(pauseFirstButtonWeb);
        }
        else
        {
            pauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
        }

        Time.timeScale = 0.0f;
        gameIsPaused = true;


        touchPauseButton.SetActive(false);

    }

    public void Resume()
    {
        if (onWeb)
        {
            pauseMenuWeb.SetActive(false);
        }
        else
        {
            pauseMenuUI.SetActive(false);
        }

        Time.timeScale = 1.0f;
        gameIsPaused = false;
        touchPauseButton.SetActive(true);
    }

    public void LoadMenu()
    {
        Debug.Log("Loading Menu...");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(mainMenuScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
