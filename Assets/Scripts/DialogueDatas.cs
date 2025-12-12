using UnityEngine;

[System.Serializable]
public struct DialogueRow
    {
    public string charactereName;
    public string longDialogueText;
    public Sprite nextRowNumber;
}
[CreateAssetMenu(fileName = "DialogueDatas", menuName = "Scriptable Objects/DialogueDatas")]
public class DialogueDatas : ScriptableObject
{
    public DialogueRow[] rows;
}
