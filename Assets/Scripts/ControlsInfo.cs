using UnityEngine;
using System.Collections;

public class ControlsInfo : MonoBehaviour {

	public GUISkin skinControls;
	private float gameTime;

	void OnGUI() {
		GUI.skin = skinControls;
		GUI.Label(new Rect(0,0,80,80), "SPHERES:");
	}

}
