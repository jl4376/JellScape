using UnityEngine;
using System.Collections;

public class Player1 : Player
{
    // public Animator animator;
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
        if(moveDirection.magnitude > 0){
            rigidBody.linearVelocity = moveDirection * moveSpeed;
       } else {
            rigidBody.linearVelocity -= rigidBody.linearVelocity * friction;
       }
    }

    void Update() {
        if (isDashing) {
            return;
        }

        if (Input.GetKey(KeyCode.W)) {
            moveDirection.y = 1;
        } else if (Input.GetKey(KeyCode.S)) {
            moveDirection.y = -1;
        } else {
            moveDirection.y = 0;
        }
        if (Input.GetKey(KeyCode.A)) {
            moveDirection.x = -1;
        } else if (Input.GetKey(KeyCode.D)) {
            moveDirection.x = 1;
        } else {
            moveDirection.x = 0;
        }

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
}
