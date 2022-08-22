using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private float playInterval;

    static public DialogueController Instance { get; private set; }
    private bool isPlaying = false;
    private Queue<Dialogue> dialoguePlayQueue = new Queue<Dialogue>();

    private void Awake()
    {
        if( Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if( dialoguePlayQueue.Count>0 && isPlaying ==false)
        {
            Play(dialoguePlayQueue.Dequeue());
        }
    }
    public void Play(Dialogue dialogue)
    {
        if (isPlaying)
        {
            Debug.Log("In isplaying");
            dialoguePlayQueue.Enqueue(dialogue);
        }  
        else
        {
            Debug.Log("In else");
            StartCoroutine(PlaySequence(dialogue));
        }
            
    }
    IEnumerator PlaySequence(Dialogue dialogue)
    {
        isPlaying = true;
        for(int i = 0; i < dialogue.Sentences.Length; i++)
        {
            content.text = dialogue.Sentences[i].Content;
            Debug.Log("In loop");
            yield return new WaitForSeconds(playInterval);
        }
        content.text = string.Empty;
        isPlaying = false;
    }
}
