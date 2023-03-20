using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Socket : MonoBehaviour, IPointerClickHandler
{
    public SocketStateType socketStateType;
    public Item item;
    private Image _icon;
    [SerializeField] TMP_Text _displayText;

    private void Awake()
    {
        _icon = GetComponent<Image>();
    }

    void Start()
    {
        SetSocket();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            PlayerStorage.instance.HandleItemRightClick(this);
        }
    }

    public void SetSocket()
    {
        if (item == null)
        {
            Debug.LogWarning("not impl");
            _displayText.SetText(string.Empty);
            return;
        }
        _icon.sprite = item.icon;
        var stats = item.stats;
        _displayText.SetText($"D:{stats.damage} Def: {stats.defense} HP: {stats.health}");
    }
}

public enum SocketStateType
{
    Equipped,
    Unequipped,
}