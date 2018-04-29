using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRTextScript : MonoBehaviour
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
    private GameObject drTextManager; // The Text Manager object - if we don't assign it, automatically find it.
    [Tooltip("Boolean to determine whether or not, on collision/trigger, we output text.")]
    public bool playText;
    [Tooltip("Boolean flag to determine if we've already printed out the text before. (To not reprint.)")]
    public bool alreadyPlayed = false;
    [Tooltip("Boolean flag to signal if the collided trigger for the text is a Diary Page. (Special Interaction)")]
    public bool isPage = false;
    [Tooltip("Ruby from the Scene.")]
    public GameObject Ruby;
    [Tooltip("Idle frame for Ruby.")]
    public Sprite RubyIdle;
    #endregion

    #region TextScript functions
    void Start()
    {
        if (!Ruby)
        {
            Ruby = GameObject.Find("Ruby");
        }
        // Verify that a TextManager was attached, else attach it (find it)
        if (!this.drTextManager)
        {
            this.drTextManager = GameObject.Find("DRTextManager");
        }
    }

    // On trigger OR collision, handle checking if we play text at all.
    void OnTriggerEnter2D(Collider2D col)
    {
        // Set that it's the DR Encounter (true)
        Ruby.GetComponent<RubyWalk>().isDarkRubyEncounter = true;
        // Disable the animator since we don't want her animated
        Ruby.GetComponent<RubyWalk>().GetRubyAnimator().enabled = false;
        // Set her sprite to the idle one
        Ruby.GetComponent<SpriteRenderer>().sprite = RubyIdle;
        // And disable the text manager (prevents enter presses)
        GameObject.Find("TextManager").SetActive(false);
        // Case where we want to play the dialogue, it was not played before, and the collided object is
        // either Ruby or her Melee and we did not interact with a lever (i.e. we passed a door)
        if (playText && !alreadyPlayed && (col.gameObject.tag == "Ruby" || col.gameObject.tag == "Melee") && this.gameObject.tag != "Levers")
        {
            TriggerDialogue();
        }

        // Similar case, however only if the collided object is melee (Ruby's M) or a projectile (rock)
        // So for the levers from range (walking over them won't trigger it)
        if (playText && !alreadyPlayed && (col.gameObject.tag == "Melee" || col.gameObject.tag == "Rock") && this.gameObject.tag == "Levers")
        {
            TriggerDialogue();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // If we collide and it was either Ruby or her Melee, we trigger dialogue
        // This could be for boxes or the likes later
        
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
        drTextManager.GetComponent<DRTextManager>().WriteText(dialogueText);
        alreadyPlayed = true;
    }
    #endregion
}
