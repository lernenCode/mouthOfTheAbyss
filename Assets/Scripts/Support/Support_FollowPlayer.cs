using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Support_FollowPlayer : MonoBehaviour
{
    public float moveSpeed;
    public float debuffSpeed;
    private float startSpeed;
    public Transform target;
    public static Rigidbody2D supportRB;
    public float stoppingDistance;
    private float dist;

    private void Start()
    {
        supportRB = GetComponent<Rigidbody2D>();

        startSpeed = moveSpeed;
    }
    void Update()
    {
        // So pode fazer enquanto personagem vivo
        if(player_status.isDie == false)
        {
            if (Player_Carried.HolderItem != null)
            {
                moveSpeed = debuffSpeed;
            } else { moveSpeed = startSpeed; }
            
            if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            }

            #region Flip
                dist = transform.position.x - target.position.x;
                if (dist > 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }
                else { transform.localRotation = Quaternion.Euler(0, 0, 0); }
            #endregion
        }

    }
}
