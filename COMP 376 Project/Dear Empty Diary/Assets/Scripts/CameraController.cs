using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    const float HORIZONTAL_ROOM_SIZE_PAN = 16.44f;
    const float VERTICAL_ROOM_SIZE_PAN   = 10.2279f;

    public void MoveCamera(string doorName) {

        // Move Camera to the Right
        if (doorName == "Right Door") {
            Vector3 newPos = this.transform.position;
            newPos.x += HORIZONTAL_ROOM_SIZE_PAN;
            this.transform.position = newPos;
        }

        // Move Camera to the Left
        else if (doorName == "Left Door") {
            Vector3 newPos = this.transform.position;
            newPos.x -= HORIZONTAL_ROOM_SIZE_PAN;
            this.transform.position = newPos;
        }

        // Move Camera to the Top
        else if (doorName == "Top Door")
        {
            Vector3 newPos = this.transform.position;
            newPos.y += VERTICAL_ROOM_SIZE_PAN;
            this.transform.position = newPos;
        }

        // Move Camera to the Bottom
        else if (doorName == "Bottom Door")
        {
            Vector3 newPos = this.transform.position;
            newPos.y -= VERTICAL_ROOM_SIZE_PAN;
            this.transform.position = newPos;
        }

    }
}
