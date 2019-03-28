using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Knife : MonoBehaviour {

	public Rigidbody rb;
	public Vector2 startswipe;
	public Vector2 endswipe;
	public float force = 5f;
	public float torque = 25f;
	private float TimeWhenWeStartFlying;
	public Text highScore;


	private Shake shake;


	// Use this for initialization
	void Start () {
		highScore.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
		shake = GameObject.FindGameObjectWithTag ("ScreenShake").GetComponent<Shake>();
	}
	
	// Update is called once per frame
	void Update () {

		if (!rb.isKinematic) {
			return;
		}

		if (Input.GetMouseButtonDown (0)) {

			startswipe = Camera.main.ScreenToViewportPoint (Input.mousePosition);

		}

		if (Input.GetMouseButtonUp (0)) {
		
			endswipe = Camera.main.ScreenToViewportPoint (Input.mousePosition);
			swipe ();
		}
	}
		void swipe(){
			
		    rb.isKinematic = false;
		    TimeWhenWeStartFlying = Time.time;
			Vector2 swipe;
			swipe = endswipe - startswipe;
			rb.AddForce(swipe * force, ForceMode.Impulse);
			rb.AddTorque(0f, 0f, torque, ForceMode.Impulse);
			FindObjectOfType<AudioManager>().Play("Jump");

		}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Block") {

			shake.CamShake ();
			rb.isKinematic = true;
			FindObjectOfType<AudioManager>().Play("Knife");
			score.PinCount++;
			if (score.PinCount > PlayerPrefs.GetInt ("HighScore", 0)) {
			
				PlayerPrefs.SetInt ("HighScore", score.PinCount);
				highScore.text = score.PinCount.ToString ();
			}
		} else {

			Restart ();
		
		}
	}

	void OnCollisionEnter(){
	
		float TimeInAir = Time.time - TimeWhenWeStartFlying;
		if (!rb.isKinematic && TimeInAir >= .05f) {
			Restart ();
		}
	
	}
	void Restart(){

		FindObjectOfType<AudioManager>().Play("Drop");
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	
	}
}