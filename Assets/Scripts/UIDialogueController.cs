using System;
using TMPro;
using UnityEngine;

public class UIDialogueController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private TMP_Text _dialogueText;

    [Header("Buttons (GameObjects)")]
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _choiceMinusButton;
    [SerializeField] private GameObject _choicePlusButton;

    [Header("Choice Button Texts (TMP)")]
    [SerializeField] private TMP_Text _choiceMinusText;
    [SerializeField] private TMP_Text _choicePlusText;

    public event Action<DialogueComponent> OnDialogueClosed;

    private DialogueComponent _currentDialogue;
    private bool _dialogueOpen;
    private bool _choiceDone;
    public bool IsDialogueOpen { get; private set; }

    public void StartDialogue(DialogueComponent dialogue)
    {
        if (_dialogueOpen) return;

        _dialogueOpen = true;
        _choiceDone = false;

        _currentDialogue = dialogue;

        Debug.Log("StartDialogue called");
        IsDialogueOpen = true;

        _dialoguePanel.SetActive(true);
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (_currentDialogue == null) return;

        _characterNameText.text = _currentDialogue.GetCharacterName();
        _dialogueText.text = _currentDialogue.GetDialogueText();

        bool hasChoiceNow = _currentDialogue.HasChoice() && !_choiceDone;

        _choiceMinusButton.SetActive(hasChoiceNow);
        _choicePlusButton.SetActive(hasChoiceNow);
        _nextButton.SetActive(!hasChoiceNow);

        if (hasChoiceNow)
        {
            if (_choiceMinusText != null) _choiceMinusText.text = _currentDialogue.GetMinusChoiceText();
            if (_choicePlusText != null) _choicePlusText.text = _currentDialogue.GetPlusChoiceText();
        }
    }

    // Button Next -> OnClick
    public void ChangeRow()
    {
        if (!_dialogueOpen) return;
        if (_currentDialogue == null) return;

     
        if (_currentDialogue.HasChoice() && !_choiceDone) return;

        _currentDialogue.NextRow();
    }

    // Button choice -5 -> OnClick
    public void ChooseMinus5()
    {
        if (!_dialogueOpen) return;
        if (_currentDialogue == null) return;
        if (_choiceDone) return;

        _choiceDone = true;
        _currentDialogue.ChooseBranch(calm: true);
        RefreshUI();
    }

    // Button choice +5 -> OnClick
    public void ChoosePlus5()
    {
        if (!_dialogueOpen) return;
        if (_currentDialogue == null) return;
        if (_choiceDone) return;

        _choiceDone = true;
        _currentDialogue.ChooseBranch(calm: false);
        RefreshUI();
    }

    public void EndDialogue(DialogueComponent dialogue)
    {
        if (!_dialogueOpen) return;

        _dialogueOpen = false;
        _choiceDone = false;
        _dialoguePanel.SetActive(false);

        Debug.Log("EndDialogue called");
        IsDialogueOpen = false;

        OnDialogueClosed?.Invoke(dialogue);

        _currentDialogue = null;
    }
}
