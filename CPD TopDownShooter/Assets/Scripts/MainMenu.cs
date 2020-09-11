using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject playFirstButton;

    public GameObject button;

    bool onWeb = false;
    private void Start()
    {
#if (UNITY_WEBGL)
        onWeb = true;
#endif
        if (onWeb)
            button.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(playFirstButton);
    }

    private void Update()
    {
        
    }
    public void PlayGame()
    {
        FindObjectOfType<LevelLoader>().LoadNextLevel();
        SceneManager.LoadScene(newGameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
