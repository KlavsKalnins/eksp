using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScrollHeight : MonoBehaviour
{
    [SerializeField] int _slotsPerRow = 3;
    [SerializeField] float _slotHeight = 100f;
    [SerializeField] float _baseHeight = 200f;
    RectTransform _recTransform;

    private void Awake()
    {
        _recTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        SetInventoryScrollHeight();
    }

    void Update()
    {
        
    }
    public void SetInventoryScrollHeight()
    {
        var childCount = transform.childCount;
        _recTransform.sizeDelta = new Vector2 (_recTransform.sizeDelta.x, ((childCount / _slotsPerRow) * _slotHeight) + _baseHeight);
        gameObject.GetComponent<RectTransform>();
    }
}
