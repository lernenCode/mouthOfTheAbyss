using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rope : MonoBehaviour
{
    [Header("Components")]
    private LineRenderer lineRenderer;

    [Header("Controlers")]
    public static bool ropeAdvancing;
    public static bool ropeReturning;
    public static bool collidingRope;
    public static bool pullingRope;

    [Header("Variables")]
    [SerializeField] private Transform pointA; // Ponto A
    [SerializeField] private Transform pointB; // Ponto B
    [SerializeField] private float maxDistance = 5f; // Distância máxima da corda
    [SerializeField] private float ropeSpeed = 1f; // Velocidade da corda

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(Player_Input.InputRope)
        {
            if(Player_Input.InputUp) {RopeVertical();}
            else {RopeHorizontal();}
        }
    }

    void RopeHorizontal()
    {
        lineRenderer.SetPosition(0, pointA.position);
    }

    void RopeVertical()
    {
        
    }
}