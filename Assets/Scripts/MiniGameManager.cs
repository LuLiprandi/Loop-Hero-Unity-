using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MiniGameManager : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float    _gameDuration = 120f;
    [SerializeField] private TMP_Text _timerText;

    [Header("UI")]
    [SerializeField] private GameObject _caughtPanel;
    [SerializeField] private GameObject _returnCellObject;

    [Header("References")]
    [SerializeField] private AvaController _avaController;
    [SerializeField] private PlayerDatas   _playerDatas;

    [Header("Fear Impact")]
    [SerializeField] private int _fearReductionOnSurvive = 20;
    [SerializeField] private int _fearIncreaseOnCaught   = 15;

    private float _remainingTime;
    private bool  _gameOver = false;

    private const string MainSceneName           = "Dev_map";
    private const float  ReturnDelayAfterCaught  = 3f;

    private void Start()
    {
        _remainingTime = _gameDuration;
        _caughtPanel?.SetActive(false);
        _returnCellObject?.SetActive(false);

        UpdateTimerUI();
    }

    private void Update()
    {
        if (_gameOver) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0f)
        {
            _remainingTime = 0f;
            UpdateTimerUI();
            OnTimerExpired();
            return;
        }

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        if (_timerText == null) return;

        int minutes = Mathf.FloorToInt(_remainingTime / 60f);
        int seconds = Mathf.FloorToInt(_remainingTime % 60f);
        _timerText.text = $"{minutes:0}:{seconds:00}";
    }

    
    public void OnPlayerCaught()
    {
        if (_gameOver) return;

        _gameOver = true;
        _avaController?.SetMovement(false);

        ApplyFearPenalty(_fearIncreaseOnCaught);

        _caughtPanel?.SetActive(true);

        StartCoroutine(ReturnToMainSceneAfterDelay(ReturnDelayAfterCaught));
    }

    
    public void ReturnToMainScene()
    {
        SceneManager.LoadScene(MainSceneName);
    }

    private void OnTimerExpired()
    {
        _gameOver = true;
        _avaController?.SetMovement(false);

        ApplyFearBonus(_fearReductionOnSurvive);

        _returnCellObject?.SetActive(true);
    }

    private void ApplyFearPenalty(int amount)
    {
        if (_playerDatas == null) return;
        _playerDatas._fear = Mathf.Min(_playerDatas._fear + amount, 100);
    }

    private void ApplyFearBonus(int amount)
    {
        if (_playerDatas == null) return;
        _playerDatas._fear = Mathf.Max(_playerDatas._fear - amount, 0);
    }

    private IEnumerator ReturnToMainSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(MainSceneName);
    }
}
