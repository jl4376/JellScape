using UnityEngine;
using System.Collections.Generic;

public class Hazard : MonoBehaviour
{
    public int damage = 1;
    public float damageInterval = 1f;

    private Dictionary<Player, float> nextDamageTime = new Dictionary<Player, float>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                nextDamageTime[player] = Time.time + damageInterval;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null && Time.time >= nextDamageTime.GetValueOrDefault(player, 0))
            {
                player.TakeDamage(damage);
                nextDamageTime[player] = Time.time + damageInterval;
            }
        }
    }

    
}
