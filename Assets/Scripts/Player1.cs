using UnityEngine;
using System.Collections;

public class Player1 : Player
{
    public Animator animator;
    private int defaultHealth;
    public static Player1 Instance;
    private Vector3 initalLocation;
    public static bool isDestroyed = false;
    // [SerializeField] private GameObject playerPrefab;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;

    [SerializeField] private TrailRenderer tr;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    protected override void CustomStart() {
        initalLocation = rigidBody.transform.position;
        defaultHealth = health;
    }

    protected override void Move() {
    if (isDashing) {
        return;
    }

    if (moveDirection.magnitude > 0) {
        rigidBody.linearVelocity = moveDirection * moveSpeed;
    } else {
        // Slowly reduce velocity and clamp it to zero when small
        rigidBody.linearVelocity *= (1f - friction);

        if (rigidBody.linearVelocity.magnitude < 0.01f) {
            rigidBody.linearVelocity = Vector2.zero;
        }
    }
}

    void Update() {
        moveDirection = Vector2.zero;

        // Collect input
        if (Input.GetKey(KeyCode.W)) {
            moveDirection.y = 1;
        } else if (Input.GetKey(KeyCode.S)) {
            moveDirection.y = -1;
        }

        if (Input.GetKey(KeyCode.A)) {
            moveDirection.x = -1;
        } else if (Input.GetKey(KeyCode.D)) {
            moveDirection.x = 1;
        }

        // Set walking animation based on movement vector
        //animator.SetBool("isWalk", moveDirection != Vector2.zero);

        if (Input.GetKeyDown(KeyCode.Tab) && canDash) {
            StartCoroutine(Dash());
        }

    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;

        Vector2 dashDir = moveDirection.magnitude > 0 ? moveDirection.normalized : Vector2.right * transform.localScale.x;
        rigidBody.linearVelocity = dashDir * dashingPower;

        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void TakeDamage(int amount) {
        health -= amount;
       //animator.setBool();
        if (health <= 0)
        {
            Debug.Log("Player has died!");
            gameObject.SetActive(false); // Or trigger death animation, etc.
        } else {
            Debug.Log($"Player has {health} health left");
        }
    }

}
