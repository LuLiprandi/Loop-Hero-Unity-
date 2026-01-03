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
    [SerializeField] private GameObject _choiceMinusButton; // -5 peur
    [SerializeField] private GameObject _choicePlusButton;  // +5 peur

    private DialogueComponent _dialogueComponent;
    private bool _choiceDone;

    public void StartDialogue(DialogueComponent dialogueComponent)
    {
        _dialogueComponent = dialogueComponent;
        _choiceDone = false;
        _dialoguePanel.SetActive(true);
        _choiceMinusButton.SetActive(true);
        _choicePlusButton.SetActive(true);
        _nextButton.SetActive(false);

        UpdateText();
    }

    public void ChooseMinus5()
    {
        if (_choiceDone) return;
        _choiceDone = true;
        _dialogueComponent.ApplyFearChoice(-5);
        _choiceMinusButton.SetActive(false);
        _choicePlusButton.SetActive(false);
        _nextButton.SetActive(true);
    }

    public void ChoosePlus5()
    {
        if (_choiceDone) return;
        _choiceDone = true;
        _dialogueComponent.ApplyFearChoice(+5);
        _choiceMinusButton.SetActive(false);
        _choicePlusButton.SetActive(false);
        _nextButton.SetActive(true);
    }

    public void ChangeRow()
    {
        if (!_choiceDone) return;

        _dialogueComponent.GetNextRow();
    }

    public void UpdateText()
    {
        Debug.Log("UI UpdateText -> " + _dialogueComponent.GetCharacterName() + " / " + _dialogueComponent.GetDialogueText());
        _characterNameText.text = _dialogueComponent.GetCharacterName();
        _dialogueText.text = _dialogueComponent.GetDialogueText();
    }

    public void EndDialogue()
    {
        
        _dialoguePanel.SetActive(false);
        _characterNameText.text = "";
        _dialogueText.text = "";
        _choiceMinusButton.SetActive(false);
        _choicePlusButton.SetActive(false);
        _nextButton.SetActive(false);
        _choiceDone = false;
        _dialogueComponent = null;
    }
}
