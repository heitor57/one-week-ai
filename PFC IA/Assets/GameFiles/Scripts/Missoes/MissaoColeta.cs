using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissaoColeta : Missoes {

	private int totalItem = 0;//Valor correspondente ao total de itens que o jogador deve coletar
	private int itemsColetados;//Vetor que armazena quantos itens o jogador já coletou
	private bool emMissao;
	public float posiX;
	public float posiY;
	public string[] ponto= new string[3];


	//Métodos de acesso e modificação do atributo da classe MissaoColeta
	public int gettotalItem(){
		return totalItem;
	} 

	public void settotalItem(){//Toda vez que o jogador aceita uma missão, o valor de itens a ser coletado deve ser passado a variável totalItem
		this.totalItem = Random.Range(0,10);
	} 

	//Método que irá verificar se o item que o personagem está querendo coletar eh ou não o pedido na missao
	//Caso seja um item diferente, não serpa permitido ao personagem coleta-lo
	//Se for o item pedido, tal será permitido ser coletado e colocado no vetor de itens
	protected void verificaItem(bool testaItem){
		if(testaItem == true){
			itemsColetados++;
		}else{
			Debug.Log("Este item não confera com nenhum em sua lista de procura");
		}
	}
	void Update(){
		if (itemsColetados == totalItem) {
			setConcluida (true);
			emMissao = false;
		}
		ponto [0] = "Preciso que voce pegue baldes de agua ou garrafas de vinho";
		ponto [1] = "Faltam" + (totalItem-itemsColetados)+"itens";
		ponto [2] = "Obrigado por ter coletado para min";
	}
	public void Começar(){
		settotalItem ();
		itemsColetados = 0;
		GetComponent<GeradorItem> ().Criando(gettotalItem());
		emMissao = true;

	}
	public string estatosdaMissao(){
		if (!getConcluida ())
			return ponto [2];
		if (emMissao) {
			return ponto [1];
		} else {
			return ponto [0];
		}
	}

	public void OnGUI(){
		posiX = Screen.width / 2;
		posiY = Screen.height / 2 + Screen.height / 5;
		if (emMissao == true) {
			GUI.Label(new Rect(posiX,posiY+10,1000,100),estatosdaMissao());
		}
	}
}
