using TMPro;
using UnityEngine;

public class FearUIDisplay : MonoBehaviour
{
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private TMP_Text _fearText;
    [SerializeField] private int _maxFear = 100;

    private void Update()
    {
        if (_fearText != null && _playerDatas != null)
        {
            _fearText.text = $"Peur: {_playerDatas._fear}/{_maxFear}";
        }
    }
}
