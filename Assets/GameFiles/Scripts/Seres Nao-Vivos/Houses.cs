using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum HouseDatabase{
	House1,
	OrionHome,
	NorthLieutenantHouse
}
public class Houses : BuildingsBase {

	public BasicStats basicStats;
	/*public override void Enter(HouseDatabase houseX){
		switch houseX{
		case HouseDatabase.OrionHome{
				//salva cena anterior
				SceneManager.LoadScene("Home");
				//PlayerBehaviour.Dialogs.PlayDialog("Home");
			}
			break;
		}
	}*/
}
