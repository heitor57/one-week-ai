using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMorte : MonoBehaviour {
	[SerializeField]GameObject Tela;
	public bool morto = false;

	// Use this for initialization
	void Start () {
		Tela.SetActive(false);
		morto = false;
		Time.timeScale=1;

		
	}
	
	// Update is called once per frame
	void Update () {
		if (morto) {
			Morto ();
		} else {
			Tela.SetActive (false);
		}
	}

	public void Morto(){
		Tela.SetActive(true);
		Screen.lockCursor=false;
		Time.timeScale=0;
		StartCoroutine (wait ());
		SceneManager.LoadScene("Loader");



	}
	public IEnumerator wait(){
		yield return new WaitForSeconds (4.0f);

	}

}
