using System.IO;
using UnityEditor;
using UnityEngine;

public class ScormTemplateInstaller
{
    private const string sourceTemplatePath = "Packages/com.szczypiorofix.scormintegration/Templates/WebGLTemplates/ScormTemplate";
    
    private const string destParentPath = "Assets/WebGLTemplates";
    
    private const string finalDestPath = "Assets/WebGLTemplates/ScormTemplate";

    [MenuItem("SCORM Tools/Install 'ScormTemplate' template")]
    public static void InstallTemplate()
    {
        if (!Directory.Exists(sourceTemplatePath))
        {
            EditorUtility.DisplayDialog("Error", "Cannot find the source template folder in the package. Please ensure the package is installed and the path in the script is correct.", "OK");
            return;
        }

        if (Directory.Exists(finalDestPath))
        {
            if (!EditorUtility.DisplayDialog("Warning", "The template 'ScormTemplate' already exists in your project. Do you want to replace it with the latest version from the package?", "Yes, replace", "No, cancel"))
            {
                return;
            }
            Directory.Delete(finalDestPath, true);
        }

        Directory.CreateDirectory(destParentPath);

        FileUtil.CopyFileOrDirectory(sourceTemplatePath, finalDestPath);
        
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("Success", "The template 'ScormTemplate' has been successfully installed! You can now select it in Player Settings.", "OK");

        var templateObject = AssetDatabase.LoadAssetAtPath<Object>(finalDestPath);
        EditorGUIUtility.PingObject(templateObject);
    }
}
