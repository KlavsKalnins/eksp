using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] TMP_Text _displayText;

    private void OnEnable()
    {
        PlayerStorage.OnEquipmentStatChange += SetDisplayText;
    }

    private void OnDisable()
    {
        PlayerStorage.OnEquipmentStatChange -= SetDisplayText;
    }

    public void SetDisplayText(float damage, float defense, float health)
    {
        var formatString = $"HP: {health} ; DEF: {defense} ; Dmg: {damage}";
        _displayText.SetText(formatString);
    }
}
