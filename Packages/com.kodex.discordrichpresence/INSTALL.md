# Installation Guide

## VPM Installation (Recommended)

### Step 1: Add Repository to VPM

1. Open Unity
2. Go to **Edit > Project Settings > VPM Settings** (or use the VCC if using VRChat SDK)
3. Add this repository URL to your VPM repositories

### Step 2: Install Package

1. Open the Package Manager (Window > Package Manager)
2. Select "My Registries" from the dropdown
3. Find "Kodex Discord Rich Presence"
4. Click Install

### Step 3: Download DiscordRPC.dll

1. In Unity, go to **Kodex Tools > Discord Rich Presence > Download DiscordRPC.dll**
2. The DLL will be automatically downloaded and placed in the correct location
3. Alternatively, download manually from: https://github.com/Lachee/discord-rpc-csharp/releases
4. Place `DiscordRPC.dll` in `Packages/com.kodex.discordrichpresence/Runtime/`

### Step 4: Configure

1. Go to **Kodex Tools > Discord Rich Presence**
2. Enter your Discord Application ID
3. Customize your settings
4. Click "Save Settings"

### Step 5: Add to Scene

1. Create a new GameObject in your scene
2. Add the `DiscordRichPresence` component
3. The component will automatically use your saved settings

## Manual Installation

### Step 1: Copy Package

1. Copy the `com.kodex.discordrichpresence` folder to your Unity project's `Packages` folder

### Step 2: Download DiscordRPC.dll

1. Download DiscordRPC.dll from: https://github.com/Lachee/discord-rpc-csharp/releases
2. Place it in `Packages/com.kodex.discordrichpresence/Runtime/`

### Step 3: Configure

Follow steps 4-5 from the VPM installation above.

## Creating a Discord Application

1. Go to [Discord Developer Portal](https://discord.com/developers/applications)
2. Click "New Application"
3. Give it a name (e.g., "Unity RPC")
4. Go to "Rich Presence" in the left sidebar
5. Click "Enable Rich Presence"
6. Copy the **Application ID** (you'll need this for the settings)

## Adding Custom Images

1. Go to your Discord application in the Developer Portal
2. Navigate to "Rich Presence" → "Art Assets"
3. Upload your images (512x512 recommended)
4. Note the image keys (names you give them)
5. Use these keys in the Unity settings window

## Troubleshooting

**Package not showing in Package Manager**
- Make sure you added the repository URL to VPM settings
- Try refreshing the Package Manager
- Check that you're looking at "My Registries"

**DiscordRPC.dll missing**
- Use the menu: Kodex Tools > Discord Rich Presence > Download DiscordRPC.dll
- Or download manually from GitHub releases

**"No valid Client ID set" error**
- Open the settings window: Kodex Tools > Discord Rich Presence
- Enter your Discord Application ID
- Click Save Settings

**Not showing in Discord**
- Make sure Discord is running
- Check that your Application ID is correct
- Verify Rich Presence is enabled in your Discord application
- Make sure you added the DiscordRichPresence component to a GameObject

**Compilation errors**
- Make sure DiscordRPC.dll is in the Runtime folder
- Try restarting Unity
- Check that the asmdef files are present
