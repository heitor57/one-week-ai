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
public class AutoConfigTiger: AutoConfigNPC {

	new public void ConfigStart(){
		base.ConfigStart(); 
		//Setando o comportamento (Behavior Tree)
		AIRig ai = GetComponent<AIRig> ();
		TextAsset BehaviorTree;
		BehaviorTree = Resources.Load<TextAsset>("TigerBehavior");
		((BasicMind)ai.AI.Mind).SetBehavior(BehaviorTree, new Dictionary<string, TextAsset> ());
	}

	void Start(){
		ConfigStart();
	}

}
