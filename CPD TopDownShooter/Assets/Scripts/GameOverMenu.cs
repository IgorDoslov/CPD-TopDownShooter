using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameOverMenu : MonoBehaviour
{
    public string newGameScene;

    public GameObject playFirstButton;


    private void Start()
    {
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
}
