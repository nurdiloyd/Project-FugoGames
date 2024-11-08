using System;
using System.IO;
using UnityEngine;

namespace Main.Scripts.General
{
    public class DataManager : IContextUnit
    {
        private static readonly string PersistentPath = Application.persistentDataPath;
        
        public UserData User { get; private set; }
        
        public void Bind()
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            var destinationPath = PersistentPath;
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }
            
            var userDataPath = Path.Combine(destinationPath, "UserData.json");
            if (File.Exists(userDataPath))
            {
                var jsonFile = File.ReadAllText(userDataPath);
                var userData = JsonUtility.FromJson<UserData>(jsonFile);
                User = userData;
            }
            else
            {
                User = new UserData();
                SaveUserData();
            }
        }
        
        public void IncreaseCurrentLevelIndex()
        {
            User.levelIndex += 1;
            SaveUserData();
        }
        
        private void SaveUserData()
        {
            var destinationPath = PersistentPath;
            var userDataPath = Path.Combine(destinationPath, "UserData.json");
            var jsonFile = JsonUtility.ToJson(User);
            File.WriteAllText(userDataPath, jsonFile);
        }
    }
    
    [Serializable]
    public class UserData
    {
        public int levelIndex;
    }
}
