using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			attachPointposi = transform.parent.Find("TSMG_Rig").Find("TSMG_Cruft").Find("rightLeg1_ParentConstrain").Find("rightLeg1_Rigging").Find("rightLeg1_RigJoints").Find("rightLeg1_RigUpperLeg").Find("rightLeg1_RigLowerLeg").Find ("rightLeg1_RigFoot").position;
			attachPointangle = transform.parent.Find("TSMG_Rig").Find("TSMG_Cruft").Find("rightLeg1_ParentConstrain").Find("rightLeg1_Rigging").Find("rightLeg1_RigJoints").Find("rightLeg1_RigUpperLeg").Find("rightLeg1_RigLowerLeg").Find ("rightLeg1_RigFoot").eulerAngles;

			transform.position = attachPointposi;
			transform.eulerAngles = attachPointangle;
		}

	
	}
	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Destrutiveis>()!= null && atacando==true && col.gameObject!=owner.transform.gameObject ) {
			col.GetComponent<Destrutiveis>().perdeVida (dmg,GetComponentInParent<CharacterBase>());
		}
	}
}
