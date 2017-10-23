using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Perception.Sensors;
using RAIN.Entities.Aspects;
[RAINAction]
public class AudioUpdate : RAINAction
{
	private int i = 0;
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		List<AudioAspect> audiohelpaspects = new List<AudioAspect> ();
		List<AudioAspect> audiopleasestopaspects = new List<AudioAspect> ();

			
		if (ai.WorkingMemory.GetItem ("audio") != null) {
			for (i = 0; i < ((List<RAINAspect>)(ai.WorkingMemory.GetItem ("audio"))).Count; i++) {
				if (AboutPerson.isGoingHelpUnknown (ai.Body.GetComponent<SerHumano> ()) && ((AudioAspect)(ai.WorkingMemory.GetItem<List<RAINAspect>> ("audio") [i])).data == Speaks.help) {
					audiohelpaspects.Add ((AudioAspect)(ai.WorkingMemory.GetItem<List<RAINAspect>> ("audio") [i]));
				}
			}
		
			float radius = ((VisualSensor)ai.Senses.GetSensor ("Audio Sensor")).Range;
			AudioAspect audiohelpaspect = new AudioAspect ();

			for (i = 0; i < audiohelpaspects.Count; i++) {
				if (Vector3.Distance (audiohelpaspects [i].MountPoint.position, ai.Body.transform.position) <= radius) {
					radius = Vector3.Distance (audiohelpaspects [i].Entity.Form.transform.position, ai.Body.transform.position);
					audiohelpaspect = audiohelpaspects [i]; 
				}
			}
			if (audiohelpaspects.Count > 0 && audiohelpaspect.AspectName != null) {
				ai.WorkingMemory.SetItem<Vector3>("AudioPosition", audiohelpaspect.Entity.Form.transform.position);
			}
		}
		return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}