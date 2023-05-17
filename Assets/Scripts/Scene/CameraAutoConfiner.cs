using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAutoConfiner : MonoBehaviour
{

public GameObject virtualcam;

private void OnTriggerEnter2D(Collider2D other) 
{
    if(other.CompareTag("Player")){
        virtualcam.SetActive(true);
    }
}

private void OnTriggerExit2D(Collider2D other) {
    if(other.CompareTag("Player")){
        virtualcam.SetActive(false);
    }
}
}