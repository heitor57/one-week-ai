using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObjects : NotAlive {
	//public BasicStats basicStats;
	public void getItem(){
		/*base.obliterate ();
		return this;*/
	}	//Interação de pegar algo(Como madeira ou outros itens que tenham representação em 3D) 
	public void analyse(){
		//PlayerBehaviour.Dialogs.PlayDialog(this.toString);
	}
	public void Start(){
		SetNome (nome);
		SetVida (vida);
		SetDano (dano);
	}
	public override void perdeVida(float dano, CharacterBase cb)
	{
		this.vida -= dano;
		if (vida <= 0)
		{
			cb.currentXP += gameObject.GetComponent<XP> ().darXP (cb.xpMultiply);
			StartCoroutine (gameObject.GetComponent<TriangleExplosion> ().SplitMesh (true));
		}
	}
}
