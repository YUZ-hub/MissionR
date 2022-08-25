using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private Image speakerImg;
    [SerializeField] private float intervalPerText;
    [SerializeField] private GameObject dialogueUI;


    public static DialogueController Instance { get; private set; }
    public bool IsPlaying { get; private set; } = false;
    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void Play(Dialogue dialogue)
    {
        if (IsPlaying==false)
        {
            StartCoroutine(PlaySequence(dialogue));
        }        
    }
    IEnumerator PlaySequence(Dialogue dialogue)
    {
        IsPlaying = true;
        dialogueUI.SetActive(true);
        for(int i = 0; i < dialogue.Sentences.Length; i++)
        {
            content.text = dialogue.Sentences[i].Content;
            speakerImg.sprite = dialogue.Sentences[i].Speaker.CharacterSprite;
            yield return new WaitForSeconds(intervalPerText*dialogue.Sentences[i].Content.Length+1f);
        }
        content.text = string.Empty;
        dialogueUI.SetActive(false);
        IsPlaying = false;
    }
}
