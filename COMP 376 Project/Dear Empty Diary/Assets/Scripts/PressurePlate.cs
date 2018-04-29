using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {

    [Tooltip("The object that is connected to this switch.")]
    public GameObject attachedObject;

    public bool isActivated;

    // When Pressure is applied to plate
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PushableBox") {
            AudioSource source = this.gameObject.GetComponent<AudioSource>();
            source.PlayOneShot(source.clip, 1);
            if (attachedObject.tag == "Left Door" || attachedObject.tag == "Right Door" || attachedObject.tag == "Top Door" || attachedObject.tag == "Bottom Door") {
                attachedObject.GetComponent<DoorManager>().toggleDoor();
                isActivated = !isActivated;
            }

            //// Disable the box collider on this lever (it was used already)
            //this.GetComponent<BoxCollider2D>().enabled = false;
            //// Swap the door's sprite to the open one
            //door.GetComponent<SpriteRenderer>().sprite = openDoor;
            //// And finally enable its polygon collider
            //door.GetComponent<PolygonCollider2D>().enabled = true;

            // Trigger Dialog
            // this.GetComponent<TextScript>().TriggerDialogue();

            if(attachedObject.tag == "Level2Room4Puzzle") {
                attachedObject.GetComponent<Level2Room4Puzzle>().increaseTrapCount();
                isActivated = !isActivated;
            }

            // Level 3: Room 3 Door Toggle
            if (attachedObject.tag == "Level3Room3Puzzle") {
                attachedObject.GetComponent<Level3Room3Puzzle>().toggleDoors();
            }

            // Level 3: Room 6 Puzzle
            if (attachedObject.tag == "Level3Room6Puzzle") {
                attachedObject.GetComponent<Level3Room6Puzzle>().incrementPressurePlate();
            }


        }

        // Level 3: Room 6 Puzzle (Puzzle Activation Trigger)
        if (col.gameObject.tag == "Ruby") {
            if (attachedObject.tag == "Level3Room6Puzzle") { 
                attachedObject.GetComponent<Level3Room6Puzzle>().activateRoom();
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    // When Pressure is released from plate
    void OnTriggerExit2D(Collider2D col) {

        if (col.gameObject.tag == "PushableBox") {
            if (attachedObject.tag == "Left Door" || attachedObject.tag == "Right Door" || attachedObject.tag == "Top Door" || attachedObject.tag == "Bottom Door") {
                attachedObject.GetComponent<DoorManager>().toggleDoor();
                isActivated = !isActivated;
            }

            // Level 3: Room 3 Door Toggle
            if (attachedObject.tag == "Level3Room3Puzzle") {
                attachedObject.GetComponent<Level3Room3Puzzle>().toggleDoors();
            }

            // Level 3: Room 6 Puzzle
            if (attachedObject.tag == "Level3Room6Puzzle") {
                attachedObject.GetComponent<Level3Room6Puzzle>().decrementPressurePlate();
            }
        }
    }
}
