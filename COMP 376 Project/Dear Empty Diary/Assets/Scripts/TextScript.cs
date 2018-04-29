using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{

    #region TextScript Description
    /*
     * The TextScript objected is attached to every interactable object that would trigger text (or door if you use it that way).
     * When an object is interacted with (or would trigger text, i.e. by colliding with it), simply call a function here that communicates with the TextManager.
     * It then processes writing the text.
    */
    #endregion

    #region Variables
    [Tooltip("The text we wish to print out.")]
    public string[] dialogueText; // The dialogue text we'll be sending over, line by line, to the TextManager.
    [SerializeField]
    private GameObject textManager; // The Text Manager object - if we don't assign it, automatically find it.
    [Tooltip("Boolean to determine whether or not, on collision/trigger, we output text.")]
    public bool playText;
    [Tooltip("Boolean flag to determine if we've already printed out the text before. (To not reprint.)")]
    public bool alreadyPlayed = false;
    [Tooltip("Boolean flag to signal if the collided trigger for the text is a Diary Page. (Special Interaction)")]
    public bool isPage = false;
    #endregion

    #region TextScript functions
    void Start()
    {
        // Verify that a TextManager was attached, else attach it (find it)
        if (!this.textManager)
        {
            this.textManager = GameObject.Find("TextManager");
        }
    }

    // On trigger OR collision, handle checking if we play text at all.
    void OnTriggerEnter2D(Collider2D col)
    {
        // Case when we walk over a Diary page and pick it up
        if (isPage && col.gameObject.tag == "Ruby")
        {
            // Trigger the dialogue as usual
            TextManager.isDiaryPage = true;
            GameObject.FindGameObjectWithTag("Page").GetComponent<SpriteRenderer>().sprite = null;
            TriggerDialogue();
        }

        // Case where we want to play the dialogue, it was not played before, and the collided object is
        // either Ruby or her Melee and we did not interact with a lever (i.e. we passed a door)
        // Added the case where Ruby should not trigger the dialogue when stepping on the pressure plate
        if (playText && !alreadyPlayed && (col.gameObject.tag == "Ruby" || col.gameObject.tag == "Melee") && this.gameObject.tag != "Levers" && this.gameObject.tag != "PressurePlate")
        {
            TriggerDialogue();
        }

        // Similar case, however only if the collided object is melee (Ruby's M) or a projectile (rock)
        // So for the levers from range (walking over them won't trigger it)
        if (playText && !alreadyPlayed && (col.gameObject.tag == "Melee" || col.gameObject.tag == "Rock") && this.gameObject.tag == "Levers")
        {
            TriggerDialogue();
        }

        // Another similar case, but when the collided object is a box
        // That is, for pressure plates
        if (playText && !alreadyPlayed && (col.gameObject.tag == "PushableBox")) {
            TriggerDialogue();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If we collide and it was either Ruby or her Melee, we trigger dialogue
        if (playText && !alreadyPlayed && (col.gameObject.tag == "Ruby" || col.gameObject.name == "Melee"))
        {
            TriggerDialogue();
        }
    }
    // -------------------------

    // Function to communicate with the TextManager by passing to it the dialogue text array.
    // Every other functionality is handled through the button in the canvas.
    public void TriggerDialogue()
    {
        textManager.GetComponent<TextManager>().WriteText(dialogueText);
        alreadyPlayed = true;
    }
    #endregion
}
