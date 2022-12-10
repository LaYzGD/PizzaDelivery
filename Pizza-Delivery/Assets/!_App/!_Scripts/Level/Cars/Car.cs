using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    private float _movementSpeed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        SetComponents();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void SetComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void HandleMovement()
    {
        _rigidbody.velocity = transform.forward * _movementSpeed;
    }

    public void SetValues(Vector3 direction, float speed)
    {
        transform.rotation = Quaternion.LookRotation(direction);
        _movementSpeed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(_damage);
        }
    }
}
