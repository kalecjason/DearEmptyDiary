using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AcquirePage : MonoBehaviour {

    // Maximum loadable level and current level
    private int maxLevel, currentLevel;

    // Sets up the maxLevel and currentLevel
    void Start() {
        maxLevel = SceneManager.sceneCountInBuildSettings - 1;
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    // Loads next level after acquiring the diary page
    public void AcquireDiary()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

        if (currentLevel + 1 <= maxLevel)
        {
            SceneManager.LoadScene(currentLevel + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}

