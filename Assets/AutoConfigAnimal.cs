using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Minds;
using RAIN.Core;
using RAIN.BehaviorTrees;
using RAIN.Perception.Sensors;
using RAIN.Animation;
using RAIN.Navigation.Waypoints;
using RAIN.Entities.Aspects;
using RAIN.Entities;
public class AutoConfigAnimal : AutoConfigNPC {

	new public void ConfigStart(){
		base.ConfigStart(); 
		AIRig ai = GetComponent<AIRig> ();
		// slot para formação
		SlotAspect x = new SlotAspect("slot");
		x.Slot = new Vector3 (999,0, 999);
		x.Head = null;
		x.PositionOffset = new Vector3(0f,1f,0f);
		x.VisualSize = 1f;
		ai.AI.Body.GetComponent<EntityRig> ().Entity.AddAspect (x);
	}

	void Start(){
		ConfigStart();
	}

}
