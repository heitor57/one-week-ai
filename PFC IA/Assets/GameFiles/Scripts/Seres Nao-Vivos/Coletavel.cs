using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour {

	public int id;
	private Item item;
	private Inventory _inventory;
	private GameObject _player;

	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
		if (_player != null) {
			_inventory = _player.GetComponent<PlayerInventory> ().inventory.GetComponent<Inventory> ();
			item = _inventory.GetDATA ().getItemByID (id);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Coletei(){
		if (_inventory != null) {
			//bool check = _inventory.checkIfItemAllreadyExist(item.itemID,item.itemValue);
			//if (check)
				//Destroy(this.gameObject);
			//else
				if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height))
			{
				_inventory.addItemToInventory(item.itemID);
				_inventory.updateItemList();
				_inventory.stackableSettings();
				Destroy(this.gameObject);
			}
		}
	}
}
