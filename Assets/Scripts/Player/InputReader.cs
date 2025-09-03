using System;
using UnityEngine;


public class InputReader : MonoBehaviour
{
    private const string Jump = nameof(Jump);
    private const string Horizontal = nameof(Horizontal);
    private const string Fire1 = nameof(Fire1);

    public float HorizontalAxis { get; private set; }

    public event Action AttackInput;
    public event Action PressJumpInput;

    private void Update()
    {
        bool jumpInput = Input.GetButtonDown(Jump);
        bool attackInput = Input.GetButtonDown(Fire1);
        HorizontalAxis = Input.GetAxis(Horizontal);

        if (jumpInput)
            PressJumpInput?.Invoke();

        if (attackInput)
            AttackInput?.Invoke();
    }
}