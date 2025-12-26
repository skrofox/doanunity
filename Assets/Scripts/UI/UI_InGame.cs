using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;
    private Inventory_Player inventory;
    private UI_SkillSlot[] skillSlots;

    [SerializeField] private RectTransform healthRect;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Quick Item Slots")]
    [SerializeField] private float yOffsetQuickItemParent = 150;
    [SerializeField] private Transform quickItemOptionParent;
    private UI_QuickItemSlotOption[] quickItemOption;
    private UI_QuickItemSlot[] quickItemslots;

    private void Start()
    {
        quickItemslots = GetComponentsInChildren<UI_QuickItemSlot>();

        player = FindFirstObjectByType<Player>();
        player.health.OnHealthUpdate += UpdateHealthBar;

        skillSlots = GetComponentsInChildren<UI_SkillSlot>(true);
        inventory = player.inventory;
        inventory.OnQuickSlotUsed += UpdateQuickSlotsUI;
    }

    public void UpdateQuickSlotsUI(int slotNumber, Inventory_Item itemInSlot)
    {
        quickItemslots[slotNumber].UpdateQuickSlotUI(itemInSlot);
    }

    public void OpenQuickItemOptions(UI_QuickItemSlot quickItemSlot, RectTransform targetRect)
    {
        if (quickItemOption == null)
        {
            quickItemOption = quickItemOptionParent.GetComponentsInChildren<UI_QuickItemSlotOption>(true);
        }

        List<Inventory_Item> consumables = inventory.itemList.FindAll(item => item.itemData.itemType == ItemType.Consumable);

        for (int i = 0; i < quickItemOption.Length; i++)
        {
            if (i < consumables.Count)
            {
                quickItemOption[i].gameObject.SetActive(true);
                quickItemOption[i].SetupOption(quickItemSlot, consumables[i]);
            }
            else
                quickItemOption[i].gameObject.SetActive(false);
        }

        quickItemOptionParent.position = targetRect.position + Vector3.up * yOffsetQuickItemParent;
    }

    public void HideQuickItemOptions() => quickItemOptionParent.position = new Vector3(0, 9999);

    public UI_SkillSlot GetSkillSlot(SkillType skillType)
    {
        foreach (var slot in skillSlots)
        {
            if (slot.skillType == skillType)
            {
                slot.gameObject.SetActive(true);
                return slot;
            }
        }

        return null;
    }

    private void UpdateHealthBar()
    {
        float currentHealth = Mathf.RoundToInt(player.health.GetCurrentHealth());
        float maxHealth = player.stats.GetMaxHealth();
        float diffrentSize = Mathf.Abs(maxHealth - healthRect.sizeDelta.x);

        if (diffrentSize > .1f)
            healthRect.sizeDelta = new Vector2(maxHealth, healthRect.sizeDelta.y);

        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = player.health.GetHealthPercent();
    }
}

