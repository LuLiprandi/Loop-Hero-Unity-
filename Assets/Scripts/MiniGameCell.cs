using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameCell : Cell
{
    [Header("Mini Game")]
    [SerializeField] private string _miniGameSceneName = "Cache-Cache mini game";

    
    public override void Activate(Pawn pawn)
    {
        SceneManager.LoadScene(_miniGameSceneName);
    }
}
