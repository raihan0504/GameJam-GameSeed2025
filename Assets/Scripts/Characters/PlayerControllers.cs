using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllers : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TrailRenderer tr;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveDir;

    [Header("Dashing")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCooldown = 0.1f;
    bool isDashing;
    bool canDash = true;

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = moveDir * moveSpeed;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        tr.emitting = true;
        rb.velocity = moveDir.normalized * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
