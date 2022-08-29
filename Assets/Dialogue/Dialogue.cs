using UnityEngine;

[System.Serializable]
public class Sentence
{
    [SerializeField] private string content;
    [SerializeField] private Character speaker;
    public string Content { get { return content; } private set { content = value; } }
    public Character Speaker { get { return speaker; } private set { speaker = value; } }
}
[CreateAssetMenu(fileName ="New Dialogue",menuName ="Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private Sentence[] sentences;
    [SerializeField] private bool isPlayed = false;
    public Sentence[] Sentences { get { return sentences; } private set { sentences = value; } }
    public bool IsPlayed { get { return isPlayed; } private set { isPlayed = value; } }

    public void SetPlayed()
    {
        IsPlayed = true;
    }
}
