using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NotAlive : Destrutiveis {

	//public abstract void Enter ();
	//public abstract void Exit ();
	public override void perdeVida(float dano, CharacterBase cb)
	{
		this.vida -= dano;
		if (vida <= 0)
		{
			StartCoroutine (gameObject.GetComponent<TriangleExplosion> ().SplitMesh (true));
		}
	}

}
