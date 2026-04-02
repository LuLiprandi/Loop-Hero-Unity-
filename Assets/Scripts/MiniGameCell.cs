using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameCell : Cell
{
    [Header("Mini Game")]
    [SerializeField] private string _miniGameSceneName = "Cache-Cache mini game";

    /// <summary>Charge la scène du mini-jeu quand le Pawn s'arrête sur cette case.</summary>
    public override void Activate(Pawn pawn)
    {
        SceneManager.LoadScene(_miniGameSceneName);
    }
}
