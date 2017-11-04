using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MessagesList{
	public List<string> messageslist_west=new List<string>(),
						messageslist_verus=new List<string>(),
						messageslist_east =new List<string>();
	public MessagesList(){
		// Oeste - Cavalos de boreas
		messageslist_west.Add ("Já faz muito tempo que não vou a Verus,\n espero poder visitar a\n capital logo novamente.");
		messageslist_west.Add ("É verdade que o rei irá subir\n o preço dos impostos?");
		messageslist_west.Add ("Boreas já foi a grande cidade de Sperare,\n não entendo como o rei decidiu \ncolocar sua capital em Verus.");
		messageslist_west.Add ("O tempo está diferente,\n parece que vem tempestade\n por ai, mas não de chuva...");
		messageslist_west.Add ("Já fazem décadas que o rei \nnão vem visitar Boreas, parece\n que o Oeste foi esquecido de vez.");
		messageslist_west.Add ("Sinto falta de ir passar dias \nnos campos do sul, \nespero algum dia poder voltar\n lá... espero...");
		messageslist_west.Add ("Espero que a história dos\n novos impostos seja \napenas mais uma mentira...");
		// Verus - Realeza
		messageslist_verus.Add ("Verus nunca será a mesma depois \nda Guerra das Montanhas Cinzentas.");
		messageslist_verus.Add ("Andam dizendo que o rei está perdendo\n seu poder, que ele está se isolando.");
		messageslist_verus.Add ("Já faz um bom tempo que não vejo nosso\n rei Magnus andando pela capital.");
		messageslist_verus.Add ("Dizem que ainda pode-se ouvir os sussuros \ndaqueles que morreram no desabamento\n da passagem real em Viridis durante \na guerra.");
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
		messageslist_east.Add ("Vida longa ao rei Magnus!!!");
		messageslist_east.Add ("Diz a lenda que ao anoitecer, ainda se pode ver\n as almas dos cavaleiros marchando para a\n antiga passagem para o sul durante a \nGuerra das Montanhas Cinzentas.");
		messageslist_east.Add ("Acho que se meu pai estivesse vivo, morreria de \ndesgosto ao ver o que Viridis se tornou\n, uma simples \"vila\".");
		messageslist_east.Add ("Malditos saqueadores, viviem invadindo\n a cidade, espero que possam\n ter um fim doloroso.");
		messageslist_east.Add ("iridis é apenas uma \"vila\" agora,\n como uma cidade que antes era conhecida como \nfonte de metais preciosos, hoje vive de passado.");

	}
}
public class Messages : MonoBehaviour {
	void Start(){
		GameObject obj = new GameObject("message");
		obj.transform.parent = transform;
		obj.transform.localPosition = new Vector3(0,3f,0);
		obj.transform.localScale = new Vector3 (0.3f,0.3f,1f);
		obj.transform.eulerAngles = new Vector3 (90f,0f,0f);
		TextMesh textcomponent = (TextMesh)(obj.AddComponent (typeof(TextMesh)));
		textcomponent.alignment = TextAlignment.Center;
		textcomponent.anchor = TextAnchor.MiddleCenter;
		textcomponent.fontSize = 10;
		textcomponent.text = "";
		StartCoroutine (messagehandler(textcomponent,60,900,3,6,GetComponent<SerVivo>().GetFacçao()));
	}
	IEnumerator messagehandler(TextMesh textcomponent,float mintimeplay, float maxtimeplay, float mintimepersist, float maxtimepersist,string faction){
		while (true) {
			yield return new WaitForSeconds (Random.Range (mintimeplay,maxtimeplay));
			MessagesList messages= new MessagesList ();
			List<string> tempm = null;
			if(faction.Equals("cavalos"))
				tempm = messages.messageslist_west;
			else if(faction.Equals("leoes"))
				tempm = messages.messageslist_east;
			else if(faction.Equals("realeza"))
				tempm = messages.messageslist_verus;
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
