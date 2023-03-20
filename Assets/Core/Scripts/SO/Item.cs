using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Core/Item")]
public class Item : ScriptableObject
{
    public string id;
    public string itemName;
    public ItemType itemType;
    public Stats stats;
    public Sprite icon;
    public GameObject prefab;

    private void Awake()
    {
        if (id == null)
        {
            id = Guid.NewGuid().ToString();
        }
    }
}
