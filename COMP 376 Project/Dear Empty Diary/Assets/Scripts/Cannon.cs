using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    [Tooltip("The prefab for the projectile.")]
    public GameObject projectilePrefab;


    public void fireCannon() {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);
        projectile.tag = "CannonProjectile";
        projectile.GetComponent<CircleCollider2D>().isTrigger = true;
        projectile.GetComponent<Rigidbody2D>().velocity = Vector2.right * 10.0f;
    }
}
