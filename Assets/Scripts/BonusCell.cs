using UnityEngine;

public class BonusCell : MonoBehaviour, IActionnable
{
    [SerializeField] private int fearReduction = 10;
    [SerializeField] private bool hasForwardEffect = false;
    [SerializeField] private int forwardValue = 1;

    public void Action(Pawn pawn)
    {
        pawn.ReduceFear(fearReduction);

        if (hasForwardEffect)
        {
            pawn.GoForward(forwardValue);
        }

        Debug.Log("Case Bonus activée");
    }
}

   
    

