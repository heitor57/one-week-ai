using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SerHumano : SerVivo {
	[SerializeField]protected int bondade=0;
	[SerializeField]protected int carisma=0;
	[SerializeField]protected int coragem=0;
	[SerializeField]protected int influencia=0;
	[SerializeField]protected int lealdade=0;
	[SerializeField]protected int intimidacao=0;
	[SerializeField]protected int lideranca=0;
	[SerializeField]protected int inteligencia=0;
	[SerializeField]protected string facçao="Neutro";
	[SerializeField]protected string profissao;


	public string GetFacçao(){
		return facçao;
	}
	public void SetFacçao(string facçao){
		this.facçao = facçao;
	}
	public void Interagir(SerHumano o)
	{
		Debug.Log("Estou interagindo com "+o.GetNome());
	}
	public string Montar()
	{
		return "Estou montado";
	}
	public string TerAnimais()
	{
		return "Estou pegando um animal para min";
	}
	public string TerFilhos()
	{
		return "Estou fazendo filhos";
	}
	public string MeusAnimais()
	{
		return "Meus animais sao";
	}
	public string Comandar()
	{
		return "Estou comandando";
	}
	public string GetProfissao()
	{
		return this.profissao;
	}
	public void SetProfissao(string profissao)
	{
		this.profissao = profissao;
	}
	public int GetBondade()
	{
		return this.bondade;
	}
	public int GetLealdade()
	{
		return this.lealdade;
	}
	public int GetCarisma()
	{
		return this.carisma;
	}
	public int GetCoragem()
	{
		return this.coragem;
	}
	public int GetInfluencia()
	{
		return this.influencia;
	}
	public int GetIntimidacao()
	{
		return this.intimidacao;
	}
	public int GetLideranca()
	{
		return this.lideranca;
	}
	public void SetBondade(int bondade)
	{
		this.bondade = bondade;
	}
	public void SetLealdade(int lealdade)
	{
		this.lealdade = lealdade;
	}
	public void SetCarisma(int carisma)
	{
		this.carisma = carisma;
	}
	public void SetCoragem(int coragem)
	{
		this.coragem = coragem;
	}
	public void SetInfluencia(int influencia)
	{
		this.influencia = influencia;
	}
	public void SetIntimidacao(int intimidacao)
	{
		this.intimidacao = intimidacao;
	}
	public void SetLideranca(int lideranca)
	{
		this.lideranca = lideranca;
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
			animator.Play ("morte1");
			this.GetComponent<CapsuleCollider> ().direction = 2;
			this.GetComponent<CapsuleCollider> ().center = new Vector3 (0f, 0f, 1.2f);
			Destroy (gameObject, 5f);


		}
	}
	}

