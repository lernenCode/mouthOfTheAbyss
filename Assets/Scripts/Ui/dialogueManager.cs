using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    // Declaração dos campos no topo da classe
    public static string[] dialogLines; // Array de strings com as linhas de diálogo
    public static Image whoTalking; // Referência para o componente Image que mostra o personagem que está falando
    public static TMPro.TextMeshProUGUI dialogText; // Referência para o componente Text que exibe o diálogo
    [SerializeField] private TMPro.TextMeshProUGUI dialogTextReal; // Referência para o componente Text que exibe o diálogo
    public static float typingDelay; // Intervalo de tempo entre as letras sendo digitadas
    [SerializeField] private float typingDelayReal; // Intervalo de tempo entre as letras sendo digitadas
    [SerializeField] private GameObject dialogBox; // Referência para o GameObject que representa a caixa de diálogo
    public static int currentLine; // Índice da linha atual de diálogo sendo exibida
    public static bool isActive = false; // Variável para controlar se o diálogo está ativo ou não
    public static bool escrevendo;

    private void Start() 
    {
        dialogText = dialogTextReal;
        typingDelay = typingDelayReal;
    }

    void Update()
    {
        // Atualiza a visibilidade da caixa de diálogo de acordo com o estado do diálogo
        dialogBox.SetActive(isActive);
    }

    public static IEnumerator TypeText()
    {
        isActive = true;
        escrevendo = true;
        // Verifica se há mais linhas de diálogo a serem exibidas
        if (currentLine < dialogLines.Length)
        {
            // Reseta o texto do diálogo
            dialogText.text = "";

            // Digita cada letra do texto da linha atual
            foreach (char letter in dialogLines[currentLine].ToCharArray())
            {
                dialogText.text += letter;
                yield return new WaitForSeconds(typingDelay);
            }

            // Incrementa o índice da linha atual e desativa o diálogo, se não houver mais linhas a serem exibidas
            currentLine++;
            if (currentLine >= dialogLines.Length)
            { 
                escrevendo = false;
                currentLine = 0;

                if(Input.GetKeyDown("k"))
                {
                    isActive = false; 
                }
            }
        }
        escrevendo = false;
    }
}
