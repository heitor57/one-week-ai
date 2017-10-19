using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterSystem;
    public GameObject craftSystem;
    private Inventory craftSystemInventory;
    private CraftSystem cS;
    private Inventory mainInventory;
    private Inventory characterSystemInventory;
    private Tooltip toolTip;
	public GameObject EquipedWeapon;
    private InputManager inputManagerDatabase;

    public GameObject HPCanvas;

    Text hpText;
    Text stmText;
	Text xpText;
	Image xpImage;
    Image hpImage;
    Image stmImage;

	//Passar a pegar o que o player usa realmente \|/
	float maxHealth = 0 ;
	float maxStm = 0;
	float maxXP = 100;// Quanto pro proximo nivel, tem que alterar de acordo com o nivel atual
    float maxDamage = 0;
    float maxArmor = 0;

	public float currentHealth = 0;
	float currentLevel=1;	
	float currentXP = 0;
	float currentStamina = 0;
    float currentDamage = 0;
    float currentArmor = 0;

    int normalSize = 3;

    public void OnEnable()
    {
        Inventory.ItemEquip += OnBackpack;
        Inventory.UnEquipItem += UnEquipBackpack;

        Inventory.ItemEquip += OnGearItem;
        Inventory.ItemConsumed += OnConsumeItem;
        Inventory.UnEquipItem += OnUnEquipItem;

		Inventory.ItemEquip += EquipWeapon;
        Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnBackpack;
        Inventory.UnEquipItem -= UnEquipBackpack;

        Inventory.ItemEquip -= OnGearItem;
        Inventory.ItemConsumed -= OnConsumeItem;
        Inventory.UnEquipItem -= OnUnEquipItem;

        Inventory.UnEquipItem -= UnEquipWeapon;
        Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(Item item)
    {
		if (EquipedWeapon != null)
		{
			UnEquipWeapon(item);
		}
		//EquipedWeapon = (GameObject)Instantiate(item.itemModel, EquipedWeapon.transform.position, EquipedWeapon.transform.rotation);
		//EquipedWeapon.transform.SetParent(EquipedWeapon.transform);
		for (int i = 0; i < item.itemAttributes.Count; i++)
		{
			if (item.itemAttributes[i].attributeName == "Health")
				maxHealth += item.itemAttributes[i].attributeValue;
			if (item.itemAttributes[i].attributeName == "Mana")
				maxStm += item.itemAttributes[i].attributeValue;
			if (item.itemAttributes[i].attributeName == "Armor")
				maxArmor += item.itemAttributes[i].attributeValue;
			if (item.itemAttributes[i].attributeName == "Damage")
				maxDamage += item.itemAttributes[i].attributeValue;
		}
		if (HPCanvas != null)
		{
			UpdateStmBar();
			UpdateHPBar();
			UpdateXPBar();
		}

		if (item.itemType == ItemType.Weapon) {
			
			GameObject.Find ("CpWeapon").GetComponent<changeEquip> ().cEquip (item.itemName);
		}
    }

    void UnEquipWeapon(Item item)
	{
		if (item.itemType == ItemType.Weapon)
		{
			
			GameObject.Find ("CpWeapon").GetComponent<changeEquip> ().destroi ();
		}
    }

    void OnBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
        {
            for (int i = 0; i < item.itemAttributes.Count; i++)
            {
                if (mainInventory == null)
                    mainInventory = inventory.GetComponent<Inventory>();
                mainInventory.sortItems();
                if (item.itemAttributes[i].attributeName == "Slots")
                    changeInventorySize(item.itemAttributes[i].attributeValue);
            }
        }
    }

    void UnEquipBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
            changeInventorySize(normalSize);
    }

    void changeInventorySize(int size)
    {
        dropTheRestItems(size);

        if (mainInventory == null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (size == 3)
        {
            mainInventory.width = 3;
            mainInventory.height = 1;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        if (size == 6)
        {
            mainInventory.width = 3;
            mainInventory.height = 2;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 12)
        {
            mainInventory.width = 4;
            mainInventory.height = 3;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 16)
        {
            mainInventory.width = 4;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 24)
        {
            mainInventory.width = 6;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
    }

    void dropTheRestItems(int size)
    {
        if (size < mainInventory.ItemsInInventory.Count)
        {
            for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
            {
                GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
                dropItem.AddComponent<PickUpItem>();
                dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            }
        }
    }

    void Start()
    {	
		currentLevel = GetComponent<PlayerBehaviour> ().currentLevel;
		maxXP = GetComponent<PlayerBehaviour> ().xpThisLevel;
		maxHealth = GetComponent<PlayerBehaviour> ().basicStats.GetLife ();
		maxStm = GetComponent<PlayerBehaviour> ().basicStats.GetStm ();

        if (HPCanvas != null)
        {
            hpText = HPCanvas.transform.GetChild(1).GetChild(0).GetComponent<Text>();
            stmText = HPCanvas.transform.GetChild(2).GetChild(0).GetComponent<Text>();
			xpText = HPCanvas.transform.GetChild(3).GetChild(0).GetComponent<Text>();

			xpImage = HPCanvas.transform.GetChild(3).GetComponent<Image>();
            hpImage = HPCanvas.transform.GetChild(1).GetComponent<Image>();
            stmImage = HPCanvas.transform.GetChild(2).GetComponent<Image>();

            UpdateHPBar();
            UpdateStmBar();
			UpdateXPBar();
        }

        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");

        if (craftSystem != null)
            cS = craftSystem.GetComponent<CraftSystem>();

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (characterSystem != null)
            characterSystemInventory = characterSystem.GetComponent<Inventory>();
        if (craftSystem != null)
            craftSystemInventory = craftSystem.GetComponent<Inventory>();
    }

    void UpdateHPBar()
    {
		currentHealth=GetComponent<PlayerBehaviour>().GetVida();
        hpText.text = (currentHealth + "/" + maxHealth);
        float fillAmount = currentHealth / maxHealth;
		hpImage.fillAmount = fillAmount;
    }

    void UpdateStmBar()
    {
		currentStamina=GetComponent<PlayerBehaviour>().GetSTM();
		stmText.text = ((int)currentStamina + "/" + maxStm);
        float fillAmount = currentStamina / maxStm;
		stmImage.fillAmount = fillAmount;
    }

	void UpdateXPBar()
	{
		currentXP=GetComponent<PlayerBehaviour>().currentXP;
		xpText.text = (currentXP + "/" + maxXP);
		float fillAmount = currentXP / maxXP;
		xpImage.fillAmount = fillAmount;
	}

    public void OnConsumeItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
            {
				if ((currentHealth + item.itemAttributes [i].attributeValue) > maxHealth) {
					currentHealth = maxHealth;
					GetComponent<PlayerBehaviour> ().SetVida (currentHealth);
				} else {
					currentHealth += item.itemAttributes [i].attributeValue;
					GetComponent<PlayerBehaviour> ().SetVida (currentHealth);
				}
            }
            if (item.itemAttributes[i].attributeName == "Mana")
            {
                if ((currentStamina + item.itemAttributes[i].attributeValue) > maxStm)
                    currentStamina = maxStm;
                else
                    currentStamina += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
                    currentArmor = maxArmor;
                else
                    currentArmor += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
                    currentDamage = maxDamage;
                else
                    currentDamage += item.itemAttributes[i].attributeValue;
            }
        }
        if (HPCanvas != null)
        {
            UpdateStmBar();
            UpdateHPBar();
			UpdateXPBar();
        }
    }

    public void OnGearItem(Item item)
    {
		if (item.itemType == ItemType.Head) {
			GameObject.Find ("Estilo").GetComponent<changeEquip> ().cEquip (item.itemName);
		}else if (item.itemType == ItemType.Trouser) {
			GameObject.Find ("Calca").GetComponent<changeEquip> ().cEquip (item.itemName);
		}else if (item.itemType == ItemType.Chest) {
			GameObject.Find ("Trapezio").GetComponent<changeEquip> ().cEquip (item.itemName);
		}
		Debug.Log (item.itemType);
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxStm += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage += item.itemAttributes[i].attributeValue;
        }
        if (HPCanvas != null)
        {
            UpdateStmBar();
            UpdateHPBar();
			UpdateXPBar();
        }

    }

    public void OnUnEquipItem(Item item)
    {
		if (item.itemType == ItemType.Head) {
			GameObject.Find ("Estilo").GetComponent<changeEquip> ().destroi ();
		}else if (item.itemType == ItemType.Trouser) {
			GameObject.Find ("Calca").GetComponent<changeEquip> ().destroi ();
		}else if (item.itemType == ItemType.Chest) {
			GameObject.Find ("Trapezio").GetComponent<changeEquip> ().destroi ();
		}
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxStm -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage -= item.itemAttributes[i].attributeValue;
        }
        if (HPCanvas != null)
        {
            UpdateStmBar();
            UpdateHPBar();
			UpdateXPBar();
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputManagerDatabase.CharacterSystemKeyCode))
        {
            if (!characterSystem.activeSelf)
            {
                characterSystemInventory.openInventory();
				Screen.lockCursor=false;
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                characterSystemInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                mainInventory.openInventory();
				Screen.lockCursor=false;
            }
            else
            {
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                mainInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
        {
            if (!craftSystem.activeSelf)
                craftSystemInventory.openInventory();
            else
            {
                if (cS != null)
                    cS.backToInventory();
                if (toolTip != null)
                    toolTip.deactivateTooltip();
                craftSystemInventory.closeInventory();
            }
        }
		if ((!inventory.activeSelf) && (!characterSystem.activeSelf)) {
			Screen.lockCursor=true;
		}

		UpdateStmBar ();
		UpdateHPBar ();
		UpdateXPBar ();
    }

}
