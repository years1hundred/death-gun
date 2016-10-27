using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    public Image npcImage;
    public Sprite npcSprite;
    public bool dialogueActive;
    public string[] dialogueLines;
    public int currentdialogueLine;



    void Start()
    {

    }



    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentdialogueLine++;
        }

        if (currentdialogueLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;

            currentdialogueLine = 0;
        }

        dialogueText.text = dialogueLines[currentdialogueLine];

        npcImage.GetComponent<Image>().sprite = npcSprite;
    }



    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
    }
}
