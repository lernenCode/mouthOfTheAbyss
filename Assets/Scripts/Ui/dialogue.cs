using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogLines;
    [SerializeField] private  Image whoTalking;
    [SerializeField] private float rangeTalk;
    [SerializeField] private LayerMask Player;

    private void Update() 
    {
        if(Input.GetKeyDown("k") && IsInRange() && dialogueManager.escrevendo == false)
        {
            dialogueManager.dialogLines = dialogLines;
            dialogueManager.whoTalking = whoTalking;

            if(dialogueManager.currentLine == 0 
            || dialogueManager.currentLine == dialogueManager.dialogLines.Length)
            {dialogueManager.isActive = !dialogueManager.isActive;}

            if (dialogueManager.isActive && dialogueManager.escrevendo == false)
            { StartCoroutine(dialogueManager.TypeText()); }
        }

        if (Player_CheckColision.inNpcRange == false)
        {
            dialogueManager.dialogText.text = "";
            dialogueManager.dialogLines = null;
            dialogueManager.whoTalking = null;
            dialogueManager.isActive = false;
            dialogueManager.currentLine = 0;
        }
    }

    private bool IsInRange()
    {return Physics2D.OverlapCircle(transform.position, rangeTalk, Player);}

    private void OnDrawGizmosSelected() 
   {
        // Desenhar range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangeTalk);
   }
}
