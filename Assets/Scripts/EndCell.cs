using UnityEngine;

public class EndCell : Cell
{
    [Header("End Game Settings")]
    [SerializeField] private GameObject _endWidget;

    public override void Activate(Pawn pawn)
    {
        base.Activate(pawn);
        ShowEndWidget();
    }

    private void ShowEndWidget()
    {
        if (_endWidget != null)
        {
            _endWidget.SetActive(true);
        }
    }
}

