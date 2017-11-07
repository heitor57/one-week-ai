using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RAIN.Core;
public class MessagesList{
	public List<string> messageslist_default=new List<string>(),
						messageslist_west=new List<string>(),
						messageslist_verus=new List<string>(),
						messageslist_east =new List<string>(),
						messageslist_searching = new List<string>(),
						messageslist_fighting_stronger = new List<string>(),
						messageslist_fighting_weaker = new List<string>(),
						messageslist_fighting_default = new List<string>(),
						messageslist_fighting_badness = new List<string>(),
						messageslist_fighting_vsbadness = new List<string>();
	public MessagesList(){
		// Oeste - Cavalos de boreas
		messageslist_west.Add ("Já faz muito tempo que não vou a Verus,\n espero poder visitar a\n capital logo novamente.");
		messageslist_west.Add ("Boreas já foi a grande cidade de Sperare,\n não entendo como o rei decidiu \ncolocar sua capital em Verus.");
		messageslist_west.Add ("Já fazem décadas que o rei \nnão vem visitar Boreas, parece\n que o Oeste foi esquecido de vez.");

		// Verus - Realeza
		messageslist_verus.Add ("Verus nunca será a mesma depois \nda Guerra das Montanhas Cinzentas.");
		messageslist_verus.Add ("Andam dizendo que o rei está perdendo\n seu poder, que ele está se isolando.");
		messageslist_verus.Add ("Já faz um bom tempo que não vejo nosso\n rei Magnus andando pela capital.");
		messageslist_verus.Add ("Como meu avô diria, Sperare está\n em completa decadência, Magnus não tem\n mais a situação nas mãos.");
		messageslist_verus.Add ("Gostaria muito de rever meus parentes\n em Boreas, já fazem anos.");
		messageslist_verus.Add ("Acho que não estarei vivo quando alguém\n decidir tirar a coroa desse merda\n que se proclama rei.");
		messageslist_verus.Add ("O rei vem sofrendo duras críticas, \nmas para mim, Verus ainda\n impõe sua força.");
		messageslist_verus.Add ("Espero que nossa capital encontre\n dias melhores em um futuro próximo.");
		// Leste - Leões
		messageslist_east.Add ("Não tem um único dia sequer que o Leste\n não lembre das perdas que tivemos \nnas Guerras contra os nativos do sul.");
		messageslist_east.Add ("Há exatos vinte e sete anos, essa rochas cairam \ne selaram a passagem para o sul,\n maldita guerra, maldito rei.");
		messageslist_east.Add ("Mesmo com tanta oposição, creio que Magnus \nnão deva se preocupar, poi no fim das contas, todos\n respeitam a coroa e aquele que\nque a carrega.");
		messageslist_east.Add ("O Cemitério logo precisará\n de uma expansão...");
		messageslist_east.Add ("Viridis é meu lar e \nnunca sairei daqui.");
		messageslist_east.Add ("Acho que se meu pai estivesse vivo, morreria de \ndesgosto ao ver o que Viridis se tornou\n, uma simples \"vila\".");
		messageslist_east.Add ("Malditos saqueadores, viviem invadindo\n a cidade, espero que possam\n ter um fim doloroso.");
		messageslist_east.Add ("Viridis é apenas uma \"vila\" agora,\n como uma cidade que antes era conhecida como \nfonte de metais preciosos, hoje vive de passado.");
		// default
		messageslist_default.Add ("É verdade que o rei irá subir\n o preço dos impostos?");
		messageslist_default.Add ("O tempo está diferente,\n parece que vem tempestade\n por ai, mas não de chuva...");
		messageslist_default.Add ("Sinto falta de ir passar dias \nnos campos do sul, \nespero algum dia poder voltar\n lá... espero...");
		messageslist_default.Add ("Espero que a história dos\n novos impostos seja \napenas mais uma mentira...");
		messageslist_default.Add ("Dizem que ainda pode-se ouvir os sussuros \ndaqueles que morreram no desabamento\n da passagem real em Viridis durante \na guerra.");
		messageslist_default.Add ("Vida longa ao rei Magnus!!!");
		messageslist_default.Add ("Diz a lenda que ao anoitecer, ainda se pode ver\n as almas dos cavaleiros marchando para a\n antiga passagem para o sul durante a \nGuerra das Montanhas Cinzentas.");
		// Procurando 
		messageslist_searching.Add("Não adianta se esconder,\n eu te encontrarei.");
		messageslist_searching.Add("Hahaha, você não irá se \nesconder por muito tempo.");
		messageslist_searching.Add("A morte correrá atras de você.");
		// lutando
		messageslist_fighting_stronger.Add("Um garoto desses acha\n que ira me vencer?");
		messageslist_fighting_stronger.Add("A sua imprudência\t será sua morte.");
		messageslist_fighting_stronger.Add("Não me desafie novato.");
		messageslist_fighting_default.Add("Essa luta não sera tão facil.");
		messageslist_fighting_default.Add("Até que você não é tão ruim.");
		messageslist_fighting_default.Add("Irei acabar com sua raça.");
		messageslist_fighting_weaker.Add("Não irei deixar que me agrida.");
		messageslist_fighting_weaker.Add("Você é forte, mas não é dois.");
		messageslist_fighting_weaker.Add("Vou te mostrar que eu sou capaz.");
		messageslist_fighting_vsbadness.Add("A sua maldade não lhe trará nada de bom.");
		messageslist_fighting_badness.Add ("A sua morte me trará prazer.");
	}
}
public class Messages : MonoBehaviour {
	GameObject messageobj;
	void Start(){
		messageobj = new GameObject("message");
		messageobj.transform.SetParent(transform);
		//+new Vector3(0,3f,0)
		messageobj.transform.position = transform.position + new Vector3(0,3f,0);
		messageobj.transform.localScale = new Vector3 (0.3f,0.3f,1f);
		TextMesh textcomponent = (TextMesh)(messageobj.AddComponent (typeof(TextMesh)));
		textcomponent.alignment = TextAlignment.Center;
		textcomponent.anchor = TextAnchor.MiddleCenter;
		textcomponent.fontSize = 10;
		textcomponent.text = "";
		StartCoroutine (messagehandler(textcomponent,60,900,3,6,GetComponent<SerHumano>(),GetComponent<AIRig>().AI));
	}
	void LateUpdate(){
		messageobj.transform.eulerAngles = new Vector3(90,0,0);
	}
	IEnumerator messagehandler(TextMesh textcomponent,float mintimeplay, float maxtimeplay, float mintimepersist, float maxtimepersist,SerVivo animal, AI ai){
		while (true) {
			string faction = animal.GetFacçao ();
			yield return new WaitForSeconds (Random.Range (mintimeplay,maxtimeplay));
			MessagesList messages= new MessagesList ();
			List<string> tempm = null;
			// Frases por sentimento e região
			if (ai.WorkingMemory.GetItem<GameObject> ("EnemyGo") == null && ai.WorkingMemory.GetItem<GameObject> ("EnemyGoPersistent") == null) {
				tempm = messages.messageslist_default;
				if (faction.Equals ("cavalos"))
					tempm.AddRange(messages.messageslist_west);
				else if (faction.Equals ("leoes"))
					tempm.AddRange(messages.messageslist_east);
				else if (faction.Equals ("realeza"))
					tempm.AddRange(messages.messageslist_verus);
			} else if (ai.WorkingMemory.GetItem<GameObject> ("EnemyGoPersistent") != null && ai.WorkingMemory.GetItem<GameObject> ("EnemyGo") == null
				&& ai.WorkingMemory.GetItem<GameObject> ("EnemyGoPersistent").GetComponent<SerHumano> () != null) {
				tempm = messages.messageslist_searching;
			} else if (ai.WorkingMemory.GetItem<GameObject> ("EnemyGo") != null
					&& ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").GetComponent<SerHumano> () != null) {
				AboutAnimal enemy=null,imenemy=null;
				foreach (AboutAnimal tempanimal in ai.WorkingMemory.GetItem<List<AboutAnimal>>("aboutanimal"))
					if (ai.WorkingMemory.GetItem<GameObject> ("EnemyGo") == tempanimal.Target) {
						enemy = tempanimal;
						break;
					}
				if (animal.currentLevel - enemy.Characbase.currentLevel > 3) {
					tempm = messages.messageslist_fighting_stronger;
				} else if (enemy.Characbase.currentLevel - animal.currentLevel > 3) {
					tempm = messages.messageslist_fighting_weaker;
				} else if (enemy.isEnemyWeak (animal)) {
					tempm = messages.messageslist_fighting_badness;
				}else{
					tempm = messages.messageslist_fighting_default;
				}

				if (ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").GetComponent<AIRig> ().AI.WorkingMemory.GetItem<GameObject> ("EnemyGo") == gameObject) {
					List<AboutAnimal> listaboutanimal = ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").GetComponent<AIRig> ().AI.WorkingMemory.GetItem<List<AboutAnimal>> ("aboutanimal");

					foreach (AboutAnimal tempanimal in listaboutanimal) {
						if (gameObject == tempanimal.Target) {
							imenemy = tempanimal;
							break;
						}
					}
					if (imenemy.isEnemyWeak(ai.WorkingMemory.GetItem<GameObject> ("EnemyGo").GetComponent<SerVivo>())) {
						tempm = messages.messageslist_fighting_vsbadness;
					}
				}
			}
			if(tempm!=null)
				StartCoroutine(message(tempm[Random.Range(0,tempm.Count)],textcomponent,Random.Range (mintimepersist, maxtimepersist)));
	}
}
	IEnumerator message(string text,TextMesh textcomponent,float time){
		int startindex, endindex;
		text += "\n";
		startindex = textcomponent.text.Length;
		textcomponent.text += text;
		endindex = textcomponent.text.Length;
		yield return new WaitForSeconds(time);
		textcomponent.text = textcomponent.text.Replace(text,"");
	}
}
