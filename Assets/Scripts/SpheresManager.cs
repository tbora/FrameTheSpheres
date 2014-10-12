using UnityEngine;
using System.Collections;

public class SpheresManager : MonoBehaviour {
	
	public GameObject Counter;
	public GameObject Sphere;
	int tenCounter = 10;
	
	void Update () {

		if (Counter.GetComponent<Counter>().gameTime > tenCounter){
			Instantiate(Sphere, new Vector3(0,0,0), transform.rotation);
			tenCounter += 10;
		}
		
	}
}

