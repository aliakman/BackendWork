using System;
using Systems.Save.Utils;

namespace Systems.Save.Base
{
    [Serializable]
    public class BaseDataModel<T>
    {
        public string id;

        /// <summary>
        /// Deep copies this object. (not copies private fields)
        /// </summary>
        /// <returns></returns>
        public virtual T Clone()
        {
            string json = this.ToJsonString();
            T returnedData = json.ToJsonObject<T>();
            return (T)Convert.ChangeType(returnedData, typeof(T));
        }
    }
}