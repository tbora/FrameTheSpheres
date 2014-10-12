using UnityEngine;
using System.Collections;
using System;
using System.IO;


public class SphereMovement : MonoBehaviour {

	public GameObject Counter;
	public GameObject GameOver;

	private float speed = 5f;
	private Vector3 velocity;
	private Vector3 lastPos;
	private Vector3 constSpeed;

	void Start () {
		GameOver.SetActive(false);

		//Start Velocity
		constSpeed = new Vector3(2, 0, 4);
		rigidbody.velocity = constSpeed;
	}


	void Update () {
		Vector3 curPos = transform.position;
		velocity = curPos - lastPos;
		lastPos = curPos;
	}

	void LateUpdate () {
		Vector3 cVel = rigidbody.velocity;
		Vector3 tVel = cVel.normalized * 5.0f;
		rigidbody.velocity = Vector3.Lerp (cVel, tVel, Time.deltaTime * 1.0f);
	}


	void OnCollisionEnter(Collision col) {

		//Normal
		Vector3 N = col.contacts[0].normal;

		//Direction
		Vector3 D = velocity.normalized;

		//Reflection
		Vector3 R = Vector3.Reflect(D,N).normalized;

		//Apply
		rigidbody.velocity = new Vector3(R.x, 0, R.z) * speed;

		//Death
		if(col.transform.gameObject.name == "Boundary"){
			Counter.GetComponent<Counter>().isDeath = true;

			GameOver.SetActive(true);

			foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[]){
				if(gameObj.name.Contains("Sphere")){
					Destroy(gameObj);
				}
			}

			//Hotmap
			foreach (ContactPoint contact in col.contacts) {
				if(contact.thisCollider == collider) {

					//Data
					Vector3 deathCoordinate = contact.point;
					float deathTime = Counter.GetComponent<Counter>().gameTime;

					//Write
					if(!File.Exists("hotmap.txt")){
						StreamWriter sr = File.CreateText("hotmap.txt");
						sr.WriteLine(deathCoordinate.x+","+deathCoordinate.z+","+deathTime.ToString("F2"));
						sr.Close();
					}
					else {
						StreamWriter sr = File.AppendText("hotmap.txt");
						sr.WriteLine(deathCoordinate.x+","+deathCoordinate.z+","+deathTime.ToString("F2"));
						sr.Close();
					}
				}
			}
		}
	}
}
