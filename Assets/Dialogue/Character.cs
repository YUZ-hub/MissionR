using UnityEngine;

[CreateAssetMenu(fileName ="New Character",menuName ="Character")]
public class Character : ScriptableObject
{
    [SerializeField] private Sprite characterSprite;
    [SerializeField] private string characterName;
    public Sprite CharacterSprite { get { return characterSprite; } private set { characterSprite = value; } }
    public string CharacterName { get { return characterName; } private set { characterName = value; } }
}
