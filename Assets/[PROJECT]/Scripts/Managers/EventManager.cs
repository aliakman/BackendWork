using Datas;
using System;

namespace Managers
{
    public static class EventManager
    {
        public static ScriptHolder Scripts = new ScriptHolder();
        public static GameStateEvents GameStates = new GameStateEvents();

        public static Action LoadDatas;
        public static Func<GameData> GameData;
        public static Func<InventoryData> InventoryData;
        public static Func<TimeData> TimeData;

        public static Func<int, bool> AddMoney;
        public static Action IncreaseLevel;
        public static Action DecreaseLevel;

        public static Action<int> AddProduct;

        public static Func<int> TimeDifference;
    }

    public struct ScriptHolder //Script referanslarina bu eventlerle ulasilir
    {
        public Func<GameManager> GameManager;
        public Func<DataManager> DataManager;
    }

    public struct GameStateEvents //Oyun referanslarina bu eventlerle ulasilir
    {
        public Func<bool> IsGameStarted;
    }

}