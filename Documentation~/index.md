# Scorm API Integration for Unity

The Scorm API Integration for Unity package is designed to send signals from applications to LMS systems in the SCORM 1.2 standard.

## Usage Examples

The main class for sending SCORM signals is the *`ScormManager`* class, which contains 4 basic public static methods:
- **`ScormManager.Initialize()`** – usually called at the beginning of loading within the application,
- **`ScormManager.SetScore(int score)`** – sets the score, where `score` is a value in the range 0 - 100,
- **`ScormManager.MarkAsComplete(bool passed)`** – where `passed` is a `true` or `false` value indicating whether the training was completed or not,
- **`ScormManager.Finish()`** – method called at the end of the training or when closing the session,

A sample scene can be imported from the Unity Package Manager by selecting the package and clicking the *`Samples`* tab.

## Server Configurations

- For Apache servers: simply add the **.htaccess** file to the main folder with the package (the file is located in `./Samples~/ServerConfig`)
- For Nginx servers, instructions for adding support for .wasm files are in [nginx_instructions.md](/Documentation~/nginx_instructions.md)
- For Microsoft IIS-based servers, add the **web.config** file located in `./Samples~/ServerConfig`