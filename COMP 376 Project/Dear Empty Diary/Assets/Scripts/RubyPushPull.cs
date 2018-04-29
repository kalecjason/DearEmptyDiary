using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyPushPull : MonoBehaviour {
    [Tooltip("Distance of the ray cast detecting that Ruby is within range of the box")]
    public float distance;
    [Tooltip("Layer mask to selectively filter game objects when ray casting")]
    public LayerMask boxMask;

    private GameObject box;
    private RaycastHit2D hit;
    private bool isLeft, isRight, isUp, isDown;

    // Update is called once per frame
    void Update() {
        Physics2D.queriesStartInColliders = false;
        
        // Push box in input direction
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);
            isRight = true;
            isLeft = false;
            isUp = false;
            isDown = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            hit = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, distance, boxMask);
            isRight = false;
            isLeft = true;
            isUp = false;
            isDown = false;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            hit = Physics2D.Raycast(transform.position, Vector2.up * transform.localScale.x, distance, boxMask);
            isRight = false;
            isLeft = false;
            isUp = true;
            isDown = false;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            hit = Physics2D.Raycast(transform.position, Vector2.down * transform.localScale.x, distance, boxMask);
            isRight = false;
            isLeft = false;
            isUp = false;
            isDown = true;
        }

        // Pull box in input direction
        if (hit.collider != null && hit.collider.gameObject.tag == "PushableBox" && Input.GetKeyDown(KeyCode.P)) {
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
            if (isRight)
                box.GetComponent<FixedJoint2D>().anchor = new Vector3(-2.0f, 0, 0);
            else if (isLeft)
                box.GetComponent<FixedJoint2D>().anchor = new Vector3(2.0f, 0, 0);
            else if (isUp)
                box.GetComponent<FixedJoint2D>().anchor = new Vector3(0, -0.5f, 0);
            else if (isDown)
                box.GetComponent<FixedJoint2D>().anchor = new Vector3(0, 3.0f, 0);

            if (isRight || isLeft) {
                isUp = false;
                isDown = false;
                box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
            else if (isUp || isDown) {
                isRight = false;
                isLeft = false;
                box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }


        }
        else if (Input.GetKeyUp(KeyCode.P)) {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Draw the ray cast
    void OnDrawGizmos() {
        Gizmos.color = Color.green;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);

        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left * transform.localScale.x * distance);

        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.up * transform.localScale.x * distance);

        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.down * transform.localScale.x * distance);
    }
}