using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MessagesList{
	public List<string> messageslist_west=new List<string>();
	public MessagesList(){
		messageslist_west.Add ("Já faz muito tempo que não vou a Verus,\n espero poder visitar a\n capital logo novamente.");
		messageslist_west.Add ("É verdade que o rei irá subir\n o preço dos impostos?");
		messageslist_west.Add ("Boreas já foi a grande cidade de Sperare,\n não entendo como o rei decidiu \ncolocar sua capital em Verus.");
		messageslist_west.Add ("O tempo está diferente,\n parece que vem tempestade\n por ai, mas não de chuva...");
		messageslist_west.Add ("Já fazem décadas que o rei \nnão vem visitar Boreas, parece\n que o Oeste foi esquecido de vez.");
		messageslist_west.Add ("Sinto falta de ir passar dias \nnos campos do sul, \nespero algum dia poder voltar\n lá... espero...");
		messageslist_west.Add ("Espero que a história dos\n novos impostos seja \napenas mais uma mentira...");

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
		StartCoroutine (messagehandler(textcomponent,10,20,3,6));
	}
	IEnumerator messagehandler(TextMesh textcomponent,float mintimeplay, float maxtimeplay, float mintimepersist, float maxtimepersist){
		while (true) {
			yield return new WaitForSeconds (Random.Range (mintimeplay,maxtimeplay));
			MessagesList messages= new MessagesList ();
			StartCoroutine(message(messages.messageslist_west[Random.Range(0,messages.messageslist_west.Count)],textcomponent,Random.Range (mintimepersist, maxtimepersist)));
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
