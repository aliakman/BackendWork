using System;
using System.Collections.Generic;
using Systems.Save.Base;
using Systems.Save.Utils;
using UnityEngine;

namespace Datas
{
    [CreateAssetMenu(fileName = "InventoryData", menuName = "Datas/InventoryData", order = 0)]
    public class InventoryData : BaseScriptableData<InventoryDataDataModel, InventoryDataUserDataModel>
    {
        protected override string RemoteDataID { get; set; } = RemoteDataKey.None;
    }

    [Serializable]
    public class InventoryDataDataModel : BaseDataModel<InventoryDataDataModel>
    {

    }

    [Serializable]
    public class InventoryDataUserDataModel : BaseDataModel<InventoryDataUserDataModel>
    {
        public List<InventorySlot> Inventory = new();
    }

    [System.Serializable]
    public class InventorySlot
    {
        public bool IsFilled;
        public bool IsFull;
        public string ItemName;
        public int ItemAmount;
    }

}