using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	MUsuario AuxMissao;
	Vector3 Dist;
	float posiX;
	float posiY;
	int opcaoConversa;
	int opcaoConversa2;
	int nivelConversa;
	bool conversa = false;
	bool podeInteragir=false;
	string textoFala;
	int i;
	string frase1;
	float frase2;
	string tipo;
	GUIStyle guiStyle = new GUIStyle();
	public Interacao j;



	// Use this for initialization
	void Start () {
		Dist = GameObject.Find ("Player").transform.position;
		Dist = Dist - transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		Dist = GameObject.Find ("Player").transform.position;
		Dist = Dist - transform.position;
		if (conversa) {
			if (Input.GetKeyDown (KeyCode.L))
				opcaoConversa++;
			if (Input.GetKeyDown (KeyCode.O))
				opcaoConversa--;
			if (opcaoConversa > 2)
				opcaoConversa = 0;
			if (opcaoConversa < 0)
				opcaoConversa = 2;
			if (podeInteragir==true) {
				//parte da conversa em que o usuario se encontra vai progredir nesse if
				if (Input.GetKeyDown (KeyCode.Return)) {
					if (nivelConversa >= 0) {
						opcaoConversa2 = opcaoConversa;
					}
					//avança o nivel da conversa
					nivelConversa++;
				}
			}
		}
	}
	void OnGUI(){
		posiX = Screen.width / 2;
		posiY = Screen.height / 2 + Screen.height / 5;
		if (Dist.magnitude < 2)
			podeInteragir = true;
		else
			podeInteragir = false;
			if(!podeInteragir)
				conversa = false;
			if (conversa == false && podeInteragir == true) {
				//mensagem quando o usuario passar perto de um npc
				guiStyle.fontSize=20;
			GUI.Label (new Rect (posiX, posiY+20, 1000, 100), "Interagir- ENTER",guiStyle);
				//se o usuario apertar enter a conversa vai começar
			if (Input.GetKeyDown (KeyCode.Return)) {
				conversa = true;
			}
				opcaoConversa = 0;
				nivelConversa = -1;

				//if (Input.GetKeyDown (KeyCode.B)) {
				//	CausarDano (gameObject.GetComponent<Destrutiveis> ().GetDano ());
				//}
			}
		if (conversa) {


			//1º nivel de conversa, o usuario respondendo ao npc
			if (nivelConversa == 0) {
				for (i = 0; i < 3; i++) {
					GUI.color = Color.white;
					if (i == 0) {
						textoFala = "Como vai voce?";
						if (opcaoConversa == 0)
							GUI.color = Color.yellow;
						frase1 = j.GetFrase ();
					}
					if (i == 1) {
						textoFala = "Onde estou?";
						if (opcaoConversa == 1)
							GUI.color = Color.yellow;
						posiY = posiY + 10;
						frase2 = Random.value;
					}
					//para acabar o dialogo
					if (i == 2) {
						textoFala = "Sair";
						if (opcaoConversa == 2)
							GUI.color = Color.yellow;
						posiY = posiY + 10;
					}
					GUI.Label (new Rect (posiX, posiY, 1000, 100), textoFala);
				}
			}
			//2º nivel de conversa, npc respondendo ao usuario
			if (nivelConversa == 1) {
				textoFala = GetComponent<Destrutiveis> ().GetNome () + " : ";

				if (opcaoConversa2 == 0)
					GUI.Label (new Rect (posiX, posiY, 1000, 100), textoFala + frase1);
				if (opcaoConversa2 == 1) {
					if (frase2 > 0.5) {
						textoFala = textoFala + "Capitão voce esta passando mal? Estamos em Boreas.";
					} else {
						if (frase2 < 0.25) {
							textoFala = textoFala + "Boreas";
						} else {
							textoFala = textoFala + "Eu nao sei";
						}
					}
					GUI.Label (new Rect (posiX, posiY, 1000, 100), textoFala);
				}
				// o usuario decidiu sair da conversa
				if (opcaoConversa2 == 2)
					conversa = false;
			}
			if (GetComponent<MissaoColeta> () != null) {
				if (nivelConversa == 2) {
					GUI.Label (new Rect (posiX, posiY, 1000, 100), "Capitao preciso de sua ajuda.");
					GUI.Label(new Rect(posiX,posiY+10,1000,100),GetComponent<MissaoColeta> ().estatosdaMissao());
				}
				if (nivelConversa == 3) {
					for (i = 0; i < 3; i++) {
						GUI.color = Color.white;
						if (i == 0) {
							textoFala = "Sim";
							if (opcaoConversa == 0)
								GUI.color = Color.yellow;
							frase1 = j.GetFrase ();
						}
						if (i == 1) {
							textoFala = "Não";
							if (opcaoConversa == 1)
								GUI.color = Color.yellow;
							posiY = posiY + 10;
							frase2 = Random.value;
						}
						//para acabar o dialogo
						if (i == 2) {
							textoFala = "Sair";
							if (opcaoConversa == 2)
								GUI.color = Color.yellow;
							posiY = posiY + 10;
						}
						GUI.Label (new Rect (posiX, posiY, 1000, 100), textoFala);
					}
				}
				if (nivelConversa == 4) {
					if (opcaoConversa2 == 0) {
						GUI.Label (new Rect (posiX + 150, posiY - 400, 1000, 100), "0 / 3 Baldes Coletados");
						//hit.transform.gameObject.GetComponent<GeradorItem> ().Criando();
						GetComponent<MissaoColeta> ().Começar ();

						nivelConversa++;
					}
					if (opcaoConversa2 == 1) {
						GUI.Label (new Rect (posiX, posiY, 1000, 100), "Entao vai se FODER!!!!!!");
					}
					if (opcaoConversa2 == 2) {
						conversa = false;
					}

				}
			}	
		}
		}
	}

