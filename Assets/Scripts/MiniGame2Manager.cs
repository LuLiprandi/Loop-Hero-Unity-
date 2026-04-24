using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>Gestionnaire du mini-jeu 2 — collecte de peluches en temps limité.</summary>
public class MiniGame2Manager : MonoBehaviour
{
    public static MiniGame2Manager Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float    _gameDuration = 60f;
    [SerializeField] private TMP_Text _timerText;

    [Header("Peluches")]
    [SerializeField] private TMP_Text _plushText;

    [Header("Zone de sortie")]
    [SerializeField] private Transform _exitZoneTransform;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float     _exitZoneRadius = 4f;

    [Header("UI")]
    [SerializeField] private GameObject _victoryPanel;
    [SerializeField] private GameObject _defeatPanel;

    [Header("Références")]
    [SerializeField] private FPSController _fpsController;
    [SerializeField] private PlayerDatas   _playerDatas;

    [Header("Impact Peur")]
    [SerializeField] private int _fearBonus   = 20;
    [SerializeField] private int _fearPenalty = 20;

    private float _remainingTime;
    private int   _collectedCount = 0;
    private bool  _gameOver       = false;

    private const int    TotalPlushes  = 7;
    private const string MainSceneName = "Dev_map";
    private const float  EndDelay      = 3f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _remainingTime = _gameDuration;
        _victoryPanel?.SetActive(false);
        _defeatPanel?.SetActive(false);

        UpdateTimerUI();
        UpdatePlushUI();
    }

    private void Update()
    {
        if (_gameOver) return;

        _remainingTime -= Time.deltaTime;

        if (_remainingTime <= 0f)
        {
            _remainingTime = 0f;
            UpdateTimerUI();
            OnTimeExpired();
            return;
        }

        UpdateTimerUI();
        CheckExitZone();
    }

    /// <summary>Vérifie chaque frame si le joueur est dans la zone de sortie avec toutes les peluches.</summary>
    private void CheckExitZone()
    {
        if (_exitZoneTransform == null || _playerTransform == null) return;
        if (_collectedCount < TotalPlushes) return;

        float dist = Vector3.Distance(
            new Vector3(_playerTransform.position.x, 0f, _playerTransform.position.z),
            new Vector3(_exitZoneTransform.position.x, 0f, _exitZoneTransform.position.z)
        );

        if (dist <= _exitZoneRadius)
            StartCoroutine(Victory());
    }

    /// <summary>Appelé par PlushCollectible quand le joueur touche une peluche.</summary>
    public void OnPlushCollected()
    {
        if (_gameOver) return;

        _collectedCount++;
        UpdatePlushUI();
    }

    private void UpdateTimerUI()
    {
        if (_timerText == null) return;
        int minutes = Mathf.FloorToInt(_remainingTime / 60f);
        int seconds = Mathf.FloorToInt(_remainingTime % 60f);
        _timerText.text = $"{minutes:0}:{seconds:00}";
    }

    private void UpdatePlushUI()
    {
        if (_plushText == null) return;
        _plushText.text = $"{_collectedCount} / {TotalPlushes} peluches";
    }

    private IEnumerator Victory()
    {
        _gameOver = true;
        _fpsController?.SetMovement(false);

        ApplyFearBonus(_fearBonus);
        _victoryPanel?.SetActive(true);

        yield return new WaitForSeconds(EndDelay);
        ReturnToMainScene();
    }

    private void OnTimeExpired()
    {
        _gameOver = true;
        _fpsController?.SetMovement(false);

        ApplyFearPenalty(_fearPenalty);
        _defeatPanel?.SetActive(true);

        StartCoroutine(ReturnAfterDelay());
    }

    private IEnumerator ReturnAfterDelay()
    {
        yield return new WaitForSeconds(EndDelay);
        ReturnToMainScene();
    }

    private void ReturnToMainScene()
    {
        GameInitializer.ReturningFromMiniGame = true;
        SceneManager.LoadScene(MainSceneName);
    }

    private void ApplyFearBonus(int amount)
    {
        if (_playerDatas == null) return;
        _playerDatas._fear = Mathf.Max(_playerDatas._fear - amount, 0);
    }

    private void ApplyFearPenalty(int amount)
    {
        if (_playerDatas == null) return;
        _playerDatas._fear = Mathf.Min(_playerDatas._fear + amount, 100);
    }
}
