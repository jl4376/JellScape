using UnityEngine;

public class Player2 : Player
{
    protected override Vector2 ReadMovementInput()
    {
        var dir = Vector2.zero;
        if      (Input.GetKey(KeyCode.UpArrow))    dir.y = +1;
        else if (Input.GetKey(KeyCode.DownArrow))  dir.y = -1;
        if      (Input.GetKey(KeyCode.LeftArrow))  dir.x = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) dir.x = +1;
        return dir;
    }

    protected override bool ReadDashInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
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
