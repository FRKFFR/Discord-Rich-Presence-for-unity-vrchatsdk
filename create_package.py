#!/usr/bin/env python3
"""
Script to create a VPM package zip from the Unity package folder
"""

import os
import zipfile
from pathlib import Path

def create_vpm_package():
    package_folder = Path("Packages/com.kodex.discordrichpresence")
    output_zip = Path("com.kodex.discordrichpresence-1.0.0.zip")
    
    if not package_folder.exists():
        print(f"Error: Package folder not found: {package_folder}")
        return
    
    print(f"Creating VPM package from {package_folder}...")
    
    with zipfile.ZipFile(output_zip, 'w', zipfile.ZIP_DEFLATED) as zipf:
        for root, dirs, files in os.walk(package_folder):
            for file in files:
                file_path = Path(root) / file
                arcname = file_path.relative_to(package_folder)
                zipf.write(file_path, arcname)
                print(f"Added: {arcname}")
    
    print(f"\nPackage created: {output_zip}")
    print(f"Size: {output_zip.stat().st_size / 1024:.2f} KB")
    
    print("\nNext steps:")
    print("1. Upload the zip file to your server/hosting")
    print("2. Update the URL in index.json to point to your hosted file")
    print("3. Host index.json on your server")
    print("4. Add your repository URL to VCC/VPM settings")

if __name__ == "__main__":
    create_vpm_package()
