using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Room3Puzzle : MonoBehaviour {

    // Doors to toggle
    public GameObject doorOne;
    public GameObject doorTwo;

    // Toggles the door
    public void toggleDoors() {
        doorOne.GetComponent<DoorManager>().toggleDoor();
        doorTwo.GetComponent<DoorManager>().toggleDoor();
    }

}
