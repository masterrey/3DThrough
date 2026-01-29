# Changelog

All notable changes to the 3D Through plugin will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024

### Added
- Initial release of 3D Through Unity plugin
- **Open source release on GitHub:** https://github.com/masterrey/3DThrough
- 3DConnexion SpaceMouse integration for Unity Editor
- Camera control mode for Scene View navigation
- Object manipulation mode for direct GameObject control
- Brute Axis mode for custom script implementations
- Adjustable rotation and translation sensitivity settings
- Automatic device reconnection system
- Comprehensive button shortcuts (Buttons 2-11)
- Support for Perspective/Orthographic view switching
- Multiple preset viewing angles (top, left, right, front)
- EditorPrefs integration for persistent settings
- Robust error handling and COM exception management
- Status indicators for device connection
- Retry connection button in UI
- Example scripts for using raw sensor data
- Complete XML documentation for all public APIs
- Undo system integration for object modifications

### Technical Details
- Namespace organization under `Mouse3D`
- Assembly definition for modular compilation
- TDxInput COM driver integration
- Static API for accessing raw sensor values
- Editor window with dockable UI
- Real-time sensor data processing
- Frame-by-frame device polling

### Requirements
- Unity Editor 2019.4 or later
- 3DConnexion device (SpaceMouse, Space Navigator, etc.)
- 3DConnexion drivers installed and running
- Windows operating system (COM driver requirement)

### Known Limitations
- Windows only (COM driver dependency)
- Requires 3D Through window to remain open
- Device must be connected before opening Unity
- Some features may vary by device model

## [Unreleased]

### Planned Features
- macOS support investigation
- Additional device button mapping options
- Configurable axis inversion
- Deadzone settings for precise control
- Profile system for different workflows
- Runtime API for gameplay use

---

For update notifications and support, contact: masterrey@gmail.com
