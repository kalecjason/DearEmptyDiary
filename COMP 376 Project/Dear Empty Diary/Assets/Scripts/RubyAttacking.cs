using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyAttacking : MonoBehaviour {

    [Tooltip("The prefab for the projectile.")]
    public GameObject projectilePrefab;

    private Animator animator;
    private GameObject projectile;
    private Vector2 direction, lastDirection;
    private Vector3 position;
    private bool shooting, hitting;

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        position = this.transform.position;

        direction = this.GetComponent<RubyWalk>().GetDirection();

        if (Input.GetKeyDown(KeyCode.Space)) {
            shooting = true;
            Shoot();
        }

        if (!Input.GetKey(KeyCode.Space)) {
            shooting = false;
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            hitting = true;
        }

        if (!Input.GetKey(KeyCode.M)) {
            hitting = false;
        }

        animator.SetBool("hitting", hitting);
        animator.SetBool("shooting", shooting);
    }

    private void Shoot() {
        if (direction == Vector2.right)
            position.x += 0.5f;
        else if (direction == -Vector2.right)
            position.x -= 0.5f;
        else if (direction == Vector2.up)
            position.y += 0.5f;
        else
            position.y -= 0.7f;

        projectile = GameObject.Instantiate(projectilePrefab, position, Quaternion.identity);

        projectile.GetComponent<Rigidbody2D>().velocity = direction * 10.0f;
    }

    private void ToggleWeaponCollider() {
        GameObject weapon = GameObject.Find("Melee");

        if (weapon.GetComponent<BoxCollider2D>() != null) {
            weapon.GetComponent<BoxCollider2D>().enabled = hitting ? true : false;
        }
    }

    public bool IsAttacking() { return shooting || hitting; }
}
