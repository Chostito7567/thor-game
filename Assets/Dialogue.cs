using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;
        public string dialogue;
    }

    public TextMeshProUGUI textComponent;
    public GameObject dialogueUI;
    public GameObject odinSprite;
    public GameObject thorSprite;
    public DialogueLine[] lines;
    public float textSpeed = 0.05f;

    private int index;
    private bool isTyping = false;
    private Coroutine currentShake;

    void Start()
    {
        StartDialogue();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (!isTyping)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index].speaker + ": " + lines[index].dialogue;
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

        string speaker = lines[index].speaker;
        string dialogue = lines[index].dialogue;
        textComponent.text = speaker + ": ";

        // Stop shaking previous sprite
        if (currentShake != null)
        {
            StopCoroutine(currentShake);
        }

        // Start shaking the current speaker's sprite
        if (speaker == "Odin")
        {
            currentShake = StartCoroutine(Shake(odinSprite.transform));
        }
        else if (speaker == "Thor")
        {
            currentShake = StartCoroutine(Shake(thorSprite.transform));
        }

        foreach (char c in dialogue.ToCharArray())
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
            dialogueUI.SetActive(false);

            if (currentShake != null)
            {
                StopCoroutine(currentShake);
            }

            StartCoroutine(Jump(thorSprite.transform));
        }
    }

    IEnumerator Shake(Transform sprite)
    {
        Vector3 originalPos = sprite.localPosition;

        while (true)
        {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            sprite.localPosition = originalPos + new Vector3(x, y, 0) * 0.2f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Jump(Transform sprite)
    {
        Vector3 startPos = sprite.localPosition;
        float elapsedTime = 0f;
        float jumpHeight = 5f;
        float jumpDuration = 0.5f;

        while (elapsedTime < jumpDuration)
        {
            float progress = elapsedTime / jumpDuration;
            float yOffset = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
            sprite.localPosition = startPos + new Vector3(0, yOffset, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sprite.localPosition = startPos;
    }
}
