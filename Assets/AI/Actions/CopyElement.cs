using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
[RAINAction]
public class CopyElement : RAINAction
{
	public Expression Frist_Element;
	public Expression Second_Element;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		string Frist_Element_s = Frist_Element.ExpressionAsEntered, 
		Second_Element_s = Second_Element.ExpressionAsEntered;

		ai.WorkingMemory.SetItem (Second_Element_s,  ai.WorkingMemory.GetItem(Frist_Element_s), ai.WorkingMemory.GetItemType(Frist_Element_s));

		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}