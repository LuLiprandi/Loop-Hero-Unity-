using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDatas", menuName = "Scriptable Objects/PlayerDatas")]
public class PlayerDatas : ScriptableObject
{
    [SerializeField] public int _cellNumber;
    [SerializeField] public int _fear;

    [SerializeField] private List<int> _doneDialogues = new List<int>();

    public void ResetData(int startingCell = 0, int startingFear = 30)
    {
        _cellNumber = startingCell;
        _fear = startingFear;
        _doneDialogues.Clear();
    }

    public bool IsDialogueDone(int dialogueId)
    {
        return _doneDialogues.Contains(dialogueId);
    }

    public void SetDialogueDone(int dialogueId, bool done)
    {
        if (done)
        {
            if (!_doneDialogues.Contains(dialogueId))
                _doneDialogues.Add(dialogueId);
        }
        else
        {
            _doneDialogues.Remove(dialogueId);
        }
    }
}
