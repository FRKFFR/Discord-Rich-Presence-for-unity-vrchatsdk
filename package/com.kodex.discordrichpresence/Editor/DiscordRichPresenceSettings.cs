using UnityEngine;

namespace Kodex.DiscordRichPresence
{
    public class DiscordRichPresenceSettings : ScriptableObject
    {
        [Header("Discord Settings")]
        public string clientId = "1524073622950514800";
        public bool enableOnStart = true;
        public float updateInterval = 5f;
        
        [Header("Display Templates")]
        public string detailsTemplate = "Working on {projectName}";
        public string stateTemplate = "Scene: {sceneName}";
        public string largeImageKey = "unitywhite";
        public string largeImageText = "Unity Editor";
        public string smallImageKey = "unity";
        public string smallImageText = "Unity {unityVersion}";
        
        private static DiscordRichPresenceSettings instance;
        
        public static DiscordRichPresenceSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<DiscordRichPresenceSettings>("DiscordRichPresenceSettings");
                    if (instance == null)
                    {
                        instance = ScriptableObject.CreateInstance<DiscordRichPresenceSettings>();
                        #if UNITY_EDITOR
                        string folderPath = "Assets/Resources";
                        if (!System.IO.Directory.Exists(folderPath))
                        {
                            System.IO.Directory.CreateDirectory(folderPath);
                        }
                        UnityEditor.AssetDatabase.CreateAsset(instance, "Assets/Resources/DiscordRichPresenceSettings.asset");
                        UnityEditor.AssetDatabase.SaveAssets();
                        #endif
                    }
                }
                return instance;
            }
        }
        
        public void Save()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            #endif
        }
    }
}
