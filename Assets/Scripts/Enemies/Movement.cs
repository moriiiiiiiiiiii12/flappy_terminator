using UnityEngine;

class Movement : MonoBehaviour
{
    [SerializeField] float _speed = 1f;
    [SerializeField] private Vector3 _direction = new Vector3(1, 0, 0); 

    private void Update()
    {
        transform.position += _direction.normalized * _speed * Time.deltaTime;
    }
}