using UnityEngine;

public class Player2 : Player
{
    // public Animator animator;
    private int defaultHealth;
    public static Player2 Instance;
    private Vector3 initalLocation;
    public static bool isDestroyed = false;
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
        // this is the AWSD or arrow keys
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveDirection.y = 1;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            moveDirection.y = -1;
        } else {
            moveDirection.y = 0;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveDirection.x = -1;
        } else if (Input.GetKey(KeyCode.RightArrow)) {
            moveDirection.x = 1;
        } else {
            moveDirection.x = 0;
        }
    }

    // Implement dash here - Ailey

}
