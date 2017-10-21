using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Animation;
using RAIN.Serialization;
using RAIN.Representation;
[RAINAction]
public class HeadSearchPoint : RAINAction
{
	public Expression headpositionstring;// "mid" "left" "right"
	public Expression headpositionangle; // numeric

	public HeadSearchPoint(){
		actionName = "HeadSearchPoint";
	}
	public override void Start(RAIN.Core.AI ai)
	{
		base.Start(ai);

	}

	public override ActionResult Execute(RAIN.Core.AI ai)
	{

		Vector3 temp = Vector3.zero;
		if (headpositionangle.IsValid) {
			temp = new Vector3 (ai.Kinematic.Position.x + Mathf.Sin (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y+float.Parse(headpositionangle.ExpressionAsEntered))),
				1.5f,
				ai.Kinematic.Position.z + Mathf.Cos (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y+float.Parse(headpositionangle.ExpressionAsEntered))));
			ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);

		}else if (headpositionstring.IsValid) {
			switch(headpositionstring.ExpressionAsEntered){
			case "mid":
				temp = new Vector3 (ai.Kinematic.Position.x + Mathf.Sin (Mathf.Deg2Rad * ai.Body.transform.eulerAngles.y),
					1.5f,
					ai.Kinematic.Position.z + Mathf.Cos (Mathf.Deg2Rad * ai.Body.transform.eulerAngles.y));
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
				break;
			case "right":
				temp = new Vector3 (ai.Kinematic.Position.x + Mathf.Sin (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y+90)),
					1.5f,
					ai.Kinematic.Position.z + Mathf.Cos (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y+90)));
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
				break;
			case "left":
				temp = new Vector3 (ai.Kinematic.Position.x + Mathf.Sin (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y-90)),
					1.5f,
					ai.Kinematic.Position.z + Mathf.Cos (Mathf.Deg2Rad * (ai.Body.transform.eulerAngles.y-90)));
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
				break;
			}
		}

		return ActionResult.SUCCESS;
	}

	public override void Stop(RAIN.Core.AI ai)
	{

		base.Stop(ai);
	}

}