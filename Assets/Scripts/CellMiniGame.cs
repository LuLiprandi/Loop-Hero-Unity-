using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Charge la scène du mini-jeu quand le joueur atterrit sur cette case.
/// </summary>
public class CellMiniGame : MonoBehaviour, IActionnable
{
    [SerializeField] private string _miniGameSceneName = "Cache-Cache mini game";

    /// <summary>
    /// Appelé par Cell.Activate() quand le joueur tombe sur la case.
    /// </summary>
    public void Action(Pawn pawn)
    {
        SceneManager.LoadScene(_miniGameSceneName);
    }
}
