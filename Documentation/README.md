# Documentation Index

Welcome to the 3D Through plugin documentation!

## Quick Links

### For Users

- **[Getting Started](../Assets/3DThrough/GettingStarted.txt)** - Quick start guide for first-time users
- **[User Manual (PDF)](3DThrough_Manual.pdf)** - Complete documentation with setup guide and examples
- **[Package README](../Assets/3DThrough/README.md)** - Overview of the plugin

### For Developers

- **[Script Reference](3DThrough_Manual.md#9-script-reference)** - Complete API documentation
- **[Code Examples](3DThrough_Manual.md#93-example-scripts)** - 4 working examples
- **[GitHub Repository](https://github.com/masterrey/3DThrough)** - Source code and issues

### For Asset Store Submission

- **[Submission Checklist](AssetStore_Submission_Checklist.md)** - Validation requirements
- **[Preparation Summary](PREPARATION_SUMMARY.md)** - What was changed
- **[Submission Guide](SUBMISSION_GUIDE.md)** - How to submit

### Project Information

- **[CHANGELOG](../CHANGELOG.md)** - Version history and features
- **[LICENSE](../LICENSE)** - Apache License 2.0
- **[Main README](../README.md)** - Project overview and contribution guide

## Documentation Structure

```
Documentation/
├── README.md                              ← You are here
├── 3DThrough_Manual.md                    Complete manual (Markdown)
├── 3DThrough_Manual.pdf                   Complete manual (PDF - 54 KB)
├── AssetStore_Submission_Checklist.md     Validation requirements met
├── PREPARATION_SUMMARY.md                 Changes made for Asset Store
├── SUBMISSION_GUIDE.md                    How to submit to Asset Store
└── [Source files: PSD images]

Assets/3DThrough/
├── 3DThrough_Manual.pdf                   User manual (included in package)
├── GettingStarted.txt                     Quick reference
└── README.md                              Package overview
```

## Quick Reference

### Installation
1. Install 3DConnexion drivers
2. Import plugin to Unity project
3. Open `Tools > 3D Through`

### System Requirements
- Unity 2019.4 or later
- Windows OS
- 3DConnexion device
- 3DConnexion drivers installed

### Using Raw Data API
```csharp
using Mouse3D;

void Update() {
    Vector3 movement = E3dThrough.BruteTranslation;
    Vector3 rotation = E3dThrough.BruteRotation;
}
```

### Button Controls
- Button 2: Toggle Object/Camera mode
- Button 4: Toggle Perspective/Ortho
- Button 5: Toggle Brute Axis mode
- Buttons 7-10: View presets

### Support
- **Email**: masterrey@gmail.com
- **GitHub Issues**: https://github.com/masterrey/3DThrough/issues
- **Repository**: https://github.com/masterrey/3DThrough

## For Contributors

This project is now open source! Contributions are welcome:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

See [CONTRIBUTING guidelines](../README.md#contributing) in the main README.

## Version Information

- **Current Version**: 1.0.0
- **License**: Apache License 2.0
- **Unity Version**: 2019.4+
- **Platform**: Windows

## Recent Updates

See [CHANGELOG.md](../CHANGELOG.md) for complete version history.

---

**Need help?** Check the [User Manual](3DThrough_Manual.pdf) or contact masterrey@gmail.com
