using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tigre : Animal {
	public override void perdeVida(float dano, CharacterBase cb)
	{
		this.vida -= dano;
		if (vida <= 0)
		{
			Destroy (gameObject.GetComponent<RAIN.Core.AIRig> ());
			GetComponent<Animator> ().Play ("morte1");
			Destroy (gameObject, 5f);
		}
	}
}
