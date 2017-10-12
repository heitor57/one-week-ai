using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Animation;
using RAIN.Representation;
using RAIN.Serialization;
[RAINAction]
public class PlayAnimation : RAINAction
{
	public Expression Animation; 
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if(Animation.IsVariable){
			((BasicAnimator)ai.Animator).UnityAnimator.Play (Animation.ExpressionAsEntered);
		}
        return ActionResult.SUCCESS;
    }


    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}