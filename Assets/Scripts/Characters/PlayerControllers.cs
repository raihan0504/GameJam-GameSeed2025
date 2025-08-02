using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllers : MonoBehaviour
{
    public static PlayerControllers instance;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] TrailRenderer tr;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer sprite;
    private Interactable currentInteractable;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    private Vector2 moveDir;
    private Vector2 lasMoveDir = Vector2.down;

    [Header("Dashing")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCooldown = 0.1f;
    bool isDashing;
    bool canDash = true;

    private void Update()
    {
        // Animation Movement
        anim.SetFloat("xInput", lasMoveDir.x);
        anim.SetFloat("yInput", lasMoveDir.y);
        anim.SetFloat("Speed", moveDir.magnitude);
        PlayerFlip();
       
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity = moveDir * moveSpeed;
        }
    }

    private void PlayerFlip()
    {
        if (moveDir.x > 0)
        {
            sprite.flipX = false;
        } 
        else if (moveDir.x < 0)
        {
            sprite.flipX = true;                                                                                                        
        }
    }

    #region InputAction
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

        if (moveDir != Vector2.zero)
        {
            lasMoveDir = moveDir;
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
    #endregion

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable))
        {
            currentInteractable = interactable;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Interactable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
