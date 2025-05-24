using UnityEngine;

public class Player1 : Player
{
    protected override Vector2 ReadMovementInput()
    {
        var dir = Vector2.zero;
        if      (Input.GetKey(KeyCode.W)) dir.y = +1;
        else if (Input.GetKey(KeyCode.S)) dir.y = -1;
        if      (Input.GetKey(KeyCode.A)) dir.x = -1;
        else if (Input.GetKey(KeyCode.D)) dir.x = +1;
        return dir;
    }

    protected override bool ReadDashInput()
    {
        return Input.GetKeyDown(KeyCode.Tab);
    }
}
