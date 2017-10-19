using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Interacao : Frases {
	public RaycastHit hit;//objeto na frente do usuario
	public bool podeInteragir=false;//se o usuario pode interagir com alguem
	public float posiX;//variavel para guardar a posiçao x da tela do usuario
	public float posiY;//variavel para guardar a posiçao y da tela do usuario
	public bool conversa = false;//se o usuario esta conversando ou nao
	int i;//variavel para botar no for

	string tipo;
	GUIStyle guiStyle = new GUIStyle();
	public void OnGUI() {
		//posisao da tela onde vai aparecer as mensagens
		posiX = Screen.width / 2;
		posiY = Screen.height / 2 + Screen.height / 5;
		//verifica se o usuario vai poder interagir 
		if (!podeInteragir)
			conversa = false;
		
		if (conversa == false && podeInteragir == true) {
			if (hit.transform.gameObject.GetComponent<Coletavel> () != null) {
				GUI.Label (new Rect (posiX,posiY+20,1000,100),"Coletar-ENTER",guiStyle);
				if (Input.GetKey (KeyCode.Return)) {
					hit.transform.gameObject.GetComponent<Coletavel> ().Coletei ();
					Destroy (hit.transform.gameObject);
				}
				
			}
		}

	}
	public void ReconherAlvo(){
		//Para ver se tem alguem ou algum objeto na frente dele, o 2 siginifica a distancia;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 2)) {
			podeInteragir = true;

		} else {
			podeInteragir = false;



		}
	}


	void Update(){


	}
}
