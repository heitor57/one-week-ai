using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
[RAINAction]
public class RandomNumber : RAINAction
{
	public Expression Name;
	public Expression Min;
	public Expression Max;
	public Expression Divider;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		if (Min.IsNull == false && Max.IsNull == false && Name.IsNull == false) {
			if(Divider.ExpressionAsEntered.Length == 0){
				ai.WorkingMemory.SetItem<float> (Name.ExpressionAsEntered,
					Random.Range (float.Parse(Min.ExpressionAsEntered),float.Parse(Max.ExpressionAsEntered)));
			}else{
				ai.WorkingMemory.SetItem<float> (Name.ExpressionAsEntered,
					Random.Range (float.Parse(Min.ExpressionAsEntered),float.Parse(Max.ExpressionAsEntered))
					/ float.Parse(Divider.ExpressionAsEntered));
			}
		}
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}