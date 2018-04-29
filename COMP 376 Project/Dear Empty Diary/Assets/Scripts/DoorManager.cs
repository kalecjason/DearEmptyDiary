using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour {

    [Tooltip("The sprite that a closed door has.")]
    public Sprite closedDoorSprite;

    [Tooltip("The sprite that an open door has.")]
    public Sprite openDoorSprite;

    public bool isOpen = false;

    // Toggles the Door mode on call
    public void toggleDoor() {
        if (isOpen) {
            CloseDoor();
        }
        else {
            OpenDoor();
        }
    }

    // Disable the collider and change sprite to Open Door
   private void OpenDoor() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = openDoorSprite;
        isOpen = true;
    }

    // Enable the collider and change sprite to Close Door
    private void CloseDoor() {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().sprite = closedDoorSprite;
        isOpen = false;
    }
}
