using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private PlayerDatas _playerDatas; 
    [SerializeField] private Board _board;

    private void Start()
    {
        MoveToCell();
    }
    private void MoveToCell()
    { 
        Transform NewPos = _board.GetCellByNumber(_playerDatas._cellNumber).transform; // to do : Get Cell Number from PlayerDatas 
        transform.position = NewPos.position;
        transform.rotation = NewPos.rotation;
    }
    


    public void TryMoving(int value)
    {
        _playerDatas._cellNumber = _board.GetNextCellToMove(_playerDatas._cellNumber+value);
        MoveToCell();
        ActivateCell();
    }

    private void ActivateCell()
    {
     Cell cell = _board .GetCellByNumber(_playerDatas._cellNumber);// to do : get cell number
     cell.Activate(this);
    }
    

}


