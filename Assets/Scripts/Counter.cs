using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

	public GUISkin skin;
	public float gameTime;
	public bool isDeath;

	void Start() {
		isDeath = false;
	}

	void Update(){

		if(!isDeath){
			gameTime = Time.timeSinceLevelLoad;
		}
	}

	void OnGUI() {
		GUI.skin = skin;
		GUI.Label(new Rect(0,0,40,40), gameTime.ToString("F2"));
	}
}
