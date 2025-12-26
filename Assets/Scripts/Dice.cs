using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] Dice _dice;
    public void RollTheDice()
    {
        int value = Random.Range(1,4);
        Debug.Log($"Le dé a fait {value}");
        _pawn.TryMoving(value);
    }
}
