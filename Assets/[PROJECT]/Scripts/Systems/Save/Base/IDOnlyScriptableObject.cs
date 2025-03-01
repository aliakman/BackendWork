using System;
using UnityEngine;

namespace Systems.Save.Base
{
    public abstract class IDOnlyScriptableObject : ScriptableObject
    {
        public string id;
        public string objectName;

        protected void OnValidate()
        {
#if UNITY_EDITOR

            if (!string.IsNullOrEmpty(id)) return;
            id = Guid.NewGuid().ToString();
#endif
        }

        [ContextMenu("Set New Id")]
        public void SetNewId()
        {
            id = Guid.NewGuid().ToString();
        }
    }
}