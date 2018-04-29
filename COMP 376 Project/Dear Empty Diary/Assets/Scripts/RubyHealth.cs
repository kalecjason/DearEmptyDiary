using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyHealth : MonoBehaviour
{

    public int health;

    public Sprite[] heartSprites;

    public GameObject healthUI;

    private bool isDamaged;
    private float timer = 2.0f;

    // Use this for initialization
    void Start()
    {
        health = PlayerPrefs.GetInt("Health");
        updateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        health = PlayerPrefs.GetInt("Health");
        
        if (isDamaged)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            TimeEnd();
        }
    }

    void takeDamage()
    {
        if(!isDamaged)
        {
            health--;   
            updateHealth();
            isDamaged = true;
        }
    }

    void updateHealth()
    {
        Image healthImage = healthUI.GetComponent<Image>();
        switch (health)
        {
            case 0:
                healthImage.sprite = heartSprites[0];
                PlayerPrefs.SetInt("Health", 4);
                SceneManager.LoadScene(0);
                break;
            case 1:
                healthImage.sprite = heartSprites[1];
                PlayerPrefs.SetInt("Health", 1);
                break;
            case 2:
                healthImage.sprite = heartSprites[2];
                PlayerPrefs.SetInt("Health", 2);
                break;
            case 3:
                healthImage.sprite = heartSprites[3];
                PlayerPrefs.SetInt("Health", 3);
                break;
            case 4:
                healthImage.sprite = heartSprites[4];
                PlayerPrefs.SetInt("Health", 4);
                break;
            default:
                healthImage.sprite = heartSprites[0];
                PlayerPrefs.SetInt("Health", 0);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Trap" || col.gameObject.tag == "CannonProjectile")
        {
            takeDamage();
            if (isDamaged)
            {
                Color color = gameObject.GetComponent<SpriteRenderer>().color;
                color.a = 0.1f;
                gameObject.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }

    void TimeEnd()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 1.0f;
        gameObject.GetComponent<SpriteRenderer>().color = color;
        isDamaged = false;
        timer = 2.0f;
    }
}
