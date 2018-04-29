using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour {

    [Tooltip("The door that is connected to this object's switch.")]
    public GameObject attachedObject;

    public Sprite leverEnabled;
    public Sprite leverDisabled;

    private bool isActivated = false;

    // When Ruby triggers the lever, activate the door (swap sprites)
    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Rock" || col.gameObject.tag == "Melee") {
            AudioSource source = this.gameObject.GetComponent<AudioSource>();
            source.PlayOneShot(source.clip, 1);
            // If the Attached Object is a Door
            if(attachedObject.tag == "Left Door" || attachedObject.tag == "Right Door" || attachedObject.tag == "Top Door" || attachedObject.tag == "Bottom Door") {
                attachedObject.GetComponent<DoorManager>().toggleDoor();
                isActivated = !isActivated;
                flipLever();
            }

            else if(attachedObject.tag == "Cannon") {
                attachedObject.GetComponent<Cannon>().fireCannon();
                isActivated = !isActivated;
                flipLever(); 
            }

            // Dummy Lever / Fake Lever : Do nothing 
            else {
                isActivated = !isActivated;
                flipLever();
            }

            //if (!isActivated) {
            //    // Mark switch as activated
            //    isActivated = true;
            //    // Change Sprite of the door 
            //    door.GetComponent<SpriteRenderer>().sprite = openDoor;
            //    // Enable door's polygon collider
            //    door.GetComponent<PolygonCollider2D>().enabled = true;
            //    // Flip the switch
            //    this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            //    // Trigger Dialog 
            //    // this.GetComponent<TextScript>().TriggerDialogue();
            //}

            //else {
            //    // Mark switch as disactivated
            //    isActivated = false;
            //    // Change Sprite of the Door
            //    door.GetComponent<SpriteRenderer>().sprite = closedDoor;
            //    // Disable door's polygon collider
            //    door.GetComponent<PolygonCollider2D>().enabled = false;
            //    // Flip the switch
            //    this.transform.localScale = new Vector3(-this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
            //    // Trigger Dialog 
            //    // this.GetComponent<TextScript>().TriggerDialogue();
            //}
        }
    }

    

    // Flips the sprite of the Lever
    private void flipLever() {
        if (isActivated) {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = leverEnabled;
        }

        else {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = leverDisabled;
        }
    }
}
