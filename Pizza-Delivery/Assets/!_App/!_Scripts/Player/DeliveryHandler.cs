using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerUIHandler))]
public class DeliveryHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pizza;
    [SerializeField] private Transform _pizzaSpawnPoint;
    [SerializeField] private float _throwTime;
    [SerializeField] private int _deliveryMinPrice;
    [SerializeField] private int _deliveryMaxPrice;
    [SerializeField] private AudioClip _deliverySound;

    private PlayerMovement _playerMovement;
    private PlayerUIHandler _playerUIHandler;

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerUIHandler = GetComponent<PlayerUIHandler>();
    }

    public void HandleDeliveryTrigger(Transform deliveryPoint)
    {
        _pizza.SetActive(true);
        _pizza.transform.position = _pizzaSpawnPoint.position;
        _playerMovement.StopMovement();
        ThrowPizza(deliveryPoint);
    }

    public void ThrowPizza(Transform deliveryPoint)
    {
        _pizza.transform.DOMove(deliveryPoint.position, _throwTime).OnComplete(OnPizzaDelivered);
    }

    private void OnPizzaDelivered()
    {
        _playerMovement.ResumeMovement();
        GameAudio.Instance.PlayAudio(_deliverySound);
        SaveSystem.AddMoney(UnityEngine.Random.Range(_deliveryMinPrice, _deliveryMaxPrice + 1));
        _playerUIHandler.UpdateMoney();
        _pizza.SetActive(false);
    }
}
