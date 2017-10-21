using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Geren
{

	public class Gerenciardor : MonoBehaviour {
		public static string a;
		Text text;

		void Awake(){
			text = GetComponent<Text> ();

		}

		void Update(){
			text.text = "";
		}
	}
}
