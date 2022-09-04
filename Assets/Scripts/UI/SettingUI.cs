using UnityEngine;
using UnityEngine.UI;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Slider volume;
    [SerializeField] private Setting setting;

    private void OnEnable()
    {
        volume.value = setting.Volume;
    }
}
