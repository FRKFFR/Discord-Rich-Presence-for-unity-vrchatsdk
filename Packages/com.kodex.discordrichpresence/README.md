# Kodex Discord Rich Presence

Display your Unity project information in Discord Rich Presence.

## Features

- Shows project name in Discord status
- Displays current scene being worked on
- Shows Unity version
- Automatically updates when scene changes
- Customizable status messages and templates
- Configurable update interval
- Easy-to-use Editor UI for settings

## Installation

### VPM (Recommended)

1. Add this repository to your VPM settings
2. Install `com.kodex.discordrichpresence` via VPM

### Manual

1. Place the `com.kodex.discordrichpresence` folder in your Unity project's `Packages` folder
2. Download [DiscordRPC.dll](https://github.com/Lachee/discord-rpc-csharp/releases) and place it in `Packages/com.kodex.discordrichpresence/Runtime/`

## Setup

### 1. Create Discord Application

1. Go to [Discord Developer Portal](https://discord.com/developers/applications)
2. Create a new application
3. Go to "Rich Presence" and enable it
4. Copy the Application ID

### 2. Configure in Unity

1. Open Unity
2. Go to **Kodex Tools > Discord Rich Presence**
3. Enter your Discord Application ID
4. Customize templates and image settings
5. Click "Save Settings"

### 3. Add to Scene

1. Create a new GameObject in your scene (e.g., "DiscordRichPresence")
2. Add the `DiscordRichPresence` component
3. The component will automatically load your settings

## Configuration

### Templates

You can use these variables in your templates:
- `{projectName}` - Your Unity project name
- `{sceneName}` - Current active scene name
- `{unityVersion}` - Unity editor version

### Images

To add custom images:
1. Go to your Discord application in the Developer Portal
2. Navigate to "Rich Presence" → "Art Assets"
3. Upload your images (512x512 recommended)
4. Note the image keys to use in the settings

## Usage

The Discord Rich Presence will automatically:
- Connect when the scene loads (if enabled)
- Update when you switch scenes
- Update periodically based on your interval setting
- Disconnect when Unity closes

## VRChat

This package works with VRChat projects. The Discord Rich Presence will show your VRChat avatar project information when working on it.

## Troubleshooting

**"No valid Client ID set"**
- Make sure you've set your Discord Application ID in the settings window
- Get an ID from https://discord.com/developers/applications

**Not showing in Discord**
- Make sure Discord is running
- Check that your Application ID is correct
- Verify Rich Presence is enabled in your Discord application

**Images not showing**
- Make sure you've uploaded images to your Discord application
- Check that the image keys match exactly (case-sensitive)
- Ensure the large_image_key is set in settings
