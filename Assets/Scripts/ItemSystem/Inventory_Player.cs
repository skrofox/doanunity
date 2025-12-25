using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Entity_Stats playerStats;
    public List<Inventory_EquipmentSlot> equidList;

    protected override void Awake()
    {
        base.Awake();
        playerStats = GetComponent<Entity_Stats>();
    }

    public void TryEquipItem(Inventory_Item item)
    {
        var inventoryItem = FindItem(item.itemData);
        var matchingSlots = equidList.FindAll(slot => slot.slotType == item.itemData.itemType);

        //trang bi tu dong vao o trong
        foreach (var slot in matchingSlots)
        {
            if (slot.HasItem() == false)
            {
                EquipItem(inventoryItem, slot);
                return;
            }
        }

        //khong co o trong. thay cai dau tien bang cai moi nhat
        var slotToReplace = matchingSlots[0];
        var itemToUnequip = slotToReplace.equipedItem;

        EquipItem(inventoryItem, slotToReplace);
        UnequipItem(itemToUnequip);
    }

    private void EquipItem(Inventory_Item itemToEquip, Inventory_EquipmentSlot slot)
    {
        slot.equipedItem = itemToEquip;
        slot.equipedItem.AddModifiers(playerStats);

        RemoveItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnEquip)
    {
        if (CanAddItem() == false)
        {
            Debug.Log("No Space");
            return;
        }

        foreach (var slot in equidList)
        {
            if (slot.equipedItem == itemToUnEquip)
            {
                slot.equipedItem = null;
                break;
            }
        }

        itemToUnEquip.RemoveModifiers(playerStats);
        AddItem(itemToUnEquip);
    }
}
