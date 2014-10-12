using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class InputManager : MonoBehaviour {

	public GameObject Inner;
	public GameObject Outer;
	public GameObject Counter;
	public GameObject Sphere;
	public GameObject HotmapTime;

	private bool hotmapDisplayed;

	void Start () {
		hotmapDisplayed = false;
	}

	void Update () {

		if(hotmapDisplayed){
			foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
				if(gameObj.name == "Hotmap(Clone)"){
					MeshRenderer m =gameObj.GetComponent<MeshRenderer>();
					m.enabled = true;
				}

				if(gameObj.name == "HotmapTime(Clone)"){
					Vector3 temp = gameObj.transform.rotation.eulerAngles;
					temp.x = 90.0f;
					gameObj.transform.rotation = Quaternion.Euler(temp);

					gameObj.GetComponent<MeshRenderer>().enabled = true;
				}
			}
		}

		//Inner Frame
		if(Input.GetKey(KeyCode.A)){
			Inner.transform.Rotate(Vector3.up, Time.deltaTime*100);
		}

		if(Input.GetKey(KeyCode.D)){
			Inner.transform.Rotate(Vector3.up, -Time.deltaTime*100);
		}

		//Outer Frame
		if(Input.GetKey(KeyCode.J)){
			Outer.transform.Rotate(Vector3.up, Time.deltaTime*100);
		}
		
		if(Input.GetKey(KeyCode.L)){
			Outer.transform.Rotate(Vector3.up, -Time.deltaTime*100);
		}

		//GameOver Controls
		if(Counter.GetComponent<Counter>().isDeath){

			//Restart 
			if (Input.GetKeyDown(KeyCode.Return)){
				Application.LoadLevel(0);
			}

			//Show Hotmap
			if (Input.GetKeyDown(KeyCode.Space) && !hotmapDisplayed){

				hotmapDisplayed = true;

				if(File.Exists("hotmap.txt")){
					StreamReader sr = File.OpenText("hotmap.txt");
					string line;

					while((line = sr.ReadLine()) != null){
						Regex regex = new Regex(","); 
						string[] subValues = regex.Split(line);

						//Times
						HotmapTime.GetComponent<TextMesh>().text = subValues[2];

						//Instantiate deathPoints
						Vector3 spherePos = new Vector3(float.Parse(subValues[0]), 0, float.Parse(subValues[1]));
						Instantiate(Sphere, spherePos,  Quaternion.identity);

						//Instantiate Times
						Vector3 timePos = new Vector3(float.Parse(subValues[0])+0.8f, 0, float.Parse(subValues[1]));
						Instantiate(HotmapTime, timePos,  Quaternion.identity);
					}

					sr.Close();

				} else {
					Debug.Log("Could not Open the file for reading.");
				}
			}
		}
	}
}
