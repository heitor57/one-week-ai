using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Serialization;
[RAINAction]
public class EnableDamage : RAINAction
{
	public RAIN.Representation.Expression Atacando;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if(ai.Body.transform.Find("free_sword") != null){
			if(Atacando.ExpressionAsEntered.Equals("true"))
				ai.Body.transform.Find ("free_sword").GetComponent<Sword> ().atacando = true;
			else if(Atacando.ExpressionAsEntered.Equals("false"))
				ai.Body.transform.Find ("free_sword").GetComponent<Sword> ().atacando = false;
		}else if(ai.Body.transform.Find("AnimalWeapon") != null){
			if(Atacando.ExpressionAsEntered.Equals("true"))
				ai.Body.transform.Find ("AnimalWeapon").GetComponent<AnimalWeapon> ().atacando = true;
			else if(Atacando.ExpressionAsEntered.Equals("false"))
				ai.Body.transform.Find ("AnimalWeapon").GetComponent<AnimalWeapon> ().atacando = false;
		}
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}