using UnityEngine;

public class BonusCell : MonoBehaviour, IActionnable
{
    [SerializeField] private int fearReduction = 10;

    public void Action(Pawn pawn)
    {
        pawn.ReduceFear(fearReduction);
        Debug.Log("Case Bonus : -" + fearReduction + "peur");
    }

   
    
}
