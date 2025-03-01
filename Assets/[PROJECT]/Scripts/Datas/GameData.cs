using System;
using Systems.Save.Base;
using Systems.Save.Utils;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Datas/GameData", order = 0)]
    public class GameData : BaseScriptableData<GameDataDataModel, GameDataUserDataModel>
    {
        protected override string RemoteDataID { get; set; } = RemoteDataKey.None;
    }

    [Serializable]
    public class GameDataDataModel : BaseDataModel<GameDataDataModel>
    {

    }

    [Serializable]
    public class GameDataUserDataModel : BaseDataModel<GameDataUserDataModel>
    {
        [Header("General Fields")]
        public string Username;
        public int Level;
        public int Money;

        [Header("Production Fields")]
        public int ProductionSpeedAsSeconds;
        public int ProductAmount;
        public int MaxCapacity;
    }
}