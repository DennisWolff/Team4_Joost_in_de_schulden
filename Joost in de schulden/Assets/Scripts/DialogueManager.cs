using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{

    [System.Serializable]
    public class DialogueSegment
    {
        public string SubjectText;

        [TextArea]
        public string DialogueToPrint;
        public bool skippable;

        [Range(1f, 25f)]
        public float LettersPerSecond;
    }

    [SerializeField] private DialogueSegment[] DialogueSegments;
    [Space]
    [SerializeField] private TMP_Text SubjectText;
    [SerializeField] private TMP_Text BodyText;

    private int DialogueIndex;
    private bool PlayingDialogue;
    private bool Skip;
    


    void Start()
    {
        StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (DialogueIndex == DialogueSegments.Length)
            {
                enabled = false;
                return;
                
            }

            if (!PlayingDialogue)
            {
                StartCoroutine(PlayDialogue(DialogueSegments[DialogueIndex]));
                
              

            }
            else
            {
                if (DialogueSegments[DialogueIndex].skippable)
                {
                    Skip = true;
                }
            }
        }
    }

    private IEnumerator PlayDialogue(DialogueSegment segment)
    {
        PlayingDialogue = true;

        BodyText.SetText(string.Empty);
        SubjectText.SetText(segment.SubjectText);

        float delay = 1f / segment.LettersPerSecond;
        for (int i = 0; i < segment.DialogueToPrint.Length; i++)
        {
            if (Skip)
            {
                BodyText.SetText(segment.DialogueToPrint);
                Skip = false;
                break;
            }

            string chunckToAdd = string.Empty;
            chunckToAdd += segment.DialogueToPrint[i];
            if(segment.DialogueToPrint[i] == ' ' && i < segment.DialogueToPrint.Length - 1)
            {
                chunckToAdd = segment.DialogueToPrint.Substring(i, 2);
                i++;
            }

            BodyText.text += chunckToAdd;
            yield return new WaitForSeconds(delay);

        }

       


        PlayingDialogue = false;
        DialogueIndex++;
        

    }
}
