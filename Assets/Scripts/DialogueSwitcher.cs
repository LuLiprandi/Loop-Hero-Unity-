using UnityEngine;

public class DialogueSwitcher : MonoBehaviour, IActionnable
{
    [Header("References")]
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private UIDialogueController _dialogueController;
    [SerializeField] private DialogueComponent _runtimeDialogue;

    [Header("Dialogue ID (unique)")]
    [SerializeField] private int _dialogueId = 1;

    [Header("Datas")]
    [SerializeField] private DialogueDatas _main;
    [SerializeField] private DialogueDatas _after;

    [Header("MAIN choice texts")]
    [SerializeField] private string _mainCalmChoiceText = "Calme";
    [SerializeField] private string _mainPanicChoiceText = "Crier";

    [Header("MAIN branch start index")]
    [SerializeField] private int _mainCalmStartRowIndex = -1;
    [SerializeField] private int _mainPanicStartRowIndex = -1;

    [Header("AFTER choice texts")]
    [SerializeField] private string _afterCalmChoiceText = "Calme";
    [SerializeField] private string _afterPanicChoiceText = "Crier";

    [Header("AFTER branch start index")]
    [SerializeField] private int _afterCalmStartRowIndex = -1;
    [SerializeField] private int _afterPanicStartRowIndex = -1;

    private bool _playingMain;

    private void OnEnable()
    {
        if (_dialogueController != null)
            _dialogueController.OnDialogueClosed += OnDialogueClosed;
    }

    private void OnDisable()
    {
        if (_dialogueController != null)
            _dialogueController.OnDialogueClosed -= OnDialogueClosed;
    }

    public void Action(Pawn currentPawn)
    {
        if (_playerDatas == null || _runtimeDialogue == null) return;

        bool done = _playerDatas.IsDialogueDone(_dialogueId);

        if (!done)
        {
            _playingMain = true;
            _runtimeDialogue.SetDatas(_main);
            _runtimeDialogue.SetChoiceTexts(_mainCalmChoiceText, _mainPanicChoiceText);
            _runtimeDialogue.SetBranching(_mainCalmStartRowIndex, _mainPanicStartRowIndex);
        }
        else
        {
            _playingMain = false;
            _runtimeDialogue.SetDatas(_after);
            _runtimeDialogue.SetChoiceTexts(_afterCalmChoiceText, _afterPanicChoiceText);
            _runtimeDialogue.SetBranching(_afterCalmStartRowIndex, _afterPanicStartRowIndex);
        }

        _runtimeDialogue.Action(currentPawn);
    }

    private void OnDialogueClosed(DialogueComponent dialogue)
    {
       
        if (!_playingMain) return;
        if (dialogue != _runtimeDialogue) return;

        _playerDatas.SetDialogueDone(_dialogueId, true);
    }
}
