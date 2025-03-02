using System;
using Systems.Save.Base;
using Systems.Save.Utils;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "TimeData", menuName = "Datas/TimeData", order = 0)]
    public class TimeData : BaseScriptableData<TimeDataDataModel, TimeDataUserDataModel>
    {
        protected override string RemoteDataID { get; set; } = RemoteDataKey.None;
    }

    [Serializable]
    public class TimeDataDataModel : BaseDataModel<TimeDataDataModel>
    {

    }

    [Serializable]
    public class TimeDataUserDataModel : BaseDataModel<TimeDataUserDataModel>
    {
        public bool IsGameOpenedFirstTime;
        public string ExitTime;
        public int TimeDifferenceAsSeconds;
        public int ExitLastSecondForAutoProducing;
    }
}