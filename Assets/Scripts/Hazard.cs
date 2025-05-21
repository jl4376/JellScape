using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private int damage = 1; // Default to 1, editable in Inspector

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player1 player = collision.gameObject.GetComponent<Player1>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }


}
