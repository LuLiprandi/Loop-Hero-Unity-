using UnityEngine;

public class Cell : MonoBehaviour, ICellActivable 
{
    public virtual void Activate(Pawn Currentpawn)
    {
        if(GetComponent<IActionnable>() != null)
        {
            GetComponent<IActionnable>().Action(Currentpawn);
        }
    }
}
