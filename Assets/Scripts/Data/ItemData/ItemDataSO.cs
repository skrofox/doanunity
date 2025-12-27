using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Material item", fileName = "Material data - ")]
public class ItemDataSO : ScriptableObject
{
    public string saveId { get; private set; }

    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public int maxStackSize = 1;

    [Header("Item Effect")]
    public ItemEffectDataSO itemEffect;

    [Header("Craft Details")]
    public Inventory_Item[] craftRecipe;


    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        saveId = AssetDatabase.AssetPathToGUID(path);
#endif

    }
}
