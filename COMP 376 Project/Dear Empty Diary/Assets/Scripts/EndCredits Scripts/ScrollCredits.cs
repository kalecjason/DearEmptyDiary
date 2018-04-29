using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollCredits : MonoBehaviour {

    private GameObject credits;
    private float scrollSpeed = 2.0f;
    private float elapsedTime, endTime = 25.0f;

	// Use this for initialization
	void Start () {
        credits = GameObject.Find("Credits");
	}
	
	// FixedUpdate is called once per frame
	void FixedUpdate () {
        Scroll();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= endTime) {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene(0);
        }
	}

    private void Scroll() {
        for (int i = 0; i < credits.transform.childCount; i++) {
            credits.transform.GetChild(i).transform.Translate(new Vector2(0.0f, 1.0f) * scrollSpeed);
        }
    }
}
