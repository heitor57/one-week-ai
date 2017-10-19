using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaNoite : MonoBehaviour {
	public float speed;
	void Update () {
		float rotation = 0.72f * Time.deltaTime; 
		transform.Rotate (rotation, 0,  0);
	}
}
