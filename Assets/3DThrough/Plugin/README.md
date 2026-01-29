# 3DThrough - 3DConnexion SpaceMouse Integration for Unity

## Overview
This plugin enables 3DConnexion SpaceMouse devices to control the Unity Scene View camera and selected objects.

## Recent Fixes (2026-01-29)

### COM Exception Error Fixed
The plugin now handles the `COMException (0x80040154): Classe não registrada` error gracefully:

**Changes Made:**
1. **Error Prevention**: Added `deviceInitFailed` flag to prevent repeated initialization attempts
2. **Better Error Messages**: Provides clear guidance on what to check when device fails to initialize
3. **User Feedback**: GUI now shows device connection status with helpful messages
4. **Retry Mechanism**: Added "Retry Connection" button in the editor window
5. **Safe Disconnect**: Improved error handling when disconnecting the device

### Why This Error Occurs
This error happens when:
- 3DConnexion drivers are not installed
- No 3D mouse device is connected
- COM components are not properly registered
- The device is being used by another application

### How to Use
1. **Install 3DConnexion Drivers**: Download and install the latest drivers from [3DConnexion website](https://www.3dconnexion.com/service/drivers.html)
2. **Connect Your Device**: Plug in your SpaceMouse
3. **Open the Plugin**: Go to `Tools > 3D Through` in Unity
4. **Check Status**: The editor window will show if the device is connected or if there are issues
5. **If Connection Fails**: Use the "Retry Connection" button after fixing any issues

### Features
- **Camera Control**: Navigate the Scene View with 6-DOF input
- **Object Control**: Directly manipulate selected GameObjects
- **Button Shortcuts**:
  - Button 2: Toggle between Object/Camera mode
  - Button 3: Reset camera
  - Button 4: Toggle Perspective/Orthographic
  - Button 5: Toggle Brute Axis mode
  - Buttons 7-11: Preset views (top, left, right, front, default)
- **Adjustable Sensitivity**: Control rotation and translation sensitivity
- **Brute Axis Mode**: Expose raw sensor values via `E3dThrough` class

### Troubleshooting
If the plugin shows a warning:
1. Verify 3DConnexion drivers are installed
2. Check that your device is connected and powered
3. Close other applications that might be using the device
4. Restart Unity after installing drivers
5. Use the "Retry Connection" button in the editor window

### Technical Details
- Uses COM interop with `TDx.TDxInput.dll`
- Requires Windows with 3DConnexion drivers
- Editor-only functionality (not available at runtime)

## Support
For driver downloads and device support, visit: https://www.3dconnexion.com/
