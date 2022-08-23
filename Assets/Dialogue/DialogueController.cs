using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private float playInterval;

    private bool isPlaying = false;
    private Queue<Dialogue> dialoguePlayQueue = new Queue<Dialogue>();

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
            dialoguePlayQueue.Enqueue(dialogue);
        }  
        else if(dialogue.IsPlayed() == false)
        {
            dialogue.SetPlayed();
            StartCoroutine(PlaySequence(dialogue));
        }
            
    }
    IEnumerator PlaySequence(Dialogue dialogue)
    {
        isPlaying = true;
        for(int i = 0; i < dialogue.Sentences.Length; i++)
        {
            content.text = dialogue.Sentences[i].Content;
            yield return new WaitForSeconds(playInterval);
        }
        content.text = string.Empty;
        isPlaying = false;
    }
}
