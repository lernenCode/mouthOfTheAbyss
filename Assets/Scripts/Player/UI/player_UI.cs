using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    public static Image barLife;
    public Image lifeBarImage;

    public static Image barEnergy;
    public Image EnergyBarImage;

    public static Image barStamina;
    public Image StaminaBarImage;
    private void Start() 
    {
        barLife = lifeBarImage;
        barEnergy = EnergyBarImage;
        barStamina = StaminaBarImage;
    }
}
