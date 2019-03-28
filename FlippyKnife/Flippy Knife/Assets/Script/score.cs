using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour {

	public static int PinCount;

	public TextMesh text;

	void Start(){
	
		PinCount = 0;
	}

	void Update(){
	
		text.text = PinCount.ToString ();
	}
}