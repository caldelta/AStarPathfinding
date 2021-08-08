using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Utility;

public class Build
{
    private const string kFolderName = "Build";
    private const string kReleaseBuild = "Release";
    private const string kDevBuild = "Development";

    [MenuItem("Tool/Build/PC/Release")]
    public static void PCBuildRelease()
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        // Specify a name for your top-level folder.
        string folderName = kFolderName;
        string dst = Path.Combine(folderName, kReleaseBuild);

        if (Directory.Exists(dst))
        {
            DebugLog.Yellow($"Delete old folders: {dst}");
            Directory.Delete(dst, true);
        }

        Directory.CreateDirectory(dst);

        var target = BuildTarget.StandaloneWindows;
        var options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(scenes, dst, target, options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            DebugLog.Yellow("Build Release succeeded: " + summary.totalSize / 1024 / 1024 + " MB" + " in " + summary.totalTime.TotalMinutes.ToString("N0") + " mins");
        }

        if (summary.result == BuildResult.Failed)
        {
            DebugLog.Yellow("Build failed");
        }
    }

    [MenuItem("Tool/Build/PC/Development")]
    public static void PCBuildDevelopment()
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        // Specify a name for your top-level folder.
        string folderName = kFolderName;
        string dst = Path.Combine(folderName, kDevBuild);

        if (Directory.Exists(dst))
        {
            DebugLog.Yellow($"Delete old folders: {dst}");
            System.IO.Directory.Delete(dst, true);
        }

        Directory.CreateDirectory(dst);

        var target = BuildTarget.StandaloneWindows;
        var options = BuildOptions.Development | BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(scenes, dst, target, options);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            DebugLog.Yellow("Build Development succeeded: " + summary.totalSize / 1024 / 1024 + " MB" + " in " + summary.totalTime.TotalMinutes.ToString("N0") + " mins");
        }

        if (summary.result == BuildResult.Failed)
        {
            DebugLog.Yellow("Build failed");
        }
    }
}