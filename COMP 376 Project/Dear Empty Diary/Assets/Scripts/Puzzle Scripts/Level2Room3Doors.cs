using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Room3Doors : MonoBehaviour {

    // Checks for open doors
    public GameObject doorOne;
    public GameObject doorTwo;

    // Enable door
    public GameObject progressionDoor;

    private bool isActivated = false;

    // Update is called once per frame
    void Update () {

        if (!isActivated) {
            // Check if both doors are open
            if (doorOne.GetComponent<DoorManager>().isOpen && doorTwo.GetComponent<DoorManager>().isOpen) {
                isActivated = !isActivated;
                progressionDoor.GetComponent<DoorManager>().toggleDoor();
            }
        }

    }
}
