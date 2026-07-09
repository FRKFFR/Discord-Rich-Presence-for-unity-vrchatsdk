using UnityEngine;
using DiscordRPC;

namespace Kodex.DiscordRichPresence
{
    public class DiscordRichPresence : MonoBehaviour
    {
        private DiscordRpcClient client;
        private RichPresence presence;
        
        [Header("Settings")]
        public string clientId = "YOUR_DISCORD_APPLICATION_ID";
        public bool enableOnStart = true;
        public float updateInterval = 15f;
        
        [Header("Display Templates")]
        public string detailsTemplate = "Working on {projectName}";
        public string stateTemplate = "Scene: {sceneName}";
        public string largeImageKey = "";
        public string largeImageText = "Unity Editor";
        public string smallImageKey = "";
        public string smallImageText = "";
        
        private float nextUpdateTime;
        private string currentScene;
        private string projectName;
        private string unityVersion;
        
        private void Start()
        {
            if (!enableOnStart) return;
            
            Initialize();
        }
        
        private void Update()
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
            if (Time.time >= nextUpdateTime)
            {
                nextUpdateTime = Time.time + updateInterval;
                UpdatePresence();
            }
        }
        
        private void OnDestroy()
        {
            if (client != null && client.IsInitialized)
            {
                client.ClearPresence();
                client.Dispose();
            }
        }
        
        private void OnApplicationQuit()
        {
            if (client != null && client.IsInitialized)
            {
                client.ClearPresence();
                client.Dispose();
            }
        }
        
        public void Initialize()
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
            
            nextUpdateTime = Time.time + updateInterval;
        }
        
        public void UpdatePresence()
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
        
        private string FormatTemplate(string template)
        {
            return template
                .Replace("{projectName}", projectName)
                .Replace("{sceneName}", currentScene)
                .Replace("{unityVersion}", unityVersion);
        }
        
        private string GetProjectName()
        {
            #if UNITY_EDITOR
            string[] s = Application.dataPath.Split('/');
            string projectName = s[s.Length - 2];
            return projectName;
            #else
            return Application.productName;
            #endif
        }
        
        public void SetClientId(string id)
        {
            clientId = id;
        }
        
        public void SetDetailsTemplate(string template)
        {
            detailsTemplate = template;
            UpdatePresence();
        }
        
        public void SetStateTemplate(string template)
        {
            stateTemplate = template;
            UpdatePresence();
        }
        
        public void SetLargeImage(string key, string text)
        {
            largeImageKey = key;
            largeImageText = text;
            UpdatePresence();
        }
        
        public void SetSmallImage(string key, string text)
        {
            smallImageKey = key;
            smallImageText = text;
            UpdatePresence();
        }
        
        public void SetUpdateInterval(float interval)
        {
            updateInterval = interval;
        }
    }
}
