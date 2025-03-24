using System;
using System.Collections.Generic;
using System.IO;
using BoothApp.Data;
using Newtonsoft.Json;
using UnityEngine;

#if UNITY_EDITOR
using Editor;
#endif

namespace BoothApp.Presentation
{
    [Serializable]
    public class BoothDataService : MonoBehaviour
    {
        #region Private Fields

        private string _applicationFilePath;

        #endregion

        #region Public Fields

        public const string FileExtension = ".json";
        public const string FolderPath = "\\boothData";

        public List<BoothData> data = new();

        #endregion

        #region MonoBehaviour Events

        private void Awake()
        {
            _applicationFilePath = Application.persistentDataPath;
            bool isFolder = CheckDataFolder();
            if (!isFolder) return;
            GetData();
        }

        #endregion

        #region Private Methods

        private bool CheckDataFolder()
        {
            DirectoryInfo directory = new DirectoryInfo(_applicationFilePath + FolderPath);
            Debug.Log(_applicationFilePath + FolderPath);
            if (!directory.Exists)
            {
                directory.Create();
                return false;
            }

            return true;
        }

        private void GetData()
        {
            DirectoryInfo directory = new DirectoryInfo(_applicationFilePath + FolderPath);
            foreach (var file in directory.GetFiles())
            {
                if (file.Extension.ToLower().CompareTo(FileExtension) == 0)
                {
                    var fileData = File.ReadAllText(_applicationFilePath + FolderPath + "\\" + file.Name);
                    data.Add(JsonConvert.DeserializeObject<BoothData>(fileData));
                }
            }
        }

        #endregion

        #region Public Methods
        
        [Button("Save")]
        public void SaveData()
        {
            foreach (var item in data)
            {
                //if (item.boothInformation.modifyAt > item.savedAt)
                {
                    item.savedAt = DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:sszzz");
                    File.WriteAllText(
                        _applicationFilePath + FolderPath + "\\" + item.boothInformationData.boothName + FileExtension,
                        JsonConvert.SerializeObject(item));
                }
            }
        }

        #endregion
    }
}