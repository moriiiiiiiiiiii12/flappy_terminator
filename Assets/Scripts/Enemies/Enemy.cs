using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private float _cooldown;
}