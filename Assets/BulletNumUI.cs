using UnityEngine;
using TMPro;

public class BulletNumUI : MonoBehaviour
{
    [SerializeField] private ShootController shoot;
    [SerializeField] private TextMeshProUGUI bulletNumText;


    private void Update()
    {
        if (shoot.gun == null)
        {
            bulletNumText.text = string.Empty;
        }
        else
        {
            bulletNumText.text = shoot.gun.bulletNum.ToString()+"/"+shoot.gun.Config.MagazineSize.ToString();
        }
    }
}
