using UnityEngine;

public class MalusCell : MonoBehaviour, IActionnable
{
    [SerializeField] private int fearIncrease = 10;

    public void Action(Pawn pawn)
    {
        pawn.IncreaseFear(fearIncrease);
        Debug.Log("Case Malus : +" + fearIncrease + "peur");
    }

 
}
