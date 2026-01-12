using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Pawn _pawn;

    [Header("Starting Values")]
    [SerializeField] private int _startingCell = 0;
    [SerializeField] private int _startingFear = 30;

    private void Awake()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        if (_playerDatas != null)
        {
            _playerDatas.ResetData(_startingCell, _startingFear);
            Debug.Log($"Game initialized: Cell {_startingCell}, Fear {_startingFear}");
        }
    }
}
