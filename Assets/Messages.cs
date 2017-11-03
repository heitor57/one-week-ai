using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
		StartCoroutine (messagehandler(textcomponent,60,120,3,6));
	}
	IEnumerator messagehandler(TextMesh textcomponent,float mintimeplay, float maxtimeplay, float mintimepersist, float maxtimepersist){
		while (true) {
			yield return new WaitForSeconds (Random.Range (mintimeplay,maxtimeplay));
			StartCoroutine(message("xx",textcomponent,Random.Range (mintimepersist, maxtimepersist)));
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
