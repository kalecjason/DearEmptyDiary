using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyWalk : MonoBehaviour {
    // The direction she's facing
    private Vector2 direction;

    // Animator for characters movement
    private Animator animator;

    // Booleans to switch state of animator
    private bool isMoving, isForward, isBack, isLeft, isRight;
    private float speed = 3.5f;
    // Bool to signal if Ruby hit a trap for the first time or not
    private bool hasHitFirstTrap = false;
    // Bool to signal if Ruby got hit by a CannonProjectile or not
    private bool hasHitFirstCannonProjectile = false;

    [Tooltip("Flag to determine if Ruby can move (for pause menu)")]
    public static bool canMove = true;

    [Tooltip("If it's the DR Encounter, Ruby can't move.")]
    public bool isDarkRubyEncounter = false;

    [Tooltip("The fade panel when crossing rooms.")]
    public GameObject fadeObject;

    void Start() {
        animator = GetComponent<Animator>();
        fadeObject.SetActive(false);

        direction = Vector2.down;
        isForward = true;
    }

    // FixedUpdate is called once per frame
    void FixedUpdate () {
        if (canMove && !isDarkRubyEncounter)
        {
            Movement();
        }

        animator.SetBool("forward", isForward);
        animator.SetBool("back", isBack);
        animator.SetBool("left", isLeft);
        animator.SetBool("right", isRight);
        animator.SetBool("moving", isMoving);
    }

    public Animator GetRubyAnimator() { return this.animator; }

    // Keyboard controls to move Ruby
    void Movement() {
        Vector2 aDir = Vector2.zero;

        // Get direction of movement
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");

        isMoving = xDir != 0 || yDir != 0;

        aDir.x = xDir;
        aDir.y = yDir;

        if (!this.GetComponent<RubyAttacking>().IsAttacking()) {
            if (aDir.x != 0) {
                direction = new Vector2(aDir.x, 0);
                transform.Translate(direction * speed * Time.deltaTime);

                isRight = aDir.x > 0 ? true : false;
                isLeft = !isRight;
                isForward = isBack = false;
            }

            else if (aDir.y != 0) {
                direction = new Vector2(0, aDir.y);
                transform.Translate(new Vector2(0, aDir.y) * speed * Time.deltaTime);

                isForward = aDir.y > 0 ? false : true;
                isBack = !isForward;
                isRight = isLeft = false;
            }
        }
    }

    // Moves Ruby depending on which door she takes
    void OnTriggerEnter2D(Collider2D col) {
        AudioSource source = col.gameObject.GetComponent<AudioSource>();
        // Move Ruby to the Right
        if (col.gameObject.tag == "Right Door")
        {
            GameController.gameCamera.GetComponent<CameraController>().MoveCamera("Right Door");
            this.transform.position = col.gameObject.transform.Find("Spawn Location").transform.position;
            source.PlayOneShot(source.clip, 1);
            StartCoroutine(fade());
        }

        // Move Ruby to the Left
        else if (col.gameObject.tag == "Left Door")
        {
            GameController.gameCamera.GetComponent<CameraController>().MoveCamera("Left Door");
            this.transform.position = col.gameObject.transform.Find("Spawn Location").transform.position;
            source.PlayOneShot(source.clip, 1);
            StartCoroutine(fade());
        }

        // Move Ruby to the Top
        else if (col.gameObject.tag == "Top Door")
        {
            GameController.gameCamera.GetComponent<CameraController>().MoveCamera("Top Door");
            this.transform.position = col.gameObject.transform.Find("Spawn Location").transform.position;
            source.PlayOneShot(source.clip, 1);
            StartCoroutine(fade());
        }

        // Move Ruby to the Bottom
        else if (col.gameObject.tag == "Bottom Door")
        {
            GameController.gameCamera.GetComponent<CameraController>().MoveCamera("Bottom Door");
            this.transform.position = col.gameObject.transform.Find("Spawn Location").transform.position;
            source.PlayOneShot(source.clip, 1);
            StartCoroutine(fade());
        }
        else if (col.gameObject.tag == "Trap" && !hasHitFirstTrap)
        {
            hasHitFirstTrap = true; // No longer do this case after 1st trap collision
            string[] trapDialogue = {"Ruby : Ouch! That hurt!",
                                    "Diary : Watch your step Ruby! These traps hurt!"};
            GameObject.Find("TextManager").GetComponent<TextManager>().WriteText(trapDialogue);
        }
        else if (col.gameObject.tag == "CannonProjectile" && !hasHitFirstCannonProjectile)
        {
            hasHitFirstCannonProjectile = true;
            string[] cannonDialogue = {"Ruby: Ouch! That hurt!",
                                      "Diary: Be careful!"};
            GameObject.Find("TextManager").GetComponent<TextManager>().WriteText(cannonDialogue);
        }
    }

    // Returns the direction Ruby's facing
    public Vector2 GetDirection() {
        return direction;
    }

    public IEnumerator fade()
    {
        fadeObject.SetActive(true);
        Image img = fadeObject.GetComponent<Image>();
        for (float i = 1; i >= 0; i -= (Time.deltaTime / 0.8f))
        {
            img.color = new Color(0, 0, 0, i);
            yield return null;
        }
        fadeObject.SetActive(false);
    }
}
