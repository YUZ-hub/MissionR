using UnityEngine;

[CreateAssetMenu(fileName ="New Dialogue",menuName ="Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private Sentence[] sentences;

    public Sentence[] Sentences { get { return sentences; } private set { sentences = value; } }
}
