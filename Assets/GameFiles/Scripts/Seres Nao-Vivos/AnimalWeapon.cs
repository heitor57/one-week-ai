using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
public class AnimalWeapon : MonoBehaviour {
	public float dmg;
	public Vector3 attachPointposi;
	public Vector3 attachPointangle;
	public GameObject owner;
	public bool atacando=false;
	void Start()
	{
		owner = transform.parent.gameObject;
		dmg += GetComponentInParent<Destrutiveis>().GetDano();
	}

	void Update()
	{
		
		if (owner != null)
		{
			if(transform.parent.Find("TSMG_Rig")!=null){
				attachPointposi = transform.parent.Find("TSMG_Rig").Find("TSMG_Cruft").Find("rightLeg1_ParentConstrain").Find("rightLeg1_Rigging").Find("rightLeg1_RigJoints").Find("rightLeg1_RigUpperLeg").Find("rightLeg1_RigLowerLeg").Find ("rightLeg1_RigFoot").position;
				attachPointangle = transform.parent.Find("TSMG_Rig").Find("TSMG_Cruft").Find("rightLeg1_ParentConstrain").Find("rightLeg1_Rigging").Find("rightLeg1_RigJoints").Find("rightLeg1_RigUpperLeg").Find("rightLeg1_RigLowerLeg").Find ("rightLeg1_RigFoot").eulerAngles;
			}else if(transform.parent.Find("mixamorig:Hips") !=null){
				attachPointposi = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").position;
				attachPointangle = transform.parent.Find ("mixamorig:Hips").Find ("mixamorig:Spine").Find ("mixamorig:Spine1").Find ("mixamorig:Spine2").Find ("mixamorig:RightShoulder").Find ("mixamorig:RightArm").Find ("mixamorig:RightForeArm").Find ("mixamorig:RightHand").eulerAngles;
			}
			transform.position = attachPointposi;
			transform.eulerAngles = attachPointangle;
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
}
