using UnityEngine;
using UnityEngine.UI;

public class UI_Options : MonoBehaviour
{
    private Player player;
    [SerializeField] private Toggle healBarToggle;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        healBarToggle.onValueChanged.AddListener(OnHealthBarToggleChanged); 
    }

    private void OnHealthBarToggleChanged(bool isOn)
    {
        player.health.EnableHealthBar(isOn);
    }
}
