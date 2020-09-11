using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    public List<string> levels = new List<string>();
    public int currentLevel = 0;

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        //{
        //    LoadNextLevel();
        //}
    }

    public void LoadNextLevel()
    {
        var sceneIndex = SceneManager.GetSceneByName(levels[currentLevel]).buildIndex;
        StartCoroutine(LoadLevel(sceneIndex));
        currentLevel += 1;

        if (currentLevel == levels.Count)
            currentLevel = 0;


    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
