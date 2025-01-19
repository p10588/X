using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.Rendering;

public class GraphicsAPIConfigurator : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Pre-Build Graphics API Configuration...");

        if (report.summary.platform == BuildTarget.Android)
        {
            ConfigureGraphicsAPIForAndroid();
        }
        else
        {
            Debug.LogWarning("Graphics API configuration is only applicable for Android platform.");
        }
    }

    private void ConfigureGraphicsAPIForAndroid()
    {
#if USE_VIVE_WAVE_XR_5_3_1
        Debug.Log("Detected Wave SDK Environment. Setting Graphics API to OpenGLES3...");
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.OpenGLES3 });
#else
        Debug.Log("Detected Oculus SDK Environment. Setting Graphics API to Vulkan...");
        PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, new[] { GraphicsDeviceType.Vulkan });
        Debug.LogWarning("No specific VR SDK detected. Using default Graphics API configuration.");
#endif

        var graphicsAPIs = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
        Debug.Log("Updated Graphics APIs:");
        foreach (var api in graphicsAPIs)
        {
            Debug.Log($"- {api}");
        }
    }
}
