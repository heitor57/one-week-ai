using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RAIN;
using RAIN.Entities.Aspects;
using RAIN.Entities;
public class PlayerAttackCreatorAspect : MonoBehaviour {
	public RAIN.Entities.EntityRig entityrig;
	//variaveis de ajuda
	bool attackmaking=false;
	float time_init=0.5f, time_final = 1f;//Tempo de demora de um ataque em inteiro, de acordo com o clock da plataforma.

	// Use this for initialization
	IEnumerator Wait(){
		attackmaking = true;
		yield return new WaitForSeconds (time_init);
		VisualAspect tempAspect = new VisualAspect ("Attack");
		tempAspect.PositionOffset = new Vector3 (0f, 1.1f, 1.7f);
		tempAspect.VisualSize = 1;
		entityrig.Entity.AddAspect (tempAspect);
		yield return new WaitForSeconds (time_final);
		entityrig.Entity.RemoveAspect(entityrig.Entity.GetAspect("Attack"));
		attackmaking = false;
	}
	void Start () {

		entityrig = GetComponent<EntityRig> ();//Guarda EnitityRig do GameObject respectivo
	}
	
	// Update is called once per frame
	void Update () {
		
		//Cria um aspecto "Attack" e o adiciona.
		if (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown (KeyCode.Keypad2) ||Input.GetKeyDown (KeyCode.Keypad1) && attackmaking == false) {
			StartCoroutine (Wait ());
		}

		//Gerencia o tempo que um ataque é feito.
			
				
	}
}
