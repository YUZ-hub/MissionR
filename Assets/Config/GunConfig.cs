using UnityEngine;


[System.Serializable]
public enum GunType
{
    pistol, rocket
}
[CreateAssetMenu(fileName ="New Gun",menuName ="Config/Gun")]
public class GunConfig : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GunType type;
    [SerializeField] private int magazineSize;
    [SerializeField] private Sound reloadSound;
    [SerializeField] private Sound shootSound;
    [SerializeField] private Sound emptySound;
    [SerializeField] private float shootCD;

    public GameObject BulletPrefab { get { return bulletPrefab; } private set { value = bulletPrefab; } }
    public int MagazineSize { get { return magazineSize; } private set { value = magazineSize; } }
    public Sound ReloadSound { get { return reloadSound; } private set { value = reloadSound; } }
    public Sound ShootSound { get { return shootSound; } private set { value = shootSound; } }
    public Sound EmptySound { get { return emptySound; } private set { value = emptySound; } }
    public float ShootCD { get { return shootCD; } private set { value = shootCD; } }
    public GunType Type { get { return type; } private set { value = type; } }
}
