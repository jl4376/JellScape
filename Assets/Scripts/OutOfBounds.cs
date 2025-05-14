using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private Rigidbody2D rb;
    float x,y;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        y = Camera.main.orthographicSize;
        x = Camera.main.aspect*y;
    }

    void Update()
    {
        if (rb.position.x > x || rb.position.x < -x || rb.position.y > y || rb.position.y < -y)
        {
            // keep the whole player in bounds 
            rb.position = new Vector2(Mathf.Clamp(rb.position.x, -x, x), Mathf.Clamp(rb.position.y, -y, y));


        }
    }
}