using TMPro;
using UnityEngine;

public class UIDialogueController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private TMP_Text _dialogueText;

    [Header("Buttons GameObjects")]
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _choiceMinusButton;
    [SerializeField] private GameObject _choicePlusButton;

    [Header("Choice Button Texts (TMP)")]
    [SerializeField] private TMP_Text _choiceMinusText;
    [SerializeField] private TMP_Text _choicePlusText;

    private DialogueComponent _currentDialogue;
    private bool _dialogueOpen;
    private bool _choiceDone;

    public void StartDialogue(DialogueComponent dialogue)
    {
        if (_dialogueOpen) return;

        _dialogueOpen = true;
        _choiceDone = false;
        _currentDialogue = dialogue;

        _dialoguePanel.SetActive(true);

        _choiceMinusButton.SetActive(true);
        _choicePlusButton.SetActive(true);
        _nextButton.SetActive(false);

        if (_choiceMinusText != null) _choiceMinusText.text = _currentDialogue.GetCalmChoiceText();
        if (_choicePlusText != null) _choicePlusText.text = _currentDialogue.GetPanicChoiceText();
    }

    public void UpdateText()
    {
        if (_currentDialogue == null) return;

        _characterNameText.text = _currentDialogue.GetCharacterName();
        _dialogueText.text = _currentDialogue.GetDialogueText();
    }

    // Bouton Next
    public void ChangeRow()
    {
        if (!_dialogueOpen || _currentDialogue == null) return;
        _currentDialogue.GetNextRow();
    }

    // Choix -5 (calm=true)
    public void ChooseMinus5()
    {
        if (!_dialogueOpen || _currentDialogue == null) return;
        if (_choiceDone) return;

        _choiceDone = true;

        _currentDialogue.ChooseBranch(true);

        _choiceMinusButton.SetActive(false);
        _choicePlusButton.SetActive(false);
        _nextButton.SetActive(true);
    }

    // Choix +5 (calm=false)
    public void ChoosePlus5()
    {
        if (!_dialogueOpen || _currentDialogue == null) return;
        if (_choiceDone) return;

        _choiceDone = true;

        _currentDialogue.ChooseBranch(false);

        _choiceMinusButton.SetActive(false);
        _choicePlusButton.SetActive(false);
        _nextButton.SetActive(true);
    }
    public void EndDialogue()
    {
        _dialogueOpen = false;
        _choiceDone = false;
        _currentDialogue = null;

        _dialoguePanel.SetActive(false);

        _nextButton.SetActive(false);
        _choiceMinusButton.SetActive(true);
        _choicePlusButton.SetActive(true);
    }
}
