using System;
using UnityEngine;
using UnityEngine.UI;

public class FearManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Image _fearFillImage;
    [SerializeField] private GameObject _gameOverWidget;

    [Header("Fear Settings")]
    [SerializeField] private int _initialFear = 30;
    [SerializeField] private int _maxFear = 100;
    [SerializeField] private int _minFear = 0;

    public event Action OnGameOver;

    private bool _isGameOver = false;

    private void Start()
    {
        UpdateFearUI();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void InitializeFear()
    {
        if (_playerDatas != null)
        {
            _playerDatas._fear = _initialFear;
        }
    }

    private void SubscribeToEvents()
    {
    }

    private void UnsubscribeFromEvents()
    {
    }

    public void UpdateFear(int newFearValue)
    {
        if (_isGameOver) return;

        if (_playerDatas != null)
        {
            _playerDatas._fear = Mathf.Clamp(newFearValue, _minFear, _maxFear);
            UpdateFearUI();

            if (_playerDatas._fear >= _maxFear)
            {
                TriggerGameOver();
            }
        }
    }

    private void UpdateFearUI()
    {
        if (_fearFillImage != null && _playerDatas != null)
        {
            float fillAmount = (float)_playerDatas._fear / _maxFear;
            _fearFillImage.fillAmount = fillAmount;
        }
    }

    private void TriggerGameOver()
    {
        if (_isGameOver) return;

        _isGameOver = true;

        if (_gameOverWidget != null)
        {
            _gameOverWidget.SetActive(true);
        }

        OnGameOver?.Invoke();
    }

    public int GetCurrentFear()
    {
        return _playerDatas != null ? _playerDatas._fear : 0;
    }

    public bool IsGameOver()
    {
        return _isGameOver;
    }
}
