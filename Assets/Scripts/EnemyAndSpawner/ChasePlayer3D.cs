using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer3D : MonoBehaviour
{
    public float speed;
    public float senseDist;
    public float targetDist;
    public enum lookAtOptions { None, Rotate, Flip }
    public lookAtOptions lookAtPlayer;

    GameObject player;
    Rigidbody rb;
    SpriteRenderer spriteRend;
    AttackPlayer attack;
    NavMeshAgent nav;

    private bool defaultFlipX;
    private bool defaultFlipY;
    private bool foundNavMesh;

    [HideInInspector]
    public float distFromPlayer
    {
        get
        {
            return Vector3.Distance(transform.position, player.transform.position);
        }
    }
	
	private void Awake ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        attack = GetComponent<AttackPlayer>();
        nav = GetComponent<NavMeshAgent>();

        defaultFlipX = spriteRend.flipX;
        defaultFlipY = spriteRend.flipY;

        foundNavMesh = (nav != null);
        Debug.Log("'Found navmesh' on " + gameObject.name + " = " + foundNavMesh);
    }
	
	private void FixedUpdate ()
    {
        if (distFromPlayer < senseDist && distFromPlayer > targetDist) //In range but can't attack, chase player
        {
            //Debug.Log("Following player.");
            Chase();
        }
        else if (distFromPlayer < senseDist && distFromPlayer < targetDist) //In range and can attack, attack 
        {
            //Debug.Log("Attacking palyer.");
            StopChase();
            attack.Attack();
        }
        else if (distFromPlayer > senseDist) //Out of range, stop
        {
            StopChase();
        }

        //Look at the player
        #region lookAtPlayer
        if (distFromPlayer < senseDist) //In range
        {
            if (lookAtPlayer == lookAtOptions.Rotate)
            {
                //transform.LookAt(player.transform);
                transform.right = player.transform.position - transform.position;

                if (player.transform.position.x >= transform.position.x) //Player is to the right, face right
                {
                    spriteRend.flipY = defaultFlipY;
                }
                else if (player.transform.position.x < transform.position.x) //Player is to the left, face left
                {
                    spriteRend.flipY = !defaultFlipY;
                }
            }
            else if (lookAtPlayer == lookAtOptions.Flip)
            {
                if (player.transform.position.x >= transform.position.x) //Player is to the right, face right
                {
                    spriteRend.flipX = defaultFlipX;
                }
                else if (player.transform.position.x < transform.position.x) //Player is to the left, face left
                {
                    spriteRend.flipX = !defaultFlipX;
                }
            }
        }
        #endregion lookAtPlayer
    }

    void Chase()
    {
        if (!foundNavMesh) //No navmesh found, use default AI
        {
            Vector3 playerDirection = player.transform.position - transform.position;
            Vector3 force = playerDirection.normalized * speed;

            rb.AddForce(force, ForceMode.Acceleration);

            if (rb.velocity.magnitude > speed)
            {
                rb.velocity = rb.velocity * (speed / rb.velocity.magnitude);
            }
        }
        else //Navmesh found, use it
        {
            nav.SetDestination(player.transform.position);
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
