using UnityEngine;

public class DebugInfo : MonoBehaviour
{
    // If this component is enabled, attached dynamic text component
    // is modified to list bunch of data from SystemInfo and Application.
    void Start()
    {
        DynamicText dynText = GetComponent<DynamicText>();
        if (!dynText)
            return;

        dynText.size /= 2;
        dynText.SetText(
            "SystemInfo.deviceModel: " + SystemInfo.deviceModel + "\n" +
            "SystemInfo.deviceName: " + SystemInfo.deviceName + "\n" +
            "SystemInfo.deviceType: " + SystemInfo.deviceType + "\n" +
            "SystemInfo.graphicsDeviceID: " + SystemInfo.graphicsDeviceID + "\n" +
            "SystemInfo.graphicsDeviceName: " + SystemInfo.graphicsDeviceName + "\n" +
            "SystemInfo.graphicsDeviceVendor: " + SystemInfo.graphicsDeviceVendor + "\n" +
            "SystemInfo.graphicsDeviceVendorID: " + SystemInfo.graphicsDeviceVendorID + "\n" +
            "SystemInfo.graphicsDeviceVersion: " + SystemInfo.graphicsDeviceVersion + "\n" +
            "SystemInfo.graphicsMemorySize: " + SystemInfo.graphicsMemorySize + "\n" +
            //"SystemInfo.graphicsPixelFillrate: " + SystemInfo.graphicsPixelFillrate + "\n" +
            "SystemInfo.graphicsShaderLevel: " + SystemInfo.graphicsShaderLevel + "\n" +
            "SystemInfo.operatingSystem: " + SystemInfo.operatingSystem + "\n" +
            "SystemInfo.processorCount: " + SystemInfo.processorCount + "\n" +
            "SystemInfo.processorType: " + SystemInfo.processorType + "\n" +
            "SystemInfo.supports...:\n" +
            "RenderTargetCount,3DTextures,Accelerometer,ComputeShaders: " +
            SystemInfo.supportedRenderTargetCount + "," +
            SystemInfo.supports3DTextures + "," +
            SystemInfo.supportsAccelerometer + "," +
            SystemInfo.supportsComputeShaders + "\n" +
            "Gyroscope,ImageEffects,Instancing: " +
            SystemInfo.supportsGyroscope + "," +
            SystemInfo.supportsImageEffects + "," +
            SystemInfo.supportsInstancing + "\n" +
            "npotSupport,Shadows,Vibration: " +
            SystemInfo.npotSupport + "," +
            SystemInfo.supportsShadows + "," +
            SystemInfo.supportsVibration + "\n" +
            "SystemInfo.systemMemorySize: " + SystemInfo.systemMemorySize + "\n" +
            "Application.isEditor: " + Application.isEditor + "\n" +
            "Application.isWebPlayer: " + Application.isWebPlayer + "\n" +
            "Application.platform: " + Application.platform.ToString() + "\n" +
            "Application.unityVersion: " + Application.unityVersion + "\n" +
            "Application.dataPath: " + Application.dataPath + "\n" +
            "Application.persistentDataPath:\n" + Application.persistentDataPath + "\n" +
            "-");
            
    }
}
