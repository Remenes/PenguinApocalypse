using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public float damage;
    public float fireRate;

    private float lastAttack; //The time of the last attack

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Attack()
    {
        if (lastAttack + fireRate < Time.time) //If enough time has passed to attack again, attack
        {
            Debug.Log(gameObject.name + " has just attacked.");
            //player.GetComponent<Health>().TakeDamage(damage);
            lastAttack = Time.time;
        }
    }
}
