using System.Collections.Generic;
using UnityEngine;

public class Inventory_Player : Inventory_Base
{
    private Player player;
    public List<Inventory_EquipmentSlot> equidList;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<Player>();
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
        float savedHealthPercent = player.health.GetHealthPercent();

        slot.equipedItem = itemToEquip;
        slot.equipedItem.AddModifiers(player.stats);
        slot.equipedItem.AddItemEffect(player);

        player.health.SetHealthToPercent(savedHealthPercent);
        RemoveItem(itemToEquip);
    }

    public void UnequipItem(Inventory_Item itemToUnEquip)
    {
        if (CanAddItem() == false)
        {
            Debug.Log("No Space");
            return;
        }

        float savedHealthPercent = player.health.GetHealthPercent();
        var slotToUnEquip = equidList.Find(slot => slot.equipedItem == itemToUnEquip);

        if (slotToUnEquip != null)
            slotToUnEquip.equipedItem = null;

        itemToUnEquip.RemoveModifiers(player.stats);
        itemToUnEquip.RemoveItemEffect();

        player.health.SetHealthToPercent(savedHealthPercent);
        AddItem(itemToUnEquip);
    }
}
