using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AutoConfigHuman : AutoConfigAnimal {
	new public void ConfigStart(){
		base.ConfigStart(); 
		gameObject.AddComponent (typeof(Messages));
	}

	void Start(){
		ConfigStart();
	}


}
