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

    [Header("Char Icon")]
    public static Image CharImage;
    public Image CharImageIcon;

    [Header("Support Icon")]
    public static Image supportImage;
    public Image SupportImageIcon;

    public static Image iconActivate;
    private void Start() 
    {
        barLife = lifeBarImage;
        CharImage = CharImageIcon;
        barEnergy = EnergyBarImage;
        barStamina = StaminaBarImage;
        supportImage = SupportImageIcon;
    }
   public static void chagerIconPlayer()
   {iconActivate = CharImage;}
   public static void chagerIconSupport()
   {iconActivate = supportImage;}
}
