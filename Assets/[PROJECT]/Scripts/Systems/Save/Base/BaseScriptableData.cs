using System;
using Systems.Save.Utils;
using UnityEditor;
using UnityEngine;

namespace Systems.Save.Base
{
     public abstract class BaseScriptableData<TDataModel, TUserData> : ScriptableObject 
        where TDataModel : BaseDataModel<TDataModel>
        where TUserData : BaseDataModel<TUserData>
    {
        #region FIELDS

        public string objectName;
        [SerializeField] protected TDataModel defaultDataModel;
        [SerializeField] protected TUserData defaultUserData;

        protected abstract string RemoteDataID { get; set; }

        private bool _isInitialized;
        private TDataModel _activeDataModel;
        private TUserData _activeUserData;

        public TDataModel DataModel
        {
            get
            {
                /*if (_activeDataModel == null || string.IsNullOrEmpty(_activeDataModel.id))
                {
                    LoadDataModel();
                }
                return _activeDataModel;
                */

                return defaultDataModel;
            }
        }

        public TUserData UserData
        {
            get
            {
                if (_activeUserData == null || string.IsNullOrEmpty(_activeUserData.id))
                {
                    LoadUserData();
                }

                return _activeUserData;
            }
        }

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            if (_isInitialized) return;
            //LoadDataModel();
            LoadUserData();
            _isInitialized = true;
        }

        public void SaveUserData()
        {
            DataManager.SaveDataWithPath(defaultUserData.id, nameof(TUserData), _activeUserData.ToJsonString());
        }

        private void SaveDataModel()
        {
            DataManager.SaveDataWithPath(defaultDataModel.id, nameof(TDataModel), _activeDataModel.ToJsonString());
        }

        #endregion

        #region PRIVATE METHODS

        private bool TryGetDefaultDataModelFromRemote(out TDataModel dataFromRemote)
        {
            var dataFromRemoteAsString = DataManager.ReadDataWithPath(defaultDataModel.id, $"{RemoteDataID}");
            dataFromRemote = dataFromRemoteAsString.ToJsonObject<TDataModel>();
            return dataFromRemote != null && string.IsNullOrEmpty(dataFromRemote.id);
        }

        private void LoadDataModel()
        {
            if (_isInitialized) return;
            if (TryGetDefaultDataModelFromRemote(out TDataModel dataFromRemote))
            {
                _activeDataModel = dataFromRemote;
            }
            else
            {
                var dataModelFromStorage = DataManager.ReadDataWithPath(defaultDataModel.id, nameof(TDataModel)).ToJsonObject<TDataModel>();
                if (dataModelFromStorage == null || string.IsNullOrEmpty(dataModelFromStorage.id))
                {
                    dataModelFromStorage = defaultDataModel.Clone();
                }

                _activeDataModel = dataModelFromStorage;
            }

            SaveDefaultData();
        }

        protected virtual void LoadUserData()
        {
            if (_isInitialized) return;
            var userDataFromStorage = DataManager.ReadDataWithPath(defaultUserData.id, nameof(TUserData)).ToJsonObject<TUserData>();
            if (userDataFromStorage == null || string.IsNullOrEmpty(userDataFromStorage.id))
            {
                userDataFromStorage = defaultUserData.Clone();
            }

            _activeUserData = userDataFromStorage;
            SaveUserData();
        }

        private void SaveDefaultData()
        {
            DataManager.SaveDataWithPath(defaultDataModel.id, nameof(TDataModel), _activeDataModel.ToJsonString());
        }

        #endregion

        #region EDITOR

        protected virtual void OnValidateChild()
        {
            
        }

#if UNITY_EDITOR

        private void OnEnable() => EditorApplication.playModeStateChanged += OnplayModeStateChanged;
        private void OnDisable() => EditorApplication.playModeStateChanged -= OnplayModeStateChanged;

        private void OnplayModeStateChanged(PlayModeStateChange newState)
        {
            switch (newState)
            {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    ResetActiveDatas();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }   
        }

        private void ResetActiveDatas()
        {
            if (_activeDataModel != null)
            {
                _activeDataModel.id = "";
            }

            if (_activeUserData != null)
            {
                _activeUserData.id = "";
            }

            _activeDataModel = null;
            _activeUserData = null;
            _isInitialized = false;
        }

        protected void OnValidate()
        {
            OnValidateChild();
            string dataModelID = defaultDataModel.id;
            string userDataID = defaultUserData.id;
            if (!string.IsNullOrEmpty(dataModelID) && !string.IsNullOrEmpty(userDataID) && !dataModelID.Equals(userDataID)) return;

            defaultDataModel.id = Guid.NewGuid().ToString();
            defaultUserData.id = Guid.NewGuid().ToString();
        }

        [ContextMenu("Set ID")]
        private void SetId()
        {
            defaultDataModel.id = Guid.NewGuid().ToString();
            defaultUserData.id = Guid.NewGuid().ToString();
        }

#endif

        #endregion
    }
}