using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Cell[] _cells;
    [SerializeField] private int _loopStartIndex = 9;

    public int CellCount => _cells.Length;


    public Cell GetCellByNumber(int number)
    {
        return _cells[number];
   
    }

    public int GetNextCellToMove(int cellNumber)
    {
        // Avant la boucle : linéaire
        if (cellNumber < _loopStartIndex)
        {
            //bloque la fin du tableau 
            return Mathf.Min(cellNumber, _cells.Length - 1);
        }
        // Dans la boucle : modulo
        int loopLength = _cells.Length - _loopStartIndex;
        int loopIndex = (cellNumber - _loopStartIndex) % loopLength;

        return _loopStartIndex + loopIndex;
    }

    public int LoopStartIndex => _loopStartIndex;



}
