using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Entities;
[RAINAction]
public class AttackCreatorAspect : RAINAction
{
	public Transform transform;
	public RAIN.Entities.EntityRig entityrig;
	//variaveis de ajuda
    public override void Start(RAIN.Core.AI ai)
    {
		transform = ai.Body.GetComponent<Transform> ();//Guarda transform do GameObject respectivo
		entityrig = ai.Body.GetComponent<EntityRig> ();//Guarda EnitityRig do GameObject respectivo
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		//Cria um aspecto "Attack" e o adiciona.
		if (entityrig.Entity.GetAspect ("Attack") == null) {
			VisualAspect tempAspect = new VisualAspect ("Attack");
			tempAspect.PositionOffset = new Vector3 (0f, 1.1f, 1.7f);
			tempAspect.VisualSize = 1;
			tempAspect.AspectColor =new Color(255,120,0);
			entityrig.Entity.AddAspect (tempAspect);
		} else {
			entityrig.Entity.RemoveAspect (entityrig.Entity.GetAspect("Attack"));
		}
		return ActionResult.SUCCESS;

    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}