using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueDatas;
    private DialogueRow _currentRow;
    private int _currentRowIndex; // index toujours a 0 au debut
    public void Action(Pawn Currentpawn)
    {

    }

    public DialogueRow GetDialogueRow()
    {
        return _dialogueDatas.rows[_currentRowIndex];
    }
    
    public string GetDialogueText()
    {
        return _currentRow.longDialogueText;
    }

    public string GetCharacterName()
    {
        return _currentRow.charactereName;
    } 
}
