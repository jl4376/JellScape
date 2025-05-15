using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour
{
    // public string opponentTag;
    protected Rigidbody2D rigidBody;
    protected SpriteRenderer spriteRenderer;
    public int health;
    [SerializeField]protected float moveSpeed;
    [SerializeField]protected float friction;
    protected Vector2 moveDirection;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }


    void Start() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        CustomStart();
    }

    abstract protected void CustomStart();

    abstract protected void Move();
    
    void FixedUpdate() { Move(); }

    

    public void takeDamage(){ 
        health--;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
            // IMPLEMENT DEATH HERE
            // Destroy(gameObject);
        }
    }    


}