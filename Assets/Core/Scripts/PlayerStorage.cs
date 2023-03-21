using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PlayerStorage : MonoBehaviour
{
    public static PlayerStorage instance;
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentSocket[] equipmentSockets;
    [SerializeField] Transform _backpack;

    public static UnityAction<GameObject?> OnEquipLeftHand;
    public static UnityAction<GameObject?> OnEquipRightHand;
    public static UnityAction<GameObject?> OnHelmetChange;

    public static UnityAction<float, float, float> OnEquipmentStatChange;

    [SerializeField] Socket _socketPrefab;

    private void OnEnable()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    private void Awake()
    {
        SetupInventory();
        EvaluateStats();
    }

    private void SaveInventoryItemState(Item item, bool isEquipped, bool isLeftSide)
    {
        for(int i = 0; i < inventory.inventory.Length; i++)
        {
            if (inventory.inventory[i].item.id == item.id)
            {
                inventory.inventory[i].isLeftSide = isLeftSide;
                inventory.inventory[i].isEquipped = isEquipped;
            }
        }
    }

    private void SetupInventory()
    {
        inventory.inventory.ToList().ForEach(i => SpawnSocket(i));
    }

    private void SpawnSocket(InventoryStruct inventory)
    {
        var inst = Instantiate(_socketPrefab, _backpack);
        inst.item = inventory.item;

        if (inventory.isEquipped)
        {
            inst.socketStateType = SocketStateType.Equipped;
            var equipmentSocket = equipmentSockets
                .Where(e => e.equipmentType == inventory.item.itemType)
                .Where(e => e.isLeftHandSocket == inventory.isLeftSide)
                .FirstOrDefault();
            EquipEquipment(equipmentSocket, inst);
        }
        else
        {
            inst.socketStateType = SocketStateType.Unequipped;
        }
    }

    public void HandleItemRightClick(Socket socket)
    {
        var filteredEquipmentSockets = equipmentSockets.Where(s => s.equipmentType == socket.item.itemType).ToList();

        switch (socket.socketStateType)
        {
            case SocketStateType.Unequipped:
                var emptySocket = filteredEquipmentSockets.Where(s => s.socket == null).FirstOrDefault();
                if (emptySocket != null) // Found at least one free slot/socket
                {
                    EquipEquipment(emptySocket, socket);
                } 
                else
                {
                    // Replacing the worst item
                    var orderEquipmentByAverageStats = filteredEquipmentSockets.OrderByDescending(s => s.socket.item.stats.average).ToList();

                    var poorestItem = orderEquipmentByAverageStats[orderEquipmentByAverageStats.Count - 1];
                    UnequipEquipment(poorestItem);
                    EquipEquipment(poorestItem, socket);
                }
                break;
            case SocketStateType.Equipped:
                var theSameItemFound = filteredEquipmentSockets.Where(s => s.socket != null && s.socket.item.id == socket.item.id).FirstOrDefault();
                if (theSameItemFound != null)
                {
                    UnequipEquipment(theSameItemFound);
                }
                else
                {
                    Debug.LogWarning("never has happed");
                }
                break;
        }
        return;
    }

    private void UnequipEquipment(EquipmentSocket equipmentSocket)
    {
        equipmentSocket.socket.socketStateType = SocketStateType.Unequipped;
        SaveInventoryItemState(equipmentSocket.socket.item, false, false);
        equipmentSocket.socket.transform.SetParent(_backpack);
        equipmentSocket.socket = null;
        UpdateDisplayAvatar(equipmentSocket);
        EvaluateStats();
    }

    private void EquipEquipment(EquipmentSocket equipmentSocket, Socket item)
    {
        item.socketStateType = SocketStateType.Equipped;
        equipmentSocket.socket = item;
        SaveInventoryItemState(equipmentSocket.socket.item, true, equipmentSocket.isLeftHandSocket);
        equipmentSocket.socket.transform.SetParent(equipmentSocket._equipmentSocket);
        UpdateDisplayAvatar(equipmentSocket);
        EvaluateStats();
    }

    public void UpdateDisplayAvatar(EquipmentSocket equipmentSocket)
    {
        var prefab = equipmentSocket.socket?.item.prefab;
        switch (equipmentSocket.equipmentType)
        {
            case ItemType.Weapon:
                if (equipmentSocket.isLeftHandSocket)
                {
                    OnEquipLeftHand?.Invoke(prefab);
                }
                else
                {
                    OnEquipRightHand?.Invoke(prefab);
                }
                break;
            case ItemType.Helmet:
                OnHelmetChange?.Invoke(prefab);
                break;
            default:
                Debug.LogError("NOT IMPLEMENTED YET");
                break;
        }
    }

    private void EvaluateStats()
    {
        var stats = equipmentSockets.Where(c => c.socket != null).Select(c => c.socket.item.stats).ToList();
        var damage = stats.Select(c => c.damage).Sum();
        var defense = stats.Select(c => c.defense).Sum();
        var health = stats.Select(c => c.health).Sum();

        OnEquipmentStatChange?.Invoke(damage, defense, health);
    }
}

[System.Serializable]
public class EquipmentSocket
{
    public Socket socket;
    public ItemType equipmentType;
    public bool isLeftHandSocket;
    public Transform _equipmentSocket;
}