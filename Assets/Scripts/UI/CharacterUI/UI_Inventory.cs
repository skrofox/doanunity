using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    private Inventory_Player inventory;
    private UI_EquipSlot[] uiEquipSlots;

    [SerializeField] private UI_ItemSlotParent inventorySlotParent;
    [SerializeField] private Transform uiEquipSlotParent;
    [SerializeField] private TextMeshProUGUI goldText;

    private void Awake()
    {
        uiEquipSlots = uiEquipSlotParent.GetComponentsInChildren<UI_EquipSlot>();

        inventory = FindFirstObjectByType<Inventory_Player>();
        inventory.OnInventoryChange += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        inventorySlotParent.UpdateSlots(inventory.itemList);
        UpdateEquipmentSlots();

        float randomMoney = Random.Range(0, 9999);
        goldText.text = randomMoney.ToString("N0") + "g.";
    }

    private void UpdateEquipmentSlots()
    {
        List<Inventory_EquipmentSlot> playerEquipList = inventory.equidList;

        for (int i = 0; i < uiEquipSlots.Length; i++)
        {
            var playerEquipSlot = playerEquipList[i];

            if (playerEquipSlot.HasItem() == false)
                uiEquipSlots[i].UpdateSlot(null);
            else
                uiEquipSlots[i].UpdateSlot(playerEquipSlot.equipedItem);
        }
    }

}
