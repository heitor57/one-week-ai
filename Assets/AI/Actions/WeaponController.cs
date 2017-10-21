using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
[RAINAction]
public class WeaponController : RAINAction
{
	public Expression naperna;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if(naperna.IsValid){
			if (naperna.ExpressionAsEntered.Equals ("true")) {
				ai.Body.transform.Find ("free_sword").GetComponent<Sword> ().naperna = true;
				return ActionResult.SUCCESS;
			}else if(naperna.ExpressionAsEntered.Equals ("false")){
				ai.Body.transform.Find ("free_sword").GetComponent<Sword> ().naperna = false;
				return ActionResult.SUCCESS;
			}
		}
		return ActionResult.FAILURE;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}