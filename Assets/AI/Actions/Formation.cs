using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.BehaviorTrees;
using RAIN.BehaviorTrees.Actions;
using RAIN.Entities.Aspects;
using RAIN.Entities;
[RAINAction]
public class Formation : RAINAction
{
	
	private RAIN.Representation.Expression expressao;
	private Animator animator;
	private GameObject lider;
	private Entity entity;
	private List<RAINAspect> slots;
	private Vector2 sizeformation;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		lider = (GameObject)ai.WorkingMemory.GetItem ("Leader");
		animator = ((RAIN.Animation.BasicAnimator)ai.Animator).UnityAnimator;
		slots = (List<RAINAspect>)ai.WorkingMemory.GetItem("slot");
		entity = ai.Body.GetComponent<RAIN.Entities.EntityRig>().Entity;
		sizeformation = ai.WorkingMemory.GetItem<GameObject> ("Leader").GetComponent<SerVivo> ().GetTamanhoFormacao ();

	}

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		int j = 0, x = 0, z = 0;
		int spotx, spoty;
		Vector3 temp;
		Vector3 lider_loc = lider.transform.position;
		float lider_rot = lider.transform.eulerAngles.y;
		List<SlotAspect> valid_slots = new List<SlotAspect>(); 
		for(j = 0 ; j<slots.Count;j++){
			if (((SlotAspect)slots [j]).Head == lider) {
				valid_slots.Add ((SlotAspect)slots [j]);
			}
		}

		if (ai.WorkingMemory.GetItem<bool> ("movesucedido") == true) { // Se tiver no ponto da formação
			animator.Play ("Idle", 0);// Animação parado
			ai.Kinematic.Orientation = lider.transform.localEulerAngles; // Adquire rotação do lider
			if (0.5f >= (ai.Kinematic.Position.x - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).x)
			   && (ai.Kinematic.Position.x - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).x >= -0.5f)
			   && 0.5f >= (ai.Kinematic.Position.z - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).z)
			   && (ai.Kinematic.Position.z - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).z >= -0.5f)) {//Ve se esta perto da posição da formação

			} else { // Se não estiver perto o movesucedido ira ser false
				//Debug.Log ("MOVESUCEDIDO FALSO");
				ai.WorkingMemory.SetItem<bool> ("movesucedido", false);
			}
		}

		if (0.5f >= (ai.Kinematic.Position.x - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).x)
		   && (ai.Kinematic.Position.x - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).x >= -0.5f)
		   && 0.5f >= (ai.Kinematic.Position.z - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).z)
		   && (ai.Kinematic.Position.z - (ai.WorkingMemory.GetItem<Vector3> ("FormacaoPos")).z >= -0.5f)) { //verifica se esta perto da formação
		} else { // O animador da play na animaçao walk
			animator.Play ("Walk", 0);
		}

		if (valid_slots.Count == 0) { // Primeiro slot de uma formação é feito aqui.
			temp = lider_loc;

			spotx = 0;
			spoty = -1;
			temp = temp + 
				new Vector3(+Mathf.Sin(Mathf.Deg2Rad*lider_rot)*spoty +Mathf.Cos(Mathf.Deg2Rad*lider_rot)*spotx ,
					0,
					+Mathf.Cos(Mathf.Deg2Rad*lider_rot)*spoty -Mathf.Sin(Mathf.Deg2Rad*lider_rot)*spotx);
			ai.WorkingMemory.SetItem<Vector3> ("FormacaoPos", temp);
			return ActionResult.SUCCESS;
		} else {// Busca por um slot caso não seja o primeiro.
			int xpos =0;
			for (z = -1; z >= -sizeformation.y; z--) {
				for (x = 0; (int)sizeformation.x > x; x++) {
					temp = lider_loc;
					if (x == 0) {
						xpos = 0;
					} else if(x%2 == 1){
						xpos = Mathf.Abs (xpos);
						xpos++;
					}else{
						xpos = -xpos;
					}
					temp = temp + 
						new Vector3(+Mathf.Sin(Mathf.Deg2Rad*lider_rot)*z +Mathf.Cos(Mathf.Deg2Rad*lider_rot)*xpos ,
							0,
							+Mathf.Cos(Mathf.Deg2Rad*lider_rot)*z -Mathf.Sin(Mathf.Deg2Rad*lider_rot)*xpos);
					for (j = 0; valid_slots.Count > j; j++) {
						
						if (valid_slots[j].Slot.z == z && valid_slots[j].Slot.x == xpos)
							break;
						if (valid_slots.Count - 1 == j) {
							((SlotAspect)entity.GetAspect("slot")).Slot= new Vector3(xpos,0,z);
							ai.WorkingMemory.SetItem<Vector3> ("FormacaoPos", temp);
							return ActionResult.SUCCESS;
						}

					}
				}
			}
		}
		return ActionResult.SUCCESS;
	
    }
	/*
	 * Talvez venha a ser utilizado novamente
	Vector2 quadraticdistributionx(float area){
		int val1=0, val2=0;
		if (area >= 3 && (area + 3) % 2 == 0) {
			val1 = -((int)area-1) / 2;
			val2 = ((int)area-1) / 2;
		} else if(area>=0){
			val1 = -((int)area) / 2;
			val2 = ((int)area-1) / 2;
		}
		Vector2 vector =  new Vector2(val1,val2);

		Debug.Log (vector);
		return vector;

	}*/
    public override void Stop(RAIN.Core.AI ai)
    {
		
        base.Stop(ai);

    }
}