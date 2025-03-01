using System;
using Systems.Save.Base;
using Systems.Save.Utils;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Datas/GameData", order = 0)]
    public class GameData : BaseScriptableData<GameDataDataModel, GameDataUserDataModel> //Datalarda data ve user data modeller kullaniyorum
    {
        protected override string RemoteDataID { get; set; } = RemoteDataKey.None;
    }

    [Serializable]
    public class GameDataDataModel : BaseDataModel<GameDataDataModel> //Data model: developer tarafindan girilen ve kullanicinin degistiremedigi datalar
    {

    }

    [Serializable]
    public class GameDataUserDataModel : BaseDataModel<GameDataUserDataModel> //User data model: hem developer tarafindan hem de kullanici tarafindan degistirilebilen datalar
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