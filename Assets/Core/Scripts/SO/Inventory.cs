using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Core/Inventory")]
public class Inventory : ScriptableObject
{
    public InventoryStruct[] inventory;
}

[System.Serializable]
public struct InventoryStruct
{
    public Item item;
    public bool isEquipped;
    public bool isLeftSide;
}