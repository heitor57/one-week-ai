using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {
	public float dmg;
	/*public Transform attachPoint1;
	public Transform attachPoint2;*/
	public Vector3 attachPointposi;
	public Vector3 attachPointangle;
	public GameObject owner;
	public bool atacando=false;
	public bool naperna = true;
	// Arrumar o colisor depois adicionar os scripts de status e fazer o prefab
	void Start()
	{
		 //Transform temp= new Transform();
		/*
		temp.position.x = 0.1217021f;
		temp.position.y = -0.03058553f;
		temp.position.z = 0.183293f;
		temp.eulerAngles.x = 26.087f;
		temp.eulerAngles.y = -4.119f;
		temp.eulerAngles.z = -172.307f;
*/

		perna();
		owner = transform.parent.gameObject;
		dmg += GetComponentInParent<Destrutiveis>().GetDano();
		if (owner != null)
		{
			transform.localScale = owner.transform.localScale;
		}

	}

	void Update()
	{
		
		if (owner != null)
		{
			if (naperna) {
				perna ();
				transform.position = attachPointposi;
				transform.eulerAngles = attachPointangle;
			} else {
				mao ();
				transform.position = attachPointposi;
				transform.eulerAngles = attachPointangle;
			}
		}
		else Destroy(gameObject);

	
	}

	/*public void changeAttachPoint(){
		if (attachPoint == attachPoint1) {
			attachPoint = attachPoint2;
		} else if (attachPoint == attachPoint2) {
			attachPoint = attachPoint1;
		}
	}*/
	public void setAttachPoint(Transform newAP){
		//attachPoint = newAP;
	}
	public void mao(){
		attachPointposi = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").position;
		this.attachPointposi += new Vector3 (-0.04f,0,0);
		attachPointangle = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").eulerAngles;
		this.attachPointangle += new Vector3 (0,0,0);
	}
	public void perna(){
		this.attachPointposi = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:LeftUpLeg").transform.position;
		this.attachPointposi += new Vector3 (-0.139f, 0, 0);
		this.attachPointangle = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:LeftUpLeg").transform.eulerAngles;
		this.attachPointangle += new Vector3 (140f, +35f,0);
	}
	void OnTriggerEnter(Collider col){
		//Debug.Log ("Ataque foi feito:    "+atacando);
		if (col.GetComponent<Destrutiveis>()!= null && atacando==true && col.gameObject != owner.gameObject) {
			col.GetComponent<Destrutiveis>().perdeVida (dmg);
			//(col.gameObject as NotAlive).perdeVida (dmg);
		}
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (1.0f);
		atacando = true;
		yield return new WaitForSeconds (3.0f);
		atacando = false;
	}


}
