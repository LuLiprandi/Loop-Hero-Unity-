using UnityEngine;

public class VictoryDialogueCell : Cell
{
    [Header("Victory Dialogue Settings")]
    [SerializeField] private GameObject _victoryWidget;

    private DialogueComponent _dialogueComponent;
    private UIDialogueController _uiDialogueController;

    private void Awake()
    {
        _dialogueComponent = GetComponent<DialogueComponent>();
        _uiDialogueController = FindFirstObjectByType<UIDialogueController>();
    }

    public override void Activate(Pawn pawn)
    {
        base.Activate(pawn);

        if (_dialogueComponent != null && _uiDialogueController != null)
        {
            _uiDialogueController.OnDialogueClosed += OnDialogueCompleted;
        }
    }

    private void OnDialogueCompleted(DialogueComponent dialogue)
    {
        if (dialogue == _dialogueComponent)
        {
            _uiDialogueController.OnDialogueClosed -= OnDialogueCompleted;
            ShowVictoryWidget();
        }
    }

    private void ShowVictoryWidget()
    {
        if (_victoryWidget != null)
        {
            _victoryWidget.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        if (_uiDialogueController != null)
        {
            _uiDialogueController.OnDialogueClosed -= OnDialogueCompleted;
        }
    }
}
