using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        public string dialogue;
    }

    public DialogueLine[] lines;
    public float textSpeed;

    private int index;
    private bool isTyping = false;
    private string currentSpeaker;
    private string fullLine;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTyping)
            {
                NextLine();
            }
            else
            {
                // Instantly complete the dialogue part
                StopAllCoroutines();
                textComponent.text = currentSpeaker + ": " + lines[index].dialogue;
                isTyping = false;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        // Set speaker immediately
        currentSpeaker = lines[index].speaker;
        textComponent.text = currentSpeaker + ": ";

        foreach (char c in lines[index].dialogue.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            // End of dialogue
            gameObject.SetActive(false);
        }
    }
}
