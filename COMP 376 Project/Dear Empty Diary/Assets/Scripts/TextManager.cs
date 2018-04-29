using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    #region TextManager Description
    /*
     * The Text Manager will handle outputting Text to the dialogue box area. 
     * To use: 
     *      1. Ensure you have an instance of TextManager in the scene.
     *      2. Add a TextScript to any triggerable object or event to trigger text. Check the 'PlayText' if you wish to play text for that object.
     *      3. The TextScript will communicate with the TextManager and pass the assigned text.
     *      4. After communicating, it will output the text you wrote in the TextScript's field to the dialogue box.
     *      5. Finally, button presses will progress with the dialogue in-order of the array.
    */
    #endregion

    #region Variables
    [SerializeField]
    private GameObject textBox; // The dialogue box panel (called 'Text Box')
    [SerializeField]
    private GameObject dialogueBox; // The text box attached to the panel (called 'Dialogue')
    [SerializeField]
    private GameObject nameBox; // The field where the name of the speaking person goes
    private Text dialogueText; // The text associated to the dialogue box
    private Text nameText; // The text representing the name of the person speaking
    private string[] text; // The text passed to the TextManager to use
    private int textLength; // The number of entries in the text[] array
    private int counter = 0; // By default, start at entry 0 in the text array
    [Tooltip("Boolean flag to check if we can now load the next scene. (Diary dialogue acquisition terminated.)")]
    public static bool isDiaryPage = false;
    #endregion

    #region TextManager Functions
    void Start()
    {
        // Fetches the dialogue box's text component
        this.dialogueText = dialogueBox.GetComponent<Text>();
        this.nameText = nameBox.GetComponent<Text>();

        // Prevent Level 2 to transition to Level 3 right after the first dialogue
        isDiaryPage = false;

        ToggleTextBox();
    }

    void FixedUpdate()
    {
        // If the box is currently active, we allow enter checking to close dialogue
        if (textBox.activeSelf)
        {
            // Progress normally if it's not a diary page
            if (Input.GetKeyDown(KeyCode.Return) && !isDiaryPage)
            {
                ProgressThroughDialogueAutomatically();
            }
            else if (Input.GetKeyDown(KeyCode.Return) && isDiaryPage)
            {
                ProgressThroughDialogueAsDiaryPage();
            }
        }
    }

    // Function that takes in text and outputs it to the dialogue box
    public void WriteText(string[] script)
    {
        // Set the counter back to 0, text's max length, the text array, and print out the first entry
        this.counter = 0;
        // If we haven't enabled the dialogue box before, we enable it now
        if (!this.textBox.activeSelf)
        {
            ToggleTextBox();
            // NOTE: This makes it so that text can be overwritten by another incoming text.
        }
        this.textLength = script.Length - 1; // -1 because of array uses (i.e. an array of 2 elements returns length of 2, but we use element 0 and 1)
        this.nameText.text = script[0].Split(':')[0];
        this.text = script;
        this.dialogueText.text = this.text[this.counter].Split(':')[1];
    }

    // Toggles the text box (on if off, off if on)
    public void ToggleTextBox()
    {
        this.textBox.SetActive(!this.textBox.activeSelf);
    }

    // Progresses automatically to the next dialogue in the array (if it exists)
    public void ProgressThroughDialogueAutomatically()
    {
        // If there is still some text to output after pressing OK
        if (counter < textLength)
        {
            // Increment our counter, and then print out the text
            this.counter++;
            this.nameText.text = this.text[this.counter].Split(':')[0]; 
            this.dialogueText.text = this.text[this.counter].Split(':')[1];
        }
        else
        {
            // Otherwise, toggle the text box on pressing OK
            ToggleTextBox();
        }
    }

    // Progresses automatically to the next dialogue in the array (if it exists)
    public void ProgressThroughDialogueAsDiaryPage()
    {
        // If there is still some text to output after pressing OK
        if (counter < textLength)
        {
            // Increment our counter, and then print out the text
            this.counter++;
            this.nameText.text = this.text[this.counter].Split(':')[0];
            this.dialogueText.text = this.text[this.counter].Split(':')[1];
        }
        else
        {
            // Otherwise, toggle the text box on pressing OK
            ToggleTextBox();
            // Since it's a special case, load next level
            GameObject.FindObjectOfType<AcquirePage>().AcquireDiary();
        }
    }
    #endregion
}
