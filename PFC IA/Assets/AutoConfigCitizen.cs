using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Minds;
using RAIN.Core;
using RAIN.BehaviorTrees;
using RAIN.Perception.Sensors;
using RAIN.Navigation.Waypoints;
public class AutoConfigCitizen : AutoConfigNPC {

	new public void ConfigStart(){
		base.ConfigStart(); 
		//Setando o comportamento (Behavior Tree)
		AIRig ai = GetComponent<AIRig> ();
		TextAsset BehaviorTree;
		BehaviorTree = Resources.Load<TextAsset>("CitizenBehavior");
		((BasicMind)ai.AI.Mind).SetBehavior(BehaviorTree, new Dictionary<string, TextAsset> ());
		// Status do npc
		//NPCStatus npcstatus = ai.AI.GetCustomElement<NPCStatus> ();
	}

	void Start(){
		ConfigStart();
	}
}
