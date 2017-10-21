using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = this.GetComponent<Transform>(); //pega o componente de posição do objeto
	}
	
	// Update is called once per frame
	void Update () {
		float trans = Input.GetAxis("Vertical") * 2;	//w e s
		float rot = Input.GetAxis("Horizontal") * 2;    //a e d
		player.Translate(Time.deltaTime*rot,0,Time.deltaTime*trans); //aplica o movimento
	}
}
