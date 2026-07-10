using UnityEngine;
using DiscordRPC;
using UnityEditor;

namespace Kodex.DiscordRichPresence
{
    [InitializeOnLoad]
    public class DiscordRichPresenceEditor
    {
        private static DiscordRpcClient client;
        private static RichPresence presence;
        
        private static string clientId = "YOUR_DISCORD_APPLICATION_ID";
        private static bool enabled = true;
        private static float updateInterval = 15f;
        
        private static string detailsTemplate = "Working on {projectName}";
        private static string stateTemplate = "Scene: {sceneName}";
        private static string largeImageKey = "";
        private static string largeImageText = "Unity Editor";
        private static string smallImageKey = "";
        private static string smallImageText = "";
        
        private static float nextUpdateTime;
        private static string currentScene;
        private static string projectName;
        private static string unityVersion;
        
        static DiscordRichPresenceEditor()
        {
            LoadSettings();
            
            if (!enabled) return;
            
            EditorApplication.update += OnEditorUpdate;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
            
            EditorApplication.delayCall += Initialize;
        }
        
        private static void OnEditorUpdate()
        {
            if (client == null || !client.IsInitialized) return;
            
            // Check for scene changes
            string newScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            if (newScene != currentScene)
            {
                currentScene = newScene;
                UpdatePresence();
            }
            
            // Periodic updates
            if (EditorApplication.timeSinceStartup >= nextUpdateTime)
            {
                nextUpdateTime = EditorApplication.timeSinceStartup + updateInterval;
                UpdatePresence();
            }
        }
        
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                // Optional: handle play mode
            }
            else if (state == PlayModeStateChange.ExitingPlayMode)
            {
                // Optional: handle exit play mode
            }
        }
        
        private static void Initialize()
        {
            if (string.IsNullOrEmpty(clientId) || clientId == "YOUR_DISCORD_APPLICATION_ID")
            {
                Debug.LogWarning("[Discord Rich Presence] No valid Client ID set. Please configure in Kodex Tools > Discord Rich Presence");
                return;
            }
            
            // Get project info
            projectName = GetProjectName();
            unityVersion = Application.unityVersion;
            currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            
            // Initialize Discord RPC
            client = new DiscordRpcClient(clientId);
            
            client.OnReady += (sender, e) =>
            {
                Debug.Log("[Discord Rich Presence] Connected to Discord");
                UpdatePresence();
            };
            
            client.OnError += (sender, e) =>
            {
                Debug.LogError($"[Discord Rich Presence] Error: {e.Message}");
            };
            
            client.Initialize();
            
            nextUpdateTime = EditorApplication.timeSinceStartup + updateInterval;
        }
        
        private static void UpdatePresence()
        {
            if (client == null || !client.IsInitialized) return;
            
            presence = new RichPresence
            {
                Details = FormatTemplate(detailsTemplate),
                State = FormatTemplate(stateTemplate),
                Assets = new Assets()
            };
            
            if (!string.IsNullOrEmpty(largeImageKey))
            {
                presence.Assets.LargeImageKey = largeImageKey;
                presence.Assets.LargeImageText = FormatTemplate(largeImageText);
            }
            
            if (!string.IsNullOrEmpty(smallImageKey))
            {
                presence.Assets.SmallImageKey = smallImageKey;
                presence.Assets.SmallImageText = FormatTemplate(smallImageText);
            }
            
            client.SetPresence(presence);
        }
        
        private static string FormatTemplate(string template)
        {
            return template
                .Replace("{projectName}", projectName)
                .Replace("{sceneName}", currentScene)
                .Replace("{unityVersion}", unityVersion);
        }
        
        private static string GetProjectName()
        {
            string[] s = Application.dataPath.Split('/');
            return s[s.Length - 2];
        }
        
        private static void LoadSettings()
        {
            var settings = DiscordRichPresenceSettings.Instance;
            if (settings != null)
            {
                clientId = settings.clientId;
                enabled = settings.enabled;
                updateInterval = settings.updateInterval;
                detailsTemplate = settings.detailsTemplate;
                stateTemplate = settings.stateTemplate;
                largeImageKey = settings.largeImageKey;
                largeImageText = settings.largeImageText;
                smallImageKey = settings.smallImageKey;
                smallImageText = settings.smallImageText;
            }
        }
        
        public static void ReloadSettings()
        {
            if (client != null && client.IsInitialized)
            {
                client.ClearPresence();
                client.Dispose();
                client = null;
            }
            
            LoadSettings();
            Initialize();
        }
        
        public static void SetClientId(string id)
        {
            clientId = id;
        }
        
        public static void SetEnabled(bool value)
        {
            enabled = value;
        }
        
        public static void SetUpdateInterval(float interval)
        {
            updateInterval = interval;
        }
        
        public static void SetDetailsTemplate(string template)
        {
            detailsTemplate = template;
        }
        
        public static void SetStateTemplate(string template)
        {
            stateTemplate = template;
        }
        
        public static void SetLargeImage(string key, string text)
        {
            largeImageKey = key;
            largeImageText = text;
        }
        
        public static void SetSmallImage(string key, string text)
        {
            smallImageKey = key;
            smallImageText = text;
        }
    }
}
