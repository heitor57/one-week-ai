using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Perception.Sensors;
using RAIN.Core;
using RAIN.Minds;
public class AutoConfigPlayer : AutoConfig {

	new public void ConfigStart(){
		base.ConfigStart ();
		gameObject.AddComponent<PlayerAttackCreatorAspect> ();
		Component.Destroy( GetComponent<AIRig> ()); // so por enquanto
	}

	void Start () {
		ConfigStart ();
	}

}
