using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInputs))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _acceleration;
    [SerializeField] private float _baseMovementSpeed;

    private Rigidbody _rigidbody;
    private PlayerInputs _playerInputs;
    private PlayerHealth _playerHealth;

    private float _currentSpeed;

    private int _speedController;

    private void Awake()
    {
        SetComponents();
    }

    private void OnEnable()
    {
        _playerHealth.OnLose += StopMovement;

        _currentSpeed = _baseMovementSpeed;
    }

    private void OnDisable()
    {
        _playerHealth.OnLose -= StopMovement;
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void SetComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerInputs = GetComponent<PlayerInputs>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void HandleInputs()
    {
        _speedController = _playerInputs.HorizontalMovementInput;
    }

    private void HandleMovement()
    {
        var movementSpeed = _currentSpeed + _acceleration * _speedController;
        _rigidbody.velocity = Vector3.forward * movementSpeed;
    }

    public void StopMovement()
    {
        _currentSpeed = 0;
        _rigidbody.velocity = Vector3.zero;
    }

    public void ResumeMovement() => _currentSpeed = _baseMovementSpeed;
}
