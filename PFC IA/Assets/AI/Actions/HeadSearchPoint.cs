using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Animation;
[RAINAction]
public class HeadSearchPoint : RAINAction
{
	static private int y = 0;
	public HeadSearchPoint(){
		actionName = "HeadSearchPoint";
	}
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
		y++;
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		
		Vector3 temp = Vector3.zero;

		if(ai.Body.transform.eulerAngles.y > 315 || ai.Body.transform.eulerAngles.y < 45 || ai.Body.transform.eulerAngles.y > 135 &&  ai.Body.transform.eulerAngles.y < 225 ){
			if (y % 100 == 50) {
				temp = new Vector3 (ai.Kinematic.Position.x - 40,
					ai.Kinematic.Position.y,
					ai.Kinematic.Position.z);
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
			}
			if (y % 100 == 0) {
				temp = new Vector3 (ai.Kinematic.Position.x + 40,
					ai.Kinematic.Position.y,
					ai.Kinematic.Position.z);
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
			} 
		}else{
			if (y % 100 == 50) {
				temp = new Vector3 (ai.Kinematic.Position.x,
					ai.Kinematic.Position.y,
					ai.Kinematic.Position.z -40);
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
			}
			if (y % 100 == 0) {
				temp = new Vector3 (ai.Kinematic.Position.x,
					ai.Kinematic.Position.y,
					ai.Kinematic.Position.z +40);
				ai.WorkingMemory.SetItem<Vector3> ("SearchPosition", temp);
			} 
		}
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}