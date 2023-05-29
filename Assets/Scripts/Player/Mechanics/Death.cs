using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void reload()
    {
        gameObject.GetComponent<saveManager>().LoadPlayer();
    }
}
