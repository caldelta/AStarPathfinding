using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Utility;

public class BuildAndroid : BaseBuild
{
    private const string kPlatform = "/Android";        
    private const string kBuildName = "/AStar.apk";
    private static void SetupKeystoreOfAndroid()
    {
        PlayerSettings.Android.keystoreName = "KeyStore/astar.keystore";
        PlayerSettings.Android.keystorePass = "fucking_r4tard";
        PlayerSettings.Android.keyaliasName = "astar";
        PlayerSettings.Android.keyaliasPass = "fucking_r4tard";
    }

    [MenuItem("Tool/Build/Android/Debug")]
    public static void AndroidBuildDebug()
    {
        var config = CreateBuildConfig(kPlatform, kDevBuild, kBuildName);
        Build(config);
    }

    [MenuItem("Tool/Build/Android/Release")]
    public static void AndroidBuildRelease()
    {
        SetupKeystoreOfAndroid();
        var config = CreateBuildConfig(kPlatform, kReleaseBuild, kBuildName);
        Build(config);
    }


}