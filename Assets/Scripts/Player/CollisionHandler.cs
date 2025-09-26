using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    public event Action<IInteractable> CollisionDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        int collisionLayerMask = 1 << other.gameObject.layer;

        if (other.TryGetComponent(out IInteractable interactable) && collisionLayerMask == _targetLayer.value)
        {
            CollisionDetected?.Invoke(interactable);
        }
    }
}