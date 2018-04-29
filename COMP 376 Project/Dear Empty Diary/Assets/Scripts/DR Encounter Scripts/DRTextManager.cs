using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DRTextManager : MonoBehaviour {

    #region DRTextManager Description
    /*
     * The Text Manager will handle outputting Text to the dialogue box area. 
     * To use: 
     *      1. Ensure you have an instance of DRTextManager in the scene.
     *      2. Add a DRTextScript to the scene or event to trigger text. Check the 'PlayText' if you wish to play text for that object.
     *      3. The TextScript will communicate with the DRTextManager and pass the assigned text.
     *      4. After communicating, it will output the text you wrote in the DRTextScript's field to the dialogue box.
     *      5. Finally, button presses will progress with the dialogue in-order of the array.
    */
    #endregion

    #region Variables
    [SerializeField]
    private ChoiceCounter choiceCounter; // The choice counter for good/bad responses
    [SerializeField]
    private GameObject textBox; // The dialogue box panel (called 'Text Box')
    [SerializeField]
    private GameObject dialogueBox; // The text box attached to the panel (called 'Dialogue')
    [SerializeField]
    private Text goodBtnText;
    [SerializeField]
    private Text badBtnText;
    [SerializeField]
    private GameObject nameBox; // The field where the name of the speaking person goes
    private Text dialogueText; // The text associated to the dialogue box
    private Text nameText; // The text representing the name of the person speaking
    private string[] text; // The text passed to the TextManager to use
    private int textLength; // The number of entries in the text[] array
    private int counter = 0; // By default, start at entry 0 in the text array
    private bool canPressEnter = false; // For when Dark Ruby explains things, enable pressing Enter
    private bool processedEnd = false; // Flag to check if we've processed the end dialogue/scene or not
    [Tooltip("Boolean flag to check if we can now load the next scene. (Diary dialogue acquisition terminated.)")]
    public static bool isDiaryPage = false;
    #endregion

    #region TextManager Functions
    void Start()
    {
        // Fetches the dialogue box's text component
        this.dialogueText = dialogueBox.GetComponent<Text>();
        this.nameText = nameBox.GetComponent<Text>();
        if (!goodBtnText)
        {
            goodBtnText = GameObject.Find("GoodText").GetComponent<Text>();
        }
        if (!badBtnText)
        {
            badBtnText = GameObject.Find("BadText").GetComponent<Text>();
        }
        if (!choiceCounter)
        {
            choiceCounter = GameObject.Find("ChoiceCounter").GetComponent<ChoiceCounter>();
        }
        ToggleTextBox();
    }

    void FixedUpdate()
    {
        if (canPressEnter)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ProgressThroughDialogueAutomatically();
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
        if (!processedEnd)
        {
            this.goodBtnText.text = this.text[this.counter].Split(':')[2];
            this.badBtnText.text = this.text[this.counter].Split(':')[3];
        }
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
            if (!processedEnd)
            {
                this.goodBtnText.text = this.text[this.counter].Split(':')[2];
                this.badBtnText.text = this.text[this.counter].Split(':')[3];
            }
        }
        else
        {
            // If we haven't processed the end yet, do it
            if (!processedEnd)
            {
                ProcessEndDialogue();
            }
            else
            {
                // Game is done! Return to main menu.
                LoadEndCredits();
            }
        }
    }

    // Processes the end dialogue
    private void ProcessEndDialogue()
    {
        Debug.Log("ProcessEndDialogue() called");
        // Disable the buttons
        GameObject.Find("GoodBtn").SetActive(false);
        GameObject.Find("BadBtn").SetActive(false);
        processedEnd = true;
        canPressEnter = true;
        // Process for when we have more bad than good choices
        if (choiceCounter.badCounter > choiceCounter.goodCounter)
        {
            string[] endScript = {"Dark Ruby: Oh Ruby... You’ve got so much to learn. About life, about dad, about mom... But more importantly, about yourself.", 
                                     "Dark Ruby: Dad does love you and mom, don’t doubt that. But, things got complicated fast.",
                                 "Dark Ruby: He’s always had problems, and has been constantly fighting off this evil voice in his head. To protect you and mom, he decided to leave to seek help.",
                                 "Ruby: Dad did that? But...why? We were happy...",
                                 "Dark Ruby: That’s something for you to think about. I’m not going to tell you why when you already know. <i>Maybe you should have thought more about your answers.</i>",
                                 "Dark Ruby: Heh. Anyway... I’ve seen enough. I’ll let you go for now. But be warned, if your heart ever gets tainted by darkness again, I’ll be right here waiting to strike again. Always watching you..."};
            this.WriteText(endScript);
        }
        else
        {
            // Process for when we have more good than bad choices
            string[] endScript = { "Dark Ruby: Ruby... I was wrong about you. You do know much more than I thought.",
                                 "Dark Ruby: Dad does love you, and he also loves mom. He never stopped loving either of you.",
                                 "Dark Ruby: There’s... another person inside dad. Some other person in dad’s head that keeps messing with his feelings and thoughts.",
                                 "Dark Ruby: Those pills he keeps taking... Mom said they’d keep the bad dad away. But they couldn’t anymore.",
                                 "Dark Ruby: To protect you and mom, dad decided to leave and seek help. He… left a note for you. It was under your pillow... Here, read it.",
                                 " : <i>\"Dear Ruby, it’s daddy. I’m sorry to have to say goodbye like this, but when I hurt you because I couldn’t control myself, that left me no choice.\"</i>",
                                 " : <i>\"I knew that I couldn’t continue living here. I couldn’t risk hurting you or mom anymore. I don’t want to make you worry, or keep you up at night.\"</i>",
                                 " : <i>\"This... This monster inside my head. I... I’m starting to not be able to control it anymore. It’s scary.\"</i>",
                                 " : <i>\"I’ve decided to go get medical help. There are doctors that can help, but they’re very far from home. I promise I’ll be back. One day, we’ll all be a big happy family again.</i>\"",
                                 " : <i>\"Ruby, you’re such a strong girl. But when I saw you getting sadder by the day, I kept blaming myself. You’ve got to overcome your own darkness, you’re the strongest girl I know.</i>\"",
                                 " : <i>\"Remember, hon, I’ll always love you, and I’ll always be your daddy.</i>\"",
                                 " : <i>\"Take care of mommy for me. I’ll miss you both. Love, Dad.\"</i>",
                                 "Ruby: I... I can’t believe this...",
                                 "Dark Ruby: It’s true, Ruby. And... I have to apologize, too. I didn’t mean to trap you here. But, this was the only way to get you to overcome your own darkness.",
                                 "Ruby: I... I understand. Ruby, I mean, other me... Thank you. And, I also forgive you for this.",
                                 "Dark Ruby: Heh. You’re really something, aren’t you...",
                                 "Diary: Good job other Ruby. I knew we could do this!"};
            this.WriteText(endScript);
        }
    }

    // Load the main menu at the end
    private void LoadEndCredits()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Saved Game");

        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("You finished the game!");
        SceneManager.LoadScene(currentLevel + 1);
    }
    #endregion
}
