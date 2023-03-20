using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Socket : MonoBehaviour, IPointerClickHandler
{
    public SocketStateType socketStateType;
    public Item item;
    private Image _icon;

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
            return;
        }
        _icon.sprite = item.icon;
    }
}

public enum SocketStateType
{
    Equipped,
    Unequipped,
}