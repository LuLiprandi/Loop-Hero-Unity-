using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueDatas;
    [SerializeField] private UIDialogueController _dialogueController;

    private DialogueRow _currentRow;
    private int _currentRowIndex;
    private Pawn _currentPawn;

    public void Action(Pawn currentPawn)
    {
        _currentPawn = currentPawn;
        _currentRowIndex = 0;
        _currentRow = GetDialogueRow();
        Debug.Log($"START Dialogue: index={_currentRowIndex} name={_currentRow.charactereName} text={_currentRow.longDialogueText}");
        _dialogueController.StartDialogue(this);
        _dialogueController.UpdateText();
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

    public void ApplyFearChoice(int fearDelta)
    {
        if (_currentPawn == null) return;

        if (fearDelta > 0) _currentPawn.IncreaseFear(fearDelta);
        else _currentPawn.ReduceFear(-fearDelta);
    }
}
