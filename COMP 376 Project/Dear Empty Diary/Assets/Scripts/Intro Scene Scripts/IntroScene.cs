using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroScene : MonoBehaviour {

    #region IntroScene Description
    /*
     * The IntroScene object is attached to the trigger positioned where Ruby stands.
     * It then processes writing the text.
    */
    #endregion

    #region Variables
    [Tooltip("The text we wish to print out.")]
    public string[] dialogueText; // The dialogue text we'll be sending over, line by line, to the IntroSceneManager.
    [Tooltip("Boolean flag to determine if we've already printed out the text before. (To not reprint.)")]
    public bool alreadyPlayed = false;
    [SerializeField]
    private GameObject textManager; // The TextManager object - if we don't assign it, automatically find it.
    #endregion

    #region IntroScene functions
    void Start() {
        // Verify that a TextManager was attached, else attach it (find it)
        if (!this.textManager) {
            this.textManager = GameObject.Find("TextManager");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!alreadyPlayed && col.gameObject.tag == "Ruby")
        {
            TriggerDialogue();
        }
    }

    private void TriggerDialogue() {
        textManager.GetComponent<IntroSceneManager>().WriteText(dialogueText);
        alreadyPlayed = true;
    }
    #endregion
}