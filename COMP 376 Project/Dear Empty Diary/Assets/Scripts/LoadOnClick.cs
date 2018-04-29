using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour {

    // The main menu (to return to), the menu shown when paused (escMenu), 
    // the new game (to overwrite) - confirmationWindow, 
    // and errorWindow is if you don't have a saved game and want to continue
    private GameObject mainMenu, escMenu, confirmationWindow, errorWindow;

    GameObject dialogueBox;

    // # of levels in the build settings
    private int maxLevel;

    // checks if the dialogue box was active before pausing
    bool wasOn = false;

    void Start() {

        PlayerPrefs.SetInt("Health", PlayerPrefs.GetInt("Health"));

        maxLevel = SceneManager.sceneCountInBuildSettings - 1;

        Time.timeScale = 1;
        dialogueBox = GameObject.Find("Text Box");
        mainMenu = GameObject.Find("Main Menu");
        escMenu = GameObject.Find("Escape Menu");
        confirmationWindow = GameObject.Find("Confirmation Window");
        errorWindow = GameObject.Find("Error Window");

        if (escMenu != null) {
            escMenu.SetActive(false);
        }

        if (confirmationWindow != null) {
            confirmationWindow.SetActive(false);
        }

        if (errorWindow != null) {
            errorWindow.SetActive(false);
        }

        RubyWalk.canMove = true;
    }

    void FixedUpdate() {
        CheckEsc();
    }

    public void ConfirmNewLevel(int level) {
        if (PlayerPrefs.GetInt("Level") > 0 && PlayerPrefs.GetInt("Level") < maxLevel) {
            mainMenu.SetActive(false);
            confirmationWindow.SetActive(true);
        }
        else {
            LoadLevel(level);
        }
    }

    public void LoadNextLevel() {
        int currentLevel = PlayerPrefs.GetInt("Level");

        if (currentLevel == 0 || currentLevel + 1 > maxLevel) {
            mainMenu.SetActive(false);
            errorWindow.SetActive(true);
        }

        else if (currentLevel + 1 <= maxLevel) {
			LoadLevel(currentLevel + 1);
		}
    }

    public void ExitApplication() {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void Resume() {
        escMenu.SetActive(!escMenu.activeSelf);

        if (escMenu.activeSelf) {
            RubyWalk.canMove = false;
            if (dialogueBox.activeSelf) {
                wasOn = true;
                dialogueBox.SetActive(false);
            }
        }
        else {
            RubyWalk.canMove = true;
            if (!dialogueBox.activeSelf && wasOn == true) {
                wasOn = false;
                dialogueBox.SetActive(true);
            }
        }
    }

    public void Restart(int level) {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);

        LoadLevel(1);
    }

    public void Cancel() {
        confirmationWindow.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OK() {
        errorWindow.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void LoadLevel(int level) {
        if(level == 1){
            PlayerPrefs.SetInt("Health", 4);
        }
        SceneManager.LoadScene(level);
    }


    private void CheckEsc() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            escMenu.SetActive(!escMenu.activeSelf);

            if (escMenu.activeSelf)
            {
                RubyWalk.canMove = false;
                if (dialogueBox.activeSelf)
                {
                    wasOn = true;
                    dialogueBox.SetActive(false);
                }
            }
            else
            {
                RubyWalk.canMove = true;
                if (!dialogueBox.activeSelf && wasOn == true)
                {
                    wasOn = false;
                    dialogueBox.SetActive(true);
                }
            }
        }
    }
}
