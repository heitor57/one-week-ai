using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RAIN.Core;
public abstract class SerHumano : SerVivo {
	
	[SerializeField]protected string profissao;

	public string GetProfissao()
	{
		return this.profissao;
	}
	public void SetProfissao(string profissao)
	{
		this.profissao = profissao;
	}

	/*void Update()
	{
		

	}*/
	
	public override void perdeVida(float dano, CharacterBase cb)
	{
		this.vida -= dano;
		if (vida <= 0)
		{
			Destroy (gameObject.GetComponent<RAIN.Core.AIRig> ());
			GetComponent<Animator> ().Play ("morte1");
			this.GetComponent<CapsuleCollider> ().direction = 2;
			this.GetComponent<CapsuleCollider> ().center = new Vector3 (0f, 0f, 1.2f);
			Destroy (gameObject, 5f);


		}
	}
	}

