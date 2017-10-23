using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN;
using RAIN.Perception.Sensors;
using RAIN.Animation;
using RAIN.Entities.Aspects;
public class AutoConfig : MonoBehaviour {

	public void ConfigStart(){
		//Configurações basicas dos componentes.
		//EntityRig
		RAIN.Entities.EntityRig entity= GetComponent<RAIN.Entities.EntityRig>();
		entity.Entity.Form = gameObject;

		//AIRig
		RAIN.Core.AIRig ai = GetComponent<RAIN.Core.AIRig> ();
		ai.AI.Body = gameObject;
		

		//Detecçao de pessoa | para os outros
		VisualAspect person = new VisualAspect("person");
		person.MountPoint = ai.AI.Body.transform;
		person.PositionOffset= new Vector3(0,1,0);
		entity.Entity.AddAspect (person);
	}
	public void ConfigAwake(){
		gameObject.AddComponent<RAIN.Entities.EntityRig>();
		gameObject.AddComponent<RAIN.Core.AIRig>();
	}
	void Start(){
		//Configuração da AIRIG e EntityRIG basica.
		ConfigStart();
	}
	void Awake(){
		ConfigAwake ();
	}

}
