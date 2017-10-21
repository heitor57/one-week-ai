using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using RAIN.Serialization;


[RAINAction]
public class PlayAudio : RAINAction
{
	AudioAspect audio;
	public Expression Audio = new Expression(); // nome do som para dar play, ainda nao tem
	public Expression Data = new Expression();
	public Expression IsActive = new Expression();


    public override void Start(RAIN.Core.AI ai)
    {
		audio = (AudioAspect)ai.Body.GetComponent<RAIN.Entities.EntityRig>().Entity.GetAspect("audio");
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{
		

		if (Data.IsValid == true ) {
			audio.data = Data.ExpressionAsEntered;
		}
		if (IsActive.IsValid == true) {
			audio.IsActive = bool.Parse (IsActive.ExpressionAsEntered);
		}

		//if (Audio.IsValid == true) nao tem ainda
			
			
		
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}