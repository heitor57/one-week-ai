﻿using System.Collections;
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
public class AutoConfigSoldier : AutoConfigNPC {

	new public void ConfigStart(){
		base.ConfigStart(); 
		//Setando o comportamento (Behavior Tree)
		AIRig ai = GetComponent<AIRig> ();
		TextAsset BehaviorTree;
		BehaviorTree = Resources.Load<TextAsset>("SoldierBehavior");
		((BasicMind)ai.AI.Mind).SetBehavior(BehaviorTree, new Dictionary<string, TextAsset> ());
		// Status do npc
		//NPCStatus npcstatus = ai.AI.GetCustomElement<NPCStatus> ();

		// slot para formação
		SlotAspect x = new SlotAspect("slot");
		x.Slot = new Vector3 (999,0, 999);
		x.PositionOffset = new Vector3(0f,1f,0f);
		x.VisualSize = 1f;

		ai.AI.Body.GetComponent<EntityRig> ().Entity.AddAspect (x);
	}

	void Start(){
		ConfigStart();
	}

}