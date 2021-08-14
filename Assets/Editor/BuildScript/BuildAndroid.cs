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

    [MenuItem("Tool/Build/Android/Debug")]
    public static void PCBuildDebug()
    {
        var config = CreateBuildConfig(kPlatform, kDevBuild, kBuildName);
        Build(config);
    }

    [MenuItem("Tool/Build/Android/Release")]
    public static void PCBuildRelease()
    {
        var config = CreateBuildConfig(kPlatform, kReleaseBuild, kBuildName);
        Build(config);
    }


}