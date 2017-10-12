using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
[RAINAction]
public class AttackingCreatorAspect : RAINAction
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
		if (entityrig.Entity.GetAspect ("Attacking") == null) {
			VisualAspect tempAspect = new VisualAspect ("Attacking");
			tempAspect.PositionOffset = new Vector3 (0f, 1.1f, 1.7f);
			tempAspect.VisualSize = 1;
			entityrig.Entity.AddAspect (tempAspect);
		} else {
			entityrig.Entity.RemoveAspect (entityrig.Entity.GetAspect("Attacking"));
		}
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}