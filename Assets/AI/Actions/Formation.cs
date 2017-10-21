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
	private List<GameObject> slots;
	private Entity entity;
	private List<RAINAspect> slot_aspect;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		lider = (GameObject)ai.WorkingMemory.GetItem ("Leader");
		animator = ((RAIN.Animation.BasicAnimator)ai.Animator).UnityAnimator;
		slots = (List<GameObject>)ai.WorkingMemory.GetItem ("slotocupado");
		slot_aspect = (List<RAINAspect>)ai.WorkingMemory.GetItem("slot");
		entity = ai.Body.GetComponent<RAIN.Entities.EntityRig>().Entity;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		
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
		int j = 0, x = 0, z = 0;
		int spotx, spoty;
		Vector3 temp;
		Vector3 lider_loc = lider.transform.position;
		float lider_rot = lider.transform.eulerAngles.y;
		if (slots.Count == 0) { // Primeiro slot de uma formação é feito aqui.
			temp = lider_loc;
			spotx = -5;
			spoty = -1;
			temp = temp + 
				new Vector3(+Mathf.Sin(Mathf.Deg2Rad*lider_rot)*spoty +Mathf.Cos(Mathf.Deg2Rad*lider_rot)*spotx ,
					0,
					+Mathf.Cos(Mathf.Deg2Rad*lider_rot)*spoty -Mathf.Sin(Mathf.Deg2Rad*lider_rot)*spotx);
			ai.WorkingMemory.SetItem<Vector3> ("FormacaoPos", temp);
			return ActionResult.SUCCESS;
		} else {// Busca por um slot caso não seja o primeiro.
			for (z = -1; z >= -10; z--) {
				for (x = -1; 2 > x; x++) {
					temp = lider_loc;
					temp = temp + 
						new Vector3(+Mathf.Sin(Mathf.Deg2Rad*lider_rot)*z +Mathf.Cos(Mathf.Deg2Rad*lider_rot)*x ,
							0,
							+Mathf.Cos(Mathf.Deg2Rad*lider_rot)*z -Mathf.Sin(Mathf.Deg2Rad*lider_rot)*x);
					for (j = 0; slots.Count > j; j++) {// Verifica todos slots ocupados
						//Verifica se a distancia de uma IA para o ponto na formação em dois planos e menor que 0,5.
						//Ou seja se tiver uma IA neste respectivo ponto da formação ira sair do loop 
						//e partir pra outro ponto e fazer a verificação.


						if (((SlotAspect)slot_aspect[j]).Slot.z == z && ((SlotAspect)slot_aspect[j]).Slot.x == x)
							break;
						if (slots.Count - 1 == j) {
							//Tentando fazer eles pegarem uma posiçao fixa
							//entity.GetAspect ("slot").AspectColor =new Color(255,120,0);
							//Debug.Log (ai.Body.name+" ---- "+(temp-entity.GetAspect ("slot").PositionOffset));
							((SlotAspect)entity.GetAspect("slot")).Slot= new Vector3(x,0,z);
							ai.WorkingMemory.SetItem<Vector3> ("FormacaoPos", temp);
							return ActionResult.SUCCESS;
						}

					}
				}
			}
		}
		return ActionResult.SUCCESS;
	
    }
		
    public override void Stop(RAIN.Core.AI ai)
    {
		
        base.Stop(ai);
    }
}