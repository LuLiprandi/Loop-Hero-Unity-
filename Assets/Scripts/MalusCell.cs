using UnityEngine;

public class MalusCell : MonoBehaviour, IActionnable
{
    [SerializeField] private int fearIncrease = 10;
    [SerializeField] private bool hasBackwardEffect = false;
    [SerializeField] private int backwardValue = 1;
    public void Action(Pawn pawn)
    {
        pawn.IncreaseFear(fearIncrease);
        if (hasBackwardEffect)
        {
            pawn.GoBackward(backwardValue);
        }

        Debug.Log("Case Malus activée");
    }
}

 

