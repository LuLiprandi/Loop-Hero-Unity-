using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [Header("Runtime data (set by DialogueSwitcher)")]
    [SerializeField] private DialogueDatas _dialogueDatas;
    [SerializeField] private UIDialogueController _ui;

    [Header("Branching (set by DialogueSwitcher)")]
    [SerializeField] private int _calmStartRowIndex = -1;
    [SerializeField] private int _panicStartRowIndex = -1;

    [Header("Choice Texts (set by DialogueSwitcher)")]
    [SerializeField] private string _calmChoiceText = "Calme";
    [SerializeField] private string _panicChoiceText = "Crier";

    private Pawn _currentPawn;
    private int _currentRowIndex;
    private DialogueRow _currentRow;

    // --- Setters used by DialogueSwitcher ---
    public void SetDatas(DialogueDatas datas)
    {
        _dialogueDatas = datas;
    }

    public void SetChoiceTexts(string calmText, string panicText)
    {
        _calmChoiceText = calmText;
        _panicChoiceText = panicText;
    }

    public void SetBranching(int calmStartRowIndex, int panicStartRowIndex)
    {
        _calmStartRowIndex = calmStartRowIndex;
        _panicStartRowIndex = panicStartRowIndex;
    }

    // --- IActionnable ---
    public void Action(Pawn currentPawn)
    {
        _currentPawn = currentPawn;

        _currentRowIndex = 0;
        _currentRow = GetRow(_currentRowIndex);

        _ui.StartDialogue(this);
    }

    private DialogueRow GetRow(int index)
    {
        if (_dialogueDatas == null || _dialogueDatas.rows == null || _dialogueDatas.rows.Length == 0)
            return default;

        index = Mathf.Clamp(index, 0, _dialogueDatas.rows.Length - 1);
        return _dialogueDatas.rows[index];
    }

    public string GetDialogueText()
    {
        return _currentRow.longDialogueText;
    }

    public string GetCharacterName()
    {
        return _currentRow.charactereName;
    }

    public bool HasChoice()
    {
        // Choice exists only if both indices are set correctly
        return _calmStartRowIndex >= 0 && _panicStartRowIndex >= 0;
    }

    public string GetMinusChoiceText() => _calmChoiceText;
    public string GetPlusChoiceText() => _panicChoiceText;

    public void ChooseBranch(bool calm)
    {
        // apply fear
        if (_currentPawn != null)
        {
            if (calm) _currentPawn.ReduceFear(5);
            else _currentPawn.IncreaseFear(5);
        }

        int target = calm ? _calmStartRowIndex : _panicStartRowIndex;

        // safety
        if (_dialogueDatas == null || _dialogueDatas.rows == null) return;
        if (target < 0 || target >= _dialogueDatas.rows.Length) return;

        _currentRowIndex = target;
        _currentRow = GetRow(_currentRowIndex);

        _ui.RefreshUI();
    }

    public void NextRow()
    {
        if (_dialogueDatas == null || _dialogueDatas.rows == null) return;

        if (_currentRow.nextRowNumber == -1)
        {
            // reset for next time
            _currentRowIndex = 0;
            _currentRow = GetRow(_currentRowIndex);

            _ui.EndDialogue(this);
            return;
        }

        _currentRowIndex = _currentRow.nextRowNumber;
        _currentRow = GetRow(_currentRowIndex);

        _ui.RefreshUI();
    }
}
