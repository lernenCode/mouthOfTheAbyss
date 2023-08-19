using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    [Header("Ghost Effect")]
    [SerializeField] private float distanceBetweenImages;
    private float lastImageXpos;
    public /*static*/ bool isGhost;

    private void FixedUpdate()
    {
        if (isGhost)
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            lastImageXpos = transform.position.x;

            if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
            {
                lastImageXpos = transform.position.x;
            }
        }
    }
}
