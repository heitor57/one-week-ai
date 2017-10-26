using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class PointForward : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		Vector3 forward = ai.Body.transform.position +
			2*new Vector3( Mathf.Sin(ai.Body.transform.eulerAngles.y*Mathf.Deg2Rad),0,Mathf.Cos(ai.Body.transform.eulerAngles.y*Mathf.Deg2Rad));
		ai.WorkingMemory.SetItem<Vector3> ("forward",forward);
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}