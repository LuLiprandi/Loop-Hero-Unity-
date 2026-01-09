using System.Collections;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private PlayerDatas _playerDatas; 
    [SerializeField] private Board _board;

    private bool _isMoving = false;

    private void Start()
    {
        MoveToCell();
 
    }
    private void MoveToCell()
    { 
        Transform target = _board.GetCellByNumber(_playerDatas._cellNumber).transform; // to do : Get Cell Number from PlayerDatas 
        transform.position = target.position;
    
    }
    


    public void TryMoving(int value)
    {
        _playerDatas._cellNumber = _board.GetNextCellToMove(_playerDatas._cellNumber+value);
        MoveToCell();
        ActivateCell();
        if (_isMoving) return;
        StartCoroutine(MoveStepByStep(value));
    }

    private IEnumerator MoveStepByStep(int steps)
    {
        _isMoving = true;
        for (int i = 0; i < steps; i++)
        {
         int nextcell = _playerDatas._cellNumber + 1;

        

         if (nextcell >= _board.CellCount)
         break;

         _playerDatas._cellNumber = nextcell;
            MoveToCell();

            yield return new WaitForSeconds(1f);
        }
       ActivateCell();
         _isMoving = false;
    }

    private void ActivateCell()
    {
     Cell cell = _board .GetCellByNumber(_playerDatas._cellNumber);// to do : get cell number
     cell.Activate(this);
    }
    
    public void IncreaseFear(int value)
    {
        _playerDatas._fear += value;
        _playerDatas._fear = Mathf.Clamp(_playerDatas._fear, 0, 100);
    }

    public void ReduceFear(int value)
    {
        _playerDatas._fear -= value;
        _playerDatas._fear = Mathf.Clamp(_playerDatas._fear, 0, 100);
    }

    public void GoBackward(int value)
        {
        int targetCell = _playerDatas._cellNumber - value;

        if (targetCell < _board.LoopStartIndex)
            return;
        _playerDatas._cellNumber = _board.GetNextCellToMove(targetCell);
        MoveToCell();

    }
    public void GoForward(int value)
    {
        int targetCell = _playerDatas._cellNumber + value;
        _playerDatas._cellNumber = _board.GetNextCellToMove(targetCell);
        MoveToCell();
    }
}


