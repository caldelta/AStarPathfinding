using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Utility;

public class BaseBuild
{
    private const string kFolderName = "Build";
    public const string kReleaseBuild = "/Release";
    public const string kDevBuild = "/Debug";
    private static string[] m_scenes;

    public static string CreateDir(string platform, string buildType, string buildName)
    {
        m_scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        // Specify a name for your top-level folder.
        string folderName = kFolderName;
        folderName += platform + buildType;

        if (Directory.Exists(folderName))
        {
            DebugLog.Yellow($"Delete old folders: {folderName}");
            Directory.Delete(folderName, true);
        }

        Directory.CreateDirectory(folderName);
        return folderName + buildName;
    }

    public static BuildPlayerOptions CreateBuildConfig(string platform, string buildType, string buildName)
    {
        BuildPlayerOptions buildOption = new BuildPlayerOptions();
        buildOption.scenes = m_scenes;
        buildOption.locationPathName = CreateDir(platform, buildType, buildName);
        buildOption.target = ToBuildTarget(platform);
        buildOption.options = ToBuildOptions(buildType);
        return buildOption;
    }

    public static void Build(BuildPlayerOptions config)
    {
        BuildReport report = BuildPipeline.BuildPlayer(config);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            DebugLog.Yellow("Build " + ToBuildType(config.options) + " succeeded: " + config.locationPathName + " " + summary.totalSize / 1024 / 1024 + " MB" + " in " + summary.totalTime.TotalMinutes.ToString("N1") + " mins");
        }

        if (summary.result == BuildResult.Failed)
        {
            DebugLog.Yellow("Build failed");
        }
    }
    private static BuildTarget ToBuildTarget(string platform) => platform switch
    {
        "/PC" => BuildTarget.StandaloneWindows64,
        "/Android" => BuildTarget.Android,
        _ => throw new System.NotImplementedException(),
    };

    private static BuildOptions ToBuildOptions(string buildType) => buildType switch
    {
        kReleaseBuild => BuildOptions.None,
        kDevBuild => BuildOptions.None | BuildOptions.Development,
        _ => throw new System.NotImplementedException(),
    };

    private static string ToBuildType(BuildOptions buildOptions) => buildOptions switch
    {
        BuildOptions.None => kReleaseBuild,
        BuildOptions.None | BuildOptions.Development => kDevBuild ,
        _ => throw new System.NotImplementedException(),
    };
}