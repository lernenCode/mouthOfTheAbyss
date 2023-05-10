using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
    public static bool waitForInput = false;
    public static string[] dialogLines;
    public static Image whoTalking;
    public static TMPro.TextMeshProUGUI dialogText;
    public static GameObject dialogBox;
    [SerializeField] private TMPro.TextMeshProUGUI _dialogText;
    [SerializeField] private GameObject _dialogBox;
    public static int currentLine;
    public static bool isActive = false;
    public static bool finalLine = false;
    private void Start()
    {
        dialogText = _dialogText;
        dialogBox = _dialogBox;
    }

    void Update()
    {
        dialogBox.SetActive(isActive);
        if (finalLine && Input.GetKeyDown("k"))
        {
            isActive = false;
            finalLine = false;
            dialogBox.SetActive(false);
        }

    }

    public static void TypeText()
    {
        isActive = true;
        // Verifica se há mais linhas de diálogo a serem exibidas
        if (currentLine < dialogLines.Length)
        {
            // Reseta o texto do diálogo
            dialogText.text = dialogLines[currentLine];
            currentLine++;
        }
        if (currentLine >= dialogLines.Length)
        {
            finalLine = true;
            currentLine = 0;
            waitForInput = false;
        }
    }
}

