using UnityEngine;


[CreateAssetMenu(fileName = "Line - ", menuName = "RPG Setup/Dialog Data/New Line Data")]
public class DialogueLineSO : ScriptableObject
{
    [Header("Dialogue Info")]
    public string dialogueGroupName;
    public DialogSpeakerSO speaker;

    [Header("Text options")]
    [TextArea] public string[] textLine;

    public string GetRandomLine()
    {
        return textLine[Random.Range(0, textLine.Length)];
    }
}
