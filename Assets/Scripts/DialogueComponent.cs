using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueDatas;
    private DialogueRow _currentRow;
    private int _currentRowIndex; // index toujours a 0 au debut
    [SerializeField] private UIDialogueController _dialogueController;
    public void Action(Pawn Currentpawn)
    {
        _currentRow = GetDialogueRow(); 
        _dialogueController.StartDialogue(this);
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

    public void GetNextRow()
    {
        if (_currentRow.nextRowNumber == -1)
        {
            _currentRowIndex = 0;
            _dialogueController.EndDialogue();
        }
        else
        {
            _currentRowIndex = _currentRow.nextRowNumber;
            _currentRow = GetDialogueRow();
            _dialogueController.UpdateText();
        }
    }

}
