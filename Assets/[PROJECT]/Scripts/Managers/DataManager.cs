using Datas;
using UnityEngine;

namespace Managers
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private InventoryData inventoryData;
        [SerializeField] private TimeData timeData;

        private void Awake()
        {
            gameData.Initialize();
            inventoryData.Initialize();
            timeData.Initialize();

            EventManager.Scripts.DataManager += GetDataManager;
            EventManager.GameData += GetGameData;
            EventManager.InventoryData += GetInventoryData;
            EventManager.TimeData += GetTimeData;
        }

        private void OnDisable()
        {
            EventManager.Scripts.DataManager -= GetDataManager;
            EventManager.GameData -= GetGameData;
            EventManager.InventoryData -= GetInventoryData;
            EventManager.TimeData -= GetTimeData;
        }

        private void Start()
        {
            EventManager.LoadDatas?.Invoke();
        }

        private DataManager GetDataManager() { return this; }
        private GameData GetGameData() { return gameData; }
        private InventoryData GetInventoryData() { return inventoryData; }
        private TimeData GetTimeData() { return timeData; }

    }
}