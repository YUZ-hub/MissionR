using UnityEngine;

[System.Serializable]
public class Sentence
{
    [SerializeField] private string content;
    public string Content { get { return content; } private set { content = value; } }
}
[CreateAssetMenu(fileName ="New Dialogue",menuName ="Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private Sentence[] sentences;
    [SerializeField] private bool isPlayed;

    public Sentence[] Sentences { get { return sentences; } private set { sentences = value; } }
    public bool IsPlayed()
    {
        return isPlayed;
    }
    public void SetPlayed()
    {
        isPlayed = true;
    }
}
