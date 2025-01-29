using System;
using System.Collections.Generic;
using UiElements;

namespace SavesManagement
{
    [Serializable]
    public class GameData
    {
        public string saveName;
        public int currentLevel;
        public InventoryData inventoryData;
        public PlayerStatsData playerStatsData;

        public GameData(string name)
        {
            saveName = name;
            currentLevel = 1;
            inventoryData = new InventoryData();
            playerStatsData = new PlayerStatsData();
        }
    }

    [Serializable]
    public class InventoryData
    {
        public List<InventorySlotData> slots = new();
        public List<EquippedItemData> equippedItems = new();
    }

    [Serializable]
    public class PlayerStatsData
    {
        public float health;
        public float armorHead;
        public float armorBody;
    }

    [Serializable]
    public class InventorySlotData
    {
        public ItemType itemType;
        public int amount;
        public int slotIndex;
    }

    [Serializable]
    public class EquippedItemData
    {
        public ItemType itemType;
        public ArmorSlotType slotType;
    }
}