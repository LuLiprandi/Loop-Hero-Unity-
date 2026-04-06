using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Pawn _pawn;

    [Header("Starting Values")]
    [SerializeField] private int _startingCell = 0;
    [SerializeField] private int _startingFear = 30;

    /// <summary>
    /// Mis à true par le mini-jeu avant de revenir sur Dev_map
    /// pour empêcher le reset des données du joueur.
    /// </summary>
    public static bool ReturningFromMiniGame = false;

    private void Awake()
    {
        if (ReturningFromMiniGame)
        {
            ReturningFromMiniGame = false;
            return;
        }

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
