using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_UI : MonoBehaviour
{
    [Header("life Bar")]
    public static Image barLife;
    public Image lifeBarImage;

    [Header("Energy Bar")]
    public static Image barEnergy;
    public Image EnergyBarImage;

    [Header("Stamina Bar")]
    public static Image barStamina;
    public Image StaminaBarImage;

    public static Image iconActivate;
    private void Start() 
    {
        barLife = lifeBarImage;
        barEnergy = EnergyBarImage;
        barStamina = StaminaBarImage;
    }
}
