using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBehavior : MonoBehaviour
{
    public int levelIndex;
    public bool curserLocked = false;
    public bool curserVisible = true;

    public void changeSceneTo(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void closeApplication()
    {
        Application.Quit();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelIndex);
            setCurser();
        }
    }

    void setCurser()
    {
        if (!curserLocked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = curserVisible;
    }

}
