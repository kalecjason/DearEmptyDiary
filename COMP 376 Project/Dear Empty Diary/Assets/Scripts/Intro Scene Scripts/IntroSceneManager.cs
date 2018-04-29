using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour {
    #region IntroSceneManager Description
    /*
     * The Intro Scene Manager will handle outputting Text to the dialogue box area in the Intro Scene.
     * It works the same way as the TextManager but it takes into account the number of dialogues passed to change the background.
     * To use: 
     *      1. Ensure you have an instance of IntroSceneManager in the scene.
     *      2. Add a IntroScene script to any triggerable object or event to trigger text. Check the 'PlayText' if you wish to play text for that object.
     *      3. The Intro Scene script will communicate with the IntroSceneManager and pass the assigned text.
     *      4. After communicating, it will output the text you wrote in the IntroScene script's field to the dialogue box.
     *      5. Finally, button presses will progress with the dialogue in-order of the array.
     *      6. After a certain of presses, the background will change.
    */
    #endregion

    #region Variables
    [Tooltip("The subsequent background images to change to.")]
    public Sprite[] backgrounds;
    [SerializeField]
    private GameObject textBox; // The dialogue box panel (called 'Text Box')
    [SerializeField]
    private GameObject dialogueBox; // The text box attached to the panel (called 'Dialogue')
    [SerializeField]
    private GameObject nameBox; // The field where the name of the speaking person goes
    [SerializeField]
    private GameObject backgroundObject; // The background we wish to change
    private Text dialogueText; // The text associated to the dialogue box
    private Text nameText; // The text representing the name of the person speaking
    private string[] text; // The text passed to the TextManager to use
    private int textLength; // The number of entries in the text[] array
    private int counter = 0; // By default, start at entry 0 in the text array
    private int currentLevel = 1; // By default, the intro scene is level 1
    #endregion

    #region IntroSceneManager Functions
    void Start() {
        // Fetches the dialogue box's text component
        this.dialogueText = dialogueBox.GetComponent<Text>();
        this.nameText = nameBox.GetComponent<Text>();

        ToggleTextBox();
        backgroundObject  = GameObject.Find("r1");
    }

    void FixedUpdate() {
        if (textBox.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ProgressThroughDialogue();
            }
        }
    }

    // Function that takes in text and outputs it to the dialogue box
    public void WriteText(string[] script) {
        // Set the counter back to 0, text's max length, the text array, and print out the first entry
        this.counter = 0;
        // If we haven't enabled the dialogue box before, we enable it now
        if (!this.textBox.activeSelf) {
            ToggleTextBox();
            // NOTE: This makes it so that text can be overwritten by another incoming text.
        }
        this.textLength = script.Length - 1; // -1 because of array uses (i.e. an array of 2 elements returns length of 2, but we use element 0 and 1)
        this.nameText.text = script[0].Split(':')[0];
        this.text = script;
        this.dialogueText.text = this.text[this.counter].Split(':')[1];
    }

    // Toggles the text box (on if off, off if on)
    private void ToggleTextBox() {
        this.textBox.SetActive(!this.textBox.activeSelf);
    }

    // Progresses automatically to the next dialogue in the array (if it exists)
    private void ProgressThroughDialogue() {
        // If there is still some text to output after pressing OK
        if (counter < textLength) {
            // Increment our counter, and then print out the text
            this.counter++;
            this.nameText.text = this.text[this.counter].Split(':')[0];
            this.dialogueText.text = this.text[this.counter].Split(':')[1];

            // Change background sprite at certain amount of button presses
            if (counter == 2)
            {
                backgroundObject.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
            }
            if (counter == 4)
            {
                backgroundObject.GetComponent<SpriteRenderer>().sprite = backgrounds[2];
            }
        }
        else {
            // Otherwise, toggle the text box on pressing OK
            ToggleTextBox();
            // Since it's a special case, load next level
            ProceedToNextLevel();
        }
    }

    private void ProceedToNextLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        
        SceneManager.LoadScene(currentLevel + 1);
    }
    #endregion
}