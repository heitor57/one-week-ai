using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeEquip : MonoBehaviour {
	public GameObject a;
	GameObject b;
	public void cEquip(string name){
		b = Instantiate ((GameObject)Resources.Load(name));
		b.transform.parent = a.transform;


	}
	public void destroi(){
		
		Destroy (b);
	}
}
