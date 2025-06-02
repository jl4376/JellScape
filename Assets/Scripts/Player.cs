using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth;
    [SerializeField] protected HealthBar healthBar;

    [Header("Movement")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float friction;

    [Header("Dash")]
    [SerializeField] protected float dashingPower;
    [SerializeField] protected float dashingTime;
    [SerializeField] protected float dashingCooldown;
    [SerializeField] protected TrailRenderer tr;

    [Header("Audio")]
    [SerializeField] protected AudioClip dashingClip, damageClip, deathClip;
    private AudioSource audioSource;

    protected Rigidbody2D    rigidBody;
    protected SpriteRenderer spriteRenderer;
    protected int            health;
    protected Vector2        moveDirection;
    protected bool           canDash = true;
    protected bool           isDashing;

    void Awake()
    {
        rigidBody     = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
        audioSource = gameObject.GetComponent<AudioSource>();
        GameManager.RegisterPlayer();
        OnStart();                
    }

    void Update()
    {
        if (isDashing) {
            audioSource.PlayOneShot(dashingClip);
            return;
        }

        moveDirection = ReadMovementInput();
        if (ReadDashInput() && canDash)
            StartCoroutine(Dash());
    }

    void FixedUpdate()
    {
        if (!isDashing)
            DoMove();
    }

    protected virtual void DoMove()
    {
        if (moveDirection.sqrMagnitude > 0f)
            rigidBody.linearVelocity = moveDirection.normalized * moveSpeed;
        else
        {
            rigidBody.linearVelocity *= (1f - friction);
            if (rigidBody.linearVelocity.sqrMagnitude < 0.0001f)
                rigidBody.linearVelocity = Vector2.zero;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDir = moveDirection.sqrMagnitude > 0f
            ? moveDirection.normalized
            : Vector2.right;

        rigidBody.linearVelocity = dashDir * dashingPower;
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    protected abstract Vector2 ReadMovementInput();
    protected abstract bool    ReadDashInput();

    protected virtual void OnStart() { }

    public void TakeDamage(int amount)
    {
        if (isDashing) return;

        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            audioSource.PlayOneShot(deathClip);
            Debug.Log("Player has died!");
            StartCoroutine(DeactivateAfterSound(deathClip.length));
        } else {
            audioSource.PlayOneShot(damageClip);
        }
    }

    private IEnumerator DeactivateAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.HandlePlayerDeath();
        gameObject.SetActive(false);
    }
}
