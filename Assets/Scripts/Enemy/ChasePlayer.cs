using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float speed;
    public float senseDist;
    public float targetDist;
    public enum lookAtOptions { None, Rotate, Flip }
    public lookAtOptions lookAtPlayer;

    GameObject player;
    Rigidbody2D rb;
    SpriteRenderer spriteRend;
    AttackPlayer attack;

    public float distFromPlayer
    {
        get
        {
            return Vector2.Distance(transform.position, player.transform.position);
        }
    }
	
	private void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<AttackPlayer>();
    }
	
	private void FixedUpdate ()
    {
        Chase();
	}

    void Chase()
    {
        if (distFromPlayer < senseDist && distFromPlayer > targetDist) //In range but can't attack, chase player
        {
            Vector2 playerDirection = player.transform.position - transform.position;
            Vector2 force = playerDirection.normalized * speed * rb.mass;

            rb.AddForce(force, ForceMode2D.Force);

            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity * (speed / rb.velocity.magnitude);
            }
        }
        else if (distFromPlayer < senseDist && distFromPlayer < targetDist) //In range and can attack, attack 
        {
            StopChase();
            attack.Attack();
        }
        else if (distFromPlayer > senseDist) //Out of range, stop
        {
            StopChase();
        }

        if (distFromPlayer < senseDist) //In range
        {
            if (lookAtPlayer == lookAtOptions.Rotate)
            {
                //transform.LookAt(player.transform);
                transform.right = player.transform.position - transform.position;
            }
            else if (lookAtPlayer == lookAtOptions.Flip)
            {
                if(player.transform.position.x >= transform.position.x) //Player is to the right, face right
                {
                    spriteRend.flipX = false;
                }
                else if (player.transform.position.x < transform.position.x) //Player is to the left, face left
                {
                    spriteRend.flipX = true;
                }
            }
        }
    }

    public void StopChase()
    {
        bool instaStop = false;
        if (instaStop)
        {
            rb.velocity = Vector2.zero;
        }
        else
        {
            if (rb.velocity.magnitude > 0.5f) //Going fast, slow down
            {
                //Debug.Log(rb.velocity);
                rb.velocity = rb.velocity * 0.75f;
            }
            else //going slow, stop
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
