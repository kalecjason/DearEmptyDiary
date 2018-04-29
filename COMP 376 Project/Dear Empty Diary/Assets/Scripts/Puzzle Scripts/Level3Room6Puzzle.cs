using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Room6Puzzle : MonoBehaviour {


    // GameObjects to manipulate
    public GameObject initialDoor;
    public GameObject progressionDoor;

    // Private Booleans 
    private bool isActivated;
    private bool isCompleted;

    // Number of released Pressure Plates
    public int pressedPressurePlates;


    // Starting the Puzzle
    void Start() {
        pressedPressurePlates = 0;
        isActivated = false;
        isCompleted = false;
    }

    // Puzzle Update 
    void Update() {

        // Puzzle Completed (All 4 pressure plates Disabled)
        if(isActivated && !isCompleted && pressedPressurePlates == 0) {
            isCompleted = true;
            isActivated = false;
            initialDoor.GetComponent<DoorManager>().toggleDoor();
            progressionDoor.GetComponent<DoorManager>().toggleDoor();
        }
        
    }

    // Activate the puzzle 
    public void activateRoom() {
        if (!isActivated) {
            initialDoor.GetComponent<DoorManager>().toggleDoor();
            isActivated = true;
        }
    }

    // Decreament pressure plate count
    public void decrementPressurePlate() {
        pressedPressurePlates--;
    }

    // Increment pressure plate count
    public void incrementPressurePlate() {
        pressedPressurePlates++;
    }
}
