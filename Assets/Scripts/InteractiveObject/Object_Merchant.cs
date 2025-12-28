using UnityEngine;

public class Object_Merchant : Object_NPC, IInteractable
{
    [Header("Dialogue & Quest")]
    [SerializeField] private DialogueLineSO firstDialogueLine;

    public override void Interact()
    {
        base.Interact();


        ui.OpenDialogueUI(firstDialogueLine);
    }


}
