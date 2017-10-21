using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BasicInfoChar{
	public BasicStats baseInfo;
	public TypeCharacter typeChar;
}

public enum TypeCharacter{
	Cidadao = 0,
	Ladrao = 1,
	Campones = 2,
	Burgues = 3,
	Arqueiro = 4,
	Guerreiro = 5,
	Nobre = 6
}

public abstract class PlayerStatsController : Destrutiveis {

	//public static PlayerStatsController instance;

	public int xpMultiply = 1;
	public float xpThisLevel = 100;
	public float difficultFactor = 1.5f;	
	public List<BasicInfoChar> baseInfoChars;

	// Use this for initialization
	//public void Start () {
		//instance = this;
		//DontDestroyOnLoad(gameObject);
	//}
	
	// Update is called once per frame


	/*public static void AddXP(float xpAdd){
		float newXP = (GetCurrentXP() + xpAdd) * PlayerStatsController.instance.xpMultiply;
		while(newXP>=GetNextXP()){
			AddLevel();
			newXP -= GetNextXP ();
		}
		PlayerPrefs.SetFloat ("currentXP", newXP);
	}
*/
	public static float GetCurrentXP(){
		return PlayerPrefs.GetFloat ("currentXp");
	}
	public static int GetCurrentLevel(){
		return PlayerPrefs.GetInt ("currentLevel");
	}
	/*public static float GetNextXP(){
		return (
			PlayerStatsController.instance.xpThisLevel
			*(GetCurrentLevel()+1)
			*PlayerStatsController.instance.difficultFactor
		);
	}
*/
	public static void AddLevel(){
		int levelUp = GetCurrentLevel () + 1;
		PlayerPrefs.SetInt ("currentLevel",levelUp);
	}
	public static TypeCharacter GetTypeCharacter(){
		int id = PlayerPrefs.GetInt("TypeCharacter");
		if (id == 0) {
			return TypeCharacter.Cidadao;
		} else if (id == 1) {
			return TypeCharacter.Ladrao;
		} else if (id == 2) {
			return TypeCharacter.Campones;
		} else if (id == 3) {
			return TypeCharacter.Burgues;
		} else if (id == 4) {
			return TypeCharacter.Arqueiro;
		} else if (id == 5) {
			return TypeCharacter.Guerreiro;
		} else if (id == 6) {
			return TypeCharacter.Nobre;
		}
		return TypeCharacter.Cidadao;
	}
	public static void SetTypeCharacter(TypeCharacter newType){
		PlayerPrefs.SetInt ("TypeCharacter", (int)newType);

	}
	public BasicStats GetBasicStats(TypeCharacter type){
		foreach (BasicInfoChar info in baseInfoChars) {
			if(info.typeChar == type)
				return info.baseInfo;
		}
		return baseInfoChars[0].baseInfo;
	}
	/*void OnGUI(){
		GUI.Label (new Rect (0, 0, 200, 50), "XP Atual = " + GetCurrentXP ());
		GUI.Label (new Rect (0, 15, 200, 50), "Level Atual = " + GetCurrentLevel ());
		//GUI.Label (new Rect (0, 30, 200, 50), "Level Up em = " + GetNextXP ());
	}*/
}