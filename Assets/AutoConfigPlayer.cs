using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Perception.Sensors;
using RAIN.Core;
using RAIN.Animation;
using RAIN.Minds;
using RAIN.Entities;
public class AutoConfigPlayer : AutoConfig {

	new public void ConfigStart(){
		base.ConfigStart ();
		gameObject.AddComponent<PlayerAttackCreatorAspect> ();
		//Component.Destroy( GetComponent<AIRig> ());

		AIRig ai = GetComponent<AIRig> ();
		//Adicionando partes do corpo no animador
		Transform tempTransform = transform.GetChild(0).Find("mixamorig:Hips").Find("mixamorig:Spine").Find("mixamorig:Spine1").Find("mixamorig:Spine2").Find("mixamorig:Neck");
		((BasicAnimator)ai.AI.Animator).Neck.Joint = tempTransform;
		tempTransform = tempTransform.Find ("mixamorig:Head");
		((BasicAnimator)ai.AI.Animator).Head.Joint = tempTransform;
		((BasicAnimator)ai.AI.Animator).LeftEye.Joint = tempTransform.Find ("mixamorig:LeftEye");
		((BasicAnimator)ai.AI.Animator).RightEye.Joint = tempTransform.Find ("mixamorig:RightEye");
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


		RAIN.Entities.EntityRig entity = ai.AI.Body.GetComponent<RAIN.Entities.EntityRig>();
		AudioAspect temp = new AudioAspect("audio",null);
		temp.MountPoint = ai.AI.Body.transform;
		temp.VisualSize = 1f;
		temp.PositionOffset = new Vector3(0,1.3f,0);
		temp.AspectColor = new Color(255f,0,0);
		temp.IsActive = false;
		entity.Entity.AddAspect(temp);
		//lista de pessoas que se encontrou
		ai.AI.WorkingMemory.SetItem<List<AboutAnimal>>("aboutanimal",new List<AboutAnimal>());
		ai.AI.WorkingMemory.SetItem<Dictionary<AboutAnimal,bool>>("alreadyattacked",new Dictionary<AboutAnimal,bool>());

		TextAsset BehaviorTree;
		BehaviorTree = Resources.Load<TextAsset>("PlayerBehavior");
		((BasicMind)ai.AI.Mind).SetBehavior(BehaviorTree, new Dictionary<string, TextAsset> ());


	}

	void Start () {
		ConfigStart ();
	}

}
