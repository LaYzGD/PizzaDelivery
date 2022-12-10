using UnityEngine;

public class DeliveryTrigger : MonoBehaviour
{
    [SerializeField] private Transform _deliveryPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DeliveryHandler deliveryHandler))
        {
            deliveryHandler.HandleDeliveryTrigger(_deliveryPoint);
        }
    }
}
