using UnityEngine;
using UnityEditor;
using System.IO;

namespace Kodex.DiscordRichPresence
{
    public class SetupScript
    {
        [MenuItem("Kodex Tools/Discord Rich Presence/Download DiscordRPC.dll", false, 11)]
        public static void DownloadDiscordRpcDll()
        {
            string dllUrl = "https://github.com/Lachee/discord-rpc-csharp/releases/download/3.0.0/DiscordRPC.zip";
            string runtimePath = "Packages/com.kodex.discordrichpresence/Runtime";
            string zipPath = Path.Combine(runtimePath, "DiscordRPC.zip");
            string dllPath = Path.Combine(runtimePath, "DiscordRPC.dll");
            
            // Check if DLL already exists
            if (File.Exists(dllPath))
            {
                if (EditorUtility.DisplayDialog("DiscordRPC.dll Found", 
                    "DiscordRPC.dll already exists in the Runtime folder. Do you want to redownload it?", 
                    "Redownload", "Cancel"))
                {
                    File.Delete(dllPath);
                }
                else
                {
                    return;
                }
            }
            
            // Download the zip file
            EditorUtility.DisplayProgressBar("Downloading DiscordRPC.dll", "Downloading from GitHub...", 0.5f);
            
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.DownloadFile(dllUrl, zipPath);
                }
                
                EditorUtility.DisplayProgressBar("Downloading DiscordRPC.dll", "Extracting...", 0.8f);
                
                // Extract the DLL from the zip
                System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, runtimePath);
                
                // Clean up zip file
                File.Delete(zipPath);
                
                // Also delete the extracted folder if it exists (we only need the DLL)
                string extractedFolder = Path.Combine(runtimePath, "lib");
                if (Directory.Exists(extractedFolder))
                {
                    Directory.Delete(extractedFolder, true);
                }
                
                EditorUtility.ClearProgressBar();
                
                AssetDatabase.Refresh();
                
                EditorUtility.DisplayDialog("Success", 
                    "DiscordRPC.dll has been downloaded and placed in the Runtime folder.\n\nYou can now use Discord Rich Presence!", 
                    "OK");
            }
            catch (System.Exception e)
            {
                EditorUtility.ClearProgressBar();
                EditorUtility.DisplayDialog("Error", 
                    $"Failed to download DiscordRPC.dll: {e.Message}\n\nPlease download it manually from:\n{dllUrl}", 
                    "OK");
                
                // Clean up if download failed
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }
            }
        }
        
        [MenuItem("Kodex Tools/Discord Rich Presence/Open GitHub Release Page", false, 12)]
        public static void OpenGitHubRelease()
        {
            Application.OpenURL("https://github.com/Lachee/discord-rpc-csharp/releases");
        }
    }
}
