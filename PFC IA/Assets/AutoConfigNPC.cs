using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RAIN.Perception.Sensors;
using RAIN.Animation;
using RAIN.Core;
using RAIN.Entities.Aspects;
public abstract class AutoConfigNPC : AutoConfig {
	public GameObject route;
	new public void ConfigStart(){
		base.ConfigStart(); 

		// navmeshagent

		NavMeshAgent navmeshagent = gameObject.AddComponent<NavMeshAgent>();
		navmeshagent.autoBraking = false;
		navmeshagent.angularSpeed = 999;
		navmeshagent.acceleration = 999;
		navmeshagent.baseOffset = 0;


		AIRig ai = GetComponent<AIRig> ();
		//Adicionando sensores
		VisualSensor tempSensor = new VisualSensor ();
		tempSensor.SensorName = "Visual Sensor";
		tempSensor.MountPoint = ((BasicAnimator)ai.AI.Animator).Head.Joint;
		tempSensor.Range = 20f;
		tempSensor.HorizontalAngle = 360;
		tempSensor.VerticalAngle = 180;
		tempSensor.RequireLineOfSight = true;
		ai.AI.Senses.AddSensor (tempSensor);

		tempSensor = new VisualSensor ();
		tempSensor.SensorName = "Audio Sensor";
		tempSensor.MountPoint = ((BasicAnimator)ai.AI.Animator).Head.Joint;
		tempSensor.Range = 20f;
		tempSensor.HorizontalAngle = 360;
		tempSensor.VerticalAngle = 180;
		tempSensor.SensorColor = new Color(0,0,255f);
		ai.AI.Senses.AddSensor (tempSensor);

		tempSensor = new VisualSensor ();
		tempSensor.SensorName = "Attack Sensor";
		tempSensor.MountPoint = gameObject.transform;
		tempSensor.Range = 1.7f; // 1.05
		tempSensor.PositionOffset= new Vector3(tempSensor.PositionOffset.x,1.12f,tempSensor.PositionOffset.z);
		ai.AI.Senses.AddSensor (tempSensor);

		//Adicionando o elemento customizado dos status do personagem
		//ai.AI.AddCustomElement (new NPCStatus());
		RAIN.Entities.EntityRig entity = ai.AI.Body.GetComponent<RAIN.Entities.EntityRig>();
		AudioAspect temp = new AudioAspect("audio",null);
		temp.MountPoint = ai.AI.Body.transform;
		temp.VisualSize = 1f;
		temp.PositionOffset = new Vector3(0,1.3f,0);
		temp.AspectColor = new Color(255f,0,0);
		temp.IsActive = false;
		entity.Entity.AddAspect(temp);
		
		//lista de pessoas que se encontrou
		ai.AI.WorkingMemory.SetItem<List<AboutPerson>>("aboutperson",new List<AboutPerson>());
		ai.AI.WorkingMemory.SetItem<Dictionary<AboutPerson,bool>>("alreadyattacked",new Dictionary<AboutPerson,bool>());
		if(route != null)
		ai.AI.WorkingMemory.SetItem<GameObject>("route", route);
	}

}
