using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Room4Puzzle : MonoBehaviour {

    // Set the number of pressure plate pressed to activate trap
    public int numOfPressurePlates;

    // Door to activate once trap is triggered
    public GameObject door;
    public GameObject pressurePlateOne;
    public GameObject pressurePlateTwo;

    // Triggered Trap count
    private int numOfTriggeredTraps;

    // Gas Sprite Timer
    private float timer = 0.0f;

    bool isDamaged = false;


	// Use this for initialization
	void Start () {
        numOfTriggeredTraps = 0;

    }
	
	// Update is called once per frame
	void Update () {

        // Activate trap once all pressure plates are triggered
		if(numOfTriggeredTraps == numOfPressurePlates) {

            if (!isDamaged) {
                // ========= Insert Code to Damage Ruby ========= // 

                // Enable Cloud of Gas

                isDamaged = !isDamaged;
            }

            // Timer has elapsed
            else if (timer > 0.0f) {
                // Disable Cloud of Gas

                // Re-open Door
                door.GetComponent<DoorManager>().toggleDoor();
                pressurePlateOne.GetComponent<BoxCollider2D>().enabled = false;
                pressurePlateTwo.GetComponent<BoxCollider2D>().enabled = false;
                numOfTriggeredTraps = 0;
            }

            else {
                timer += Time.deltaTime;
            }
        }
	}


    // Update the count once a trigger has been set
    public void increaseTrapCount() {
        numOfTriggeredTraps++;
    }
}
