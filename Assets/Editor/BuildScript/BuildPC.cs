using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Utility;

public class BuildPC : BaseBuild
{
    private const string kPlatform = "/PC";        
    private const string kBuildName = "/AStar.exe";

    [MenuItem("Tool/Build/PC/Debug")]
    public static void PCBuildDebug()
    {
        var config = CreateBuildConfig(kPlatform, kDevBuild, kBuildName);
        Build(config);
    }

    [MenuItem("Tool/Build/PC/Release")]
    public static void PCBuildRelease()
    {
        var config = CreateBuildConfig(kPlatform, kReleaseBuild, kBuildName);
        Build(config);
    }


}