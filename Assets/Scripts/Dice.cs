using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private UIDialogueController _dialogueController;
    [SerializeField] private Button _diceButton;

    private void OnEnable()
    {
        if (_dialogueController != null)
        {
            _dialogueController.OnDialogueClosed += OnDialogueEnded;
        }
    }

    private void OnDisable()
    {
        if (_dialogueController != null)
        {
            _dialogueController.OnDialogueClosed -= OnDialogueEnded;
        }
    }

    public void RollTheDice()
    {
        if (_dialogueController != null && _dialogueController.IsDialogueOpen)
        {
            Debug.Log("Impossible de lancer le dé pendant un dialogue");
            return;
        }


        if (_pawn != null && _pawn.IsMoving())
        {
            Debug.Log("Impossible de lancer le dé pendant le mouvement");
            return;
        }

        int value = Random.Range(1, 4);
        Debug.Log($"Le dé a fait {value}");
        _pawn.TryMoving(value);
    }

    private void OnDialogueEnded(DialogueComponent dialogue)
    {
        EnableDiceButton();
    }

    public void DisableDiceButton()
    {
        if (_diceButton != null)
        {
            _diceButton.interactable = false;
        }
    }

    public void EnableDiceButton()
    {
        if (_diceButton != null)
        {
            _diceButton.interactable = true;
        }
    }
}
