using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoreDatabase{
	Levinho,
	MiniMercado_Saida,
	CombiniSA //Loja Padrão(Cena única) pra todo o mapa
}
public class Stores : BuildingsBase {

	public BasicStats basicStats;
	/*public override void Enter(StoreDatabase storeX){
		switch storeX{
		case HouseDatabase.CombiniSA{
				//salva cena anterior
				SceneManager.LoadScene("CombiniSA");
				//Someone.Dialogs.PlayDialog("Bem-Vindo!");
			}
			break;
		}
	}*/
}
