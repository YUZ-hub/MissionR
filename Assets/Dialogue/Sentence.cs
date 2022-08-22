using UnityEngine;

[CreateAssetMenu(fileName ="New Sentence",menuName ="Sentence")]
public class Sentence : ScriptableObject
{
    [SerializeField] private string content;
    public string Content { get { return content; } private set { content = value; } }
}
