using System.Collections;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Board _board;
    [SerializeField] private FearManager _fearManager;
    [SerializeField] private float _moveDuration = 0.5f;

    private bool _isMoving = false;

    private void Start()
    {
        MoveToCell(_playerDatas._cellNumber);
    }

    private void MoveToCell(int cellNumber)
    {
        Transform target = _board.GetCellByNumber(cellNumber).transform;
        transform.position = target.position;
    }

    public void TryMoving(int steps)
    {
        if (_isMoving) return;
        StartCoroutine(MoveStepByStep(steps));
    }

    private IEnumerator MoveStepByStep(int steps)
    {
        _isMoving = true;

        int startCell = _playerDatas._cellNumber;
        int finalCell = _board.GetNextCellToMove(startCell + steps);

        int currentCell = startCell;

        while (currentCell != finalCell)
        {
            int nextCell = currentCell + 1;

            if (nextCell >= _board.CellCount)
            {
                nextCell = _board.LoopStartIndex;
            }

            nextCell = _board.GetNextCellToMove(nextCell);

            _playerDatas._cellNumber = nextCell;
            MoveToCell(nextCell);

            yield return new WaitForSeconds(_moveDuration);

            currentCell = nextCell;

            if (currentCell == finalCell)
                break;
        }

        ActivateCell();
        _isMoving = false;
    }

    private void ActivateCell()
    {
        Cell cell = _board.GetCellByNumber(_playerDatas._cellNumber);
        cell.Activate(this);
    }

    public void IncreaseFear(int value)
    {
        if (_fearManager != null)
        {
            int newFear = _playerDatas._fear + value;
            _fearManager.UpdateFear(newFear);
        }
        else
        {
            _playerDatas._fear += value;
            _playerDatas._fear = Mathf.Clamp(_playerDatas._fear, 0, 100);
        }
    }

    public void ReduceFear(int value)
    {
        if (_fearManager != null)
        {
            int newFear = _playerDatas._fear - value;
            _fearManager.UpdateFear(newFear);
        }
        else
        {
            _playerDatas._fear -= value;
            _playerDatas._fear = Mathf.Clamp(_playerDatas._fear, 0, 100);
        }
    }

    public void GoBackward(int value)
    {
        int targetCell = _playerDatas._cellNumber - value;

        if (targetCell < _board.LoopStartIndex)
            return;

        _playerDatas._cellNumber = _board.GetNextCellToMove(targetCell);
        MoveToCell(_playerDatas._cellNumber);
    }

    public void GoForward(int value)
    {
        int targetCell = _playerDatas._cellNumber + value;
        _playerDatas._cellNumber = _board.GetNextCellToMove(targetCell);
        MoveToCell(_playerDatas._cellNumber);
    }

    public bool IsMoving()
    {
        return _isMoving;
    }
}
