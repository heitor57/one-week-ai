using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
public class Sword : MonoBehaviour {
	public float dmg;
	/*public Transform attachPoint1;
	public Transform attachPoint2;*/
	public Vector3 attachPointposi;
	public Vector3 attachPointangle;
	public GameObject owner;
	public bool atacando=false;
	public bool naperna = true;
	public GameObject posperna;
	public GameObject posmao;
	// Arrumar o colisor depois adicionar os scripts de status e fazer o prefab
	void Start()
	{
		owner = transform.parent.gameObject;
		perna();
		dmg += GetComponentInParent<Destrutiveis>().GetDano();
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
		//else Destroy(gameObject);

	
	}
	public void setAttachPoint(Transform newAP){
		//attachPoint = newAP;
	}
	public void mao(){
		if (posmao == null) {
			attachPointposi = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").position;
			this.attachPointposi += new Vector3 (-0.04f, 0, 0);
			attachPointangle = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").eulerAngles;
			this.attachPointangle += new Vector3 (0, 0, 0);
		} else {
			attachPointposi = posmao.transform.position;
			attachPointangle = posmao.transform.eulerAngles;
		}
	}
	public void perna(){
		if (posperna == null) {
			this.attachPointposi = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:LeftUpLeg").transform.position;
			this.attachPointposi += new Vector3 (-0.139f, 0, 0);
			this.attachPointangle = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:LeftUpLeg").transform.eulerAngles;
			this.attachPointangle += new Vector3 (140f, +35f, 0);
		} else {
			attachPointposi = posperna.transform.position;
			attachPointangle = posperna.transform.eulerAngles;
		}
	}
	void OnTriggerEnter(Collider col){

		if (col.GetComponent<Destrutiveis>()!= null && atacando==true && col.gameObject!=owner.transform.gameObject ) {
			col.GetComponent<Destrutiveis>().perdeVida (dmg,GetComponentInParent<CharacterBase>());
			if(col.GetComponent<AIRig> () != null){
				RAIN.Memory.RAINMemory memory = col.GetComponent<AIRig> ().AI.WorkingMemory;
				if (memory.GetItem<GameObject> ("EnemyGo") != null) {
					foreach (AboutAnimal animal in memory.GetItem<List<AboutAnimal>>("aboutanimal")) {
						if (animal.Target == gameObject) {
							if (animal.Feelings [Constants.violency] >= 100) {
								animal.Feelings [Constants.violency] += 20+ animal.Feelings [Constants.violency] / 5;
							} else {
								animal.Feelings [Constants.violency] = 100;
							}
							break;
						}
					}
				}
			}
		}
	}

	public IEnumerator wait(){
		yield return new WaitForSeconds (1.0f);
		atacando = true;
		yield return new WaitForSeconds (3.0f);
		atacando = false;
	}


}
