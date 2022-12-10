using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private Image _moneyImage;
    [SerializeField] private GameObject _endGameView;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private GameObject _joystickHolder;
    [Header("UI Animation")]
    [SerializeField] private float _shakeDuration;
    [SerializeField] private float _shakeStrength;
    [SerializeField] private int _vibrato;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        SetListeners();
    }

    private void OnEnable()
    {
        _playerHealth.OnLose += ShowEndGameScreen;
        UpdateMoney();
    }

    private void OnDisable()
    {
       _playerHealth.OnLose -= ShowEndGameScreen;
    }

    private void SetListeners()
    {
        Application.targetFrameRate = 60;
        _restartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        _exitButton.onClick.AddListener(() => Application.Quit());
    }

    public void UpdateMoney()
    {
        _moneyText.text = SaveSystem.LoadMoney().ToString();
        _moneyImage.transform.DOShakeScale(_shakeDuration, _shakeStrength, _vibrato);
    }

    private void ShowEndGameScreen()
    {
        _joystickHolder.SetActive(false);
        _endGameView.SetActive(true);
    }
}
