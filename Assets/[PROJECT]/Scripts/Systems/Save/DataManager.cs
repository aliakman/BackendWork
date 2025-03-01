using System;
using System.IO;
using Systems.Save.Utils;
using UnityEditor;
using UnityEngine;

namespace Systems.Save
{
    public static class DataManager
    {
        private const string Root = "ProjectRoot";

        #region HELPER

        private static string GetRootPath(string folderName = "")
        {
            string path = string.IsNullOrEmpty(folderName)
                ? Application.persistentDataPath + "/" + Root
                : Application.persistentDataPath + "/" + Root + "/" + folderName;
            return path;
        }

        private static string GetFilePath(string fileName, string folderName = "")
        {
            string path = string.IsNullOrEmpty(folderName)
                ? Application.persistentDataPath + "/" + Root + "/" + fileName + ".json"
                : Application.persistentDataPath + "/" + Root + "/" + folderName + "/" + fileName + ".json";
            return path;
        }

        #endregion

        #region PUBLIC METHODS
        
        public static T GetWithJson<T>(string key)
        {
            string jsonData = PlayerPrefs.GetString(key);
            return jsonData.ToJsonObject<T>();
        }
        
        public static void SaveWithJson<T>(string key, T data)
        {
            string jsonStringData = data.ToJsonString();
            PlayerPrefs.SetString(key, jsonStringData);
        }
        
        public static void RemoveData(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
        
        public static string ReadData(string fileName)
        {
            if (!Directory.Exists(GetRootPath())) Directory.CreateDirectory(GetRootPath());

            string path = GetFilePath(fileName);

            if (!File.Exists(path)) return null;

            string jsonStringData = File.ReadAllText(path);
            return jsonStringData;
        }

        public static string ReadDataWithPath(string fileName, string folderName)
        {
            if (!Directory.Exists(GetRootPath())) Directory.CreateDirectory(GetRootPath());
            if (!Directory.Exists(GetRootPath(folderName))) Directory.CreateDirectory(GetRootPath(folderName));

            string path = GetFilePath(fileName, folderName);

            if (!File.Exists(path)) return null;

            string jsonStringData = File.ReadAllText(path);
            return jsonStringData;
        }

        public static bool SaveData(string fileName, string stringData)
        {
            string path = GetFilePath(fileName);
            try
            {
                File.WriteAllText(path, stringData);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"CANNOT WRITE DATA TO FILE \n {e.Message}");
            }
        }

        public static bool SaveDataWithPath(string fileName, string folderName, string stringData)
        {
            if (!Directory.Exists(GetRootPath())) Directory.CreateDirectory(GetRootPath());
            if (!Directory.Exists(GetRootPath(folderName))) Directory.CreateDirectory(GetRootPath(folderName));

            string path = GetFilePath(fileName, folderName);
            try
            {
                File.WriteAllText(path, stringData);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"CANNOT WRITE DATA TO FILE \n {e.Message}");
            }
        }

        public static void DeleteDatas()
        {
            if (!Directory.Exists(GetRootPath())) return;

            Directory.Delete(GetRootPath(), true);
            PlayerPrefs.DeleteAll();
        }

        #endregion

        #region EDITOR TOOLS

#if UNITY_EDITOR
        [MenuItem("Data/Json/Delete All Saved Data")]
        public static void Delete()
        {
            try
            {
                if (!Directory.Exists(GetRootPath())) return;

                Directory.Delete(GetRootPath(), true);
                PlayerPrefs.DeleteAll();

                Debug.Log("ALL FILE DELETE SUCCESS");
            }
            catch (Exception e)
            {
                throw new Exception($"FILES DELETE : {e.Message}");
            }
        }
#endif

        #endregion
    }
}