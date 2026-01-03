using UnityEngine;

[System.Serializable]
public struct DialogueRow
{
    public int rowNumber;
    public string charactereName;
    [TextArea(2, 5)] public string longDialogueText;
    public int nextRowNumber; // -1 = fin
}

[CreateAssetMenu(fileName = "DialogueDatas", menuName = "Scriptable Objects/DialogueDatas")]
public class DialogueDatas : ScriptableObject
{
    public DialogueRow[] rows;
}
