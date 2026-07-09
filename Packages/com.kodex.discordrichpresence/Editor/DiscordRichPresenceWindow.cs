using UnityEngine;
using UnityEditor;

namespace Kodex.DiscordRichPresence
{
    public class DiscordRichPresenceWindow : EditorWindow
    {
        private DiscordRichPresenceSettings settings;
        private Vector2 scrollPosition;
        private bool showTemplates = true;
        private bool showImages = true;
        
        [MenuItem("Kodex Tools/Discord Rich Presence", false, 10)]
        public static void ShowWindow()
        {
            var window = GetWindow<DiscordRichPresenceWindow>("Discord Rich Presence");
            window.minSize = new Vector2(400, 500);
            window.Show();
        }
        
        private void OnEnable()
        {
            settings = DiscordRichPresenceSettings.Instance;
        }
        
        private void OnGUI()
        {
            if (settings == null)
            {
                EditorGUILayout.HelpBox("Settings not found. Creating new settings...", MessageType.Warning);
                settings = DiscordRichPresenceSettings.Instance;
                return;
            }
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            EditorGUILayout.Space(10);
            
            // Header
            GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 18,
                alignment = TextAnchor.MiddleCenter
            };
            EditorGUILayout.LabelField("Discord Rich Presence Settings", headerStyle);
            EditorGUILayout.Space(10);
            
            // Discord Settings Section
            EditorGUILayout.LabelField("Discord Settings", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            
            EditorGUI.BeginChangeCheck();
            
            string newClientId = EditorGUILayout.TextField("Discord Client ID", settings.clientId);
            if (EditorGUI.EndChangeCheck())
            {
                settings.clientId = newClientId;
                settings.Save();
            }
            
            if (string.IsNullOrEmpty(settings.clientId) || settings.clientId == "YOUR_DISCORD_APPLICATION_ID")
            {
                EditorGUILayout.HelpBox("Please set your Discord Application ID. Get one from: https://discord.com/developers/applications", MessageType.Warning);
            }
            
            EditorGUI.BeginChangeCheck();
            
            bool newEnableOnStart = EditorGUILayout.Toggle("Enable on Start", settings.enableOnStart);
            if (EditorGUI.EndChangeCheck())
            {
                settings.enableOnStart = newEnableOnStart;
                settings.Save();
            }
            
            EditorGUI.BeginChangeCheck();
            float newInterval = EditorGUILayout.Slider("Update Interval (seconds)", settings.updateInterval, 5f, 60f);
            if (EditorGUI.EndChangeCheck())
            {
                settings.updateInterval = newInterval;
                settings.Save();
            }
            
            EditorGUILayout.Space(15);
            
            // Templates Section
            showTemplates = EditorGUILayout.Foldout(showTemplates, "Display Templates", true);
            if (showTemplates)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space(5);
                
                EditorGUI.BeginChangeCheck();
                string newDetails = EditorGUILayout.TextField("Details Template", settings.detailsTemplate);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.detailsTemplate = newDetails;
                    settings.Save();
                }
                
                EditorGUI.BeginChangeCheck();
                string newState = EditorGUILayout.TextField("State Template", settings.stateTemplate);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.stateTemplate = newState;
                    settings.Save();
                }
                
                EditorGUILayout.Space(5);
                EditorGUILayout.HelpBox("Available variables: {projectName}, {sceneName}, {unityVersion}", MessageType.Info);
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(15);
            
            // Images Section
            showImages = EditorGUILayout.Foldout(showImages, "Image Settings", true);
            if (showImages)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space(5);
                
                EditorGUI.BeginChangeCheck();
                string newLargeKey = EditorGUILayout.TextField("Large Image Key", settings.largeImageKey);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.largeImageKey = newLargeKey;
                    settings.Save();
                }
                
                EditorGUI.BeginChangeCheck();
                string newLargeText = EditorGUILayout.TextField("Large Image Text", settings.largeImageText);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.largeImageText = newLargeText;
                    settings.Save();
                }
                
                EditorGUI.BeginChangeCheck();
                string newSmallKey = EditorGUILayout.TextField("Small Image Key", settings.smallImageKey);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.smallImageKey = newSmallKey;
                    settings.Save();
                }
                
                EditorGUI.BeginChangeCheck();
                string newSmallText = EditorGUILayout.TextField("Small Image Text", settings.smallImageText);
                if (EditorGUI.EndChangeCheck())
                {
                    settings.smallImageText = newSmallText;
                    settings.Save();
                }
                
                EditorGUILayout.Space(5);
                EditorGUILayout.HelpBox("Upload images to your Discord application at: https://discord.com/developers/applications", MessageType.Info);
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.Space(15);
            
            // Actions
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            
            if (GUILayout.Button("Save Settings", GUILayout.Height(30)))
            {
                settings.Save();
                EditorUtility.DisplayDialog("Settings Saved", "Your settings have been saved.", "OK");
            }
            
            if (GUILayout.Button("Open Discord Developer Portal", GUILayout.Height(30)))
            {
                Application.OpenURL("https://discord.com/developers/applications");
            }
            
            EditorGUILayout.Space(10);
            
            // Info
            EditorGUILayout.HelpBox("Add DiscordRichPresence component to a GameObject in your scene to enable Discord Rich Presence.", MessageType.Info);
            
            EditorGUILayout.EndScrollView();
        }
    }
}
