using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class autoConfiner : MonoBehaviour
{
    public CinemachineConfiner2D camConfiner;
    void OnTriggerEnter2D(Collider2D hit) 
    {
        if(hit.tag == "Player")
        {
            camConfiner.m_BoundingShape2D = gameObject.GetComponent<Collider2D>();
        }
    }
}
