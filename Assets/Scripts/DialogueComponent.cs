using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [Header("Dialogue Data")]
    [SerializeField] private DialogueDatas _dialogueDatas;
    [SerializeField] private UIDialogueController _dialogueController;

    private DialogueRow _currentRow;
    private int _currentRowIndex;
    private Pawn _currentPawn;

    [Header("Branching (for choice dialogues)")]
    [SerializeField] private int _calmStartRowIndex = -1;  // index dans rows[]
    [SerializeField] private int _panicStartRowIndex = -1; // index dans rows[]

    [Header("Choice Texts")]
    [SerializeField] private string _calmChoiceText = "Calme";
    [SerializeField] private string _panicChoiceText = "Crier";

    public void Action(Pawn currentPawn)
    {
        _currentPawn = currentPawn;
        _currentRowIndex = 0;

        if (_dialogueDatas == null || _dialogueDatas.rows == null || _dialogueDatas.rows.Length == 0)
            return;

        _currentRow = _dialogueDatas.rows[_currentRowIndex];

        _dialogueController.StartDialogue(this);
        _dialogueController.UpdateText();
    }

    public string GetDialogueText() => _currentRow.longDialogueText;
    public string GetCharacterName() => _currentRow.charactereName;

    public string GetCalmChoiceText() => _calmChoiceText;
    public string GetPanicChoiceText() => _panicChoiceText;
    public void GetNextRow()
    {
        if (_currentRow.nextRowNumber == -1)
        {
            _currentRowIndex = 0;
            _dialogueController.EndDialogue();
            return;
        }

        int nextIndex = _currentRow.nextRowNumber;
        if (nextIndex < 0 || nextIndex >= _dialogueDatas.rows.Length)
        {
            _currentRowIndex = 0;
            _dialogueController.EndDialogue();
            return;
        }

        _currentRowIndex = nextIndex;
        _currentRow = _dialogueDatas.rows[_currentRowIndex];
        _dialogueController.UpdateText();
    }
    public void ChooseBranch(bool calm)
    {
        if (_currentPawn != null)
        {
            if (calm) _currentPawn.ReduceFear(5);
            else _currentPawn.IncreaseFear(5);
        }

        int target = calm ? _calmStartRowIndex : _panicStartRowIndex;
        if (target < 0) return;
        if (_dialogueDatas == null || _dialogueDatas.rows == null) return;
        if (target >= _dialogueDatas.rows.Length) return;

        _currentRowIndex = target;
        _currentRow = _dialogueDatas.rows[_currentRowIndex];
        _dialogueController.UpdateText();
    }
}
