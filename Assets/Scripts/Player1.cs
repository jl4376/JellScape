using UnityEngine;

public class Player1 : Player
{
    // public Animator animator;
    private int defaultHealth;
    public static Player1 Instance;
    private Vector3 initalLocation;
    public static bool isDestroyed = false;
    public int dashForce;
    // [SerializeField] private GameObject playerPrefab;

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
        // Change to only arrow keys later
        if(moveDirection.magnitude > 0){
            rigidBody.linearVelocity = moveDirection * moveSpeed;
       } else {
            rigidBody.linearVelocity -= rigidBody.linearVelocity * friction;
       }
    }

    void Update() {
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
    }

    // Implement dash here - Ailey
}
