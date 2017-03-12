#addin "Cake.FileHelpers"
#addin "Cake.Coveralls"
#tool "nuget:?package=OpenCover"
#tool "nuget:?package=ReportGenerator"
#tool coveralls.io

var target = Argument<string>("target", "Default");

var githubApiKey = Argument<string>("githubApiKey", "");
var coverallsApiKey = Argument<string>("coverallsApiKey", "");
var nugetApiKey = Argument("nugetApiKey", "");

var configuration =
    HasArgument("Configuration") ? Argument<string>("Configuration") :
    EnvironmentVariable("Configuration") != null ? EnvironmentVariable("Configuration") :
    "Release";
var preReleaseSuffix =
    HasArgument("PreReleaseSuffix") ? Argument<string>("PreReleaseSuffix") :
    (AppVeyor.IsRunningOnAppVeyor && AppVeyor.Environment.Repository.Tag.IsTag) ? null :
    EnvironmentVariable("PreReleaseSuffix") != null ? EnvironmentVariable("PreReleaseSuffix") :
    "beta";
var buildNumber =
    HasArgument("BuildNumber") ? Argument<int>("BuildNumber") :
    AppVeyor.IsRunningOnAppVeyor ? AppVeyor.Environment.Build.Number :
    TravisCI.IsRunningOnTravisCI ? TravisCI.Environment.Build.BuildNumber :
    EnvironmentVariable("BuildNumber") != null ? int.Parse(EnvironmentVariable("BuildNumber")) :
    0;

 var restoreSources = new [] {
     "https://www.myget.org/F/xunit/api/v3/index.json",
     "https://dotnet.myget.org/F/dotnet-core/api/v3/index.json",
     "https://dotnet.myget.org/F/cli-deps/api/v3/index.json",
     "https://api.nuget.org/v3/index.json"
};

var githubOwner = "jrgcubano";
var githubRepo = "GildedRose";
var githubRawUri = "http://raw.githubusercontent.com";
var output = Directory("build");
var publish = Directory("publish");
var release = Directory("release");
var outputNuGet = output + Directory("nuget");
var coverageAssemblies = new[] { "GildedRose" };

Task("Clean")
	.Does(() => 
    {        
        CleanDirectories(output);
        CleanDirectories(publish);
        CleanDirectories(release);
        DeleteDirectories(GetDirectories("**/bin"), true);
        DeleteDirectories(GetDirectories("**/obj"), true);
	});


Task("Restore")
    .IsDependentOn("Clean")
    .Does(() => 
    {        
        var settings = new DotNetCoreRestoreSettings {
            Sources = restoreSources
        };
        var srcProjects = GetFiles("./src/**/*.csproj");
        var testProjects = GetFiles("./test/**/*.csproj");
        foreach(var project in srcProjects)
        {
            DotNetCoreRestore(project.GetDirectory().FullPath, settings);
        }    
        foreach(var project in testProjects)
        {
            DotNetCoreRestore(project.GetDirectory().FullPath, settings);
        }    
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    { 
        var srcProjects = GetFiles("./src/**/*.csproj");
        var testProjects = GetFiles("./test/**/*.csproj");
        foreach(var project in srcProjects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {                
                    Configuration = configuration
                });
        }           
        foreach(var project in testProjects)
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {                
                    Configuration = configuration
                });
        }           
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var testProjects = GetFiles("./test/**/*.csproj");
        foreach(var project in testProjects)
        {
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    //ArgumentCustomization = args => args
                    //    .Append("-xml")
                    //    .Append(artifactsDirectory.Path.CombineWithFilePath(project.GetFilenameWithoutExtension()).FullPath + ".xml"),
                    Configuration = configuration,
                    NoBuild = true
                });
        }
    });

Task("Package")
    .IsDependentOn("Test")
    .Does(() => 
    {
        string versionSuffix = null;
        if (!string.IsNullOrEmpty(preReleaseSuffix))
        {
            versionSuffix = preReleaseSuffix + "-" + buildNumber.ToString("D4");
        }
        var srcProjects = GetFiles("./src/**/*.csproj");
        foreach (var project in srcProjects)
        {
            DotNetCorePack(
                project.GetDirectory().FullPath,
                new DotNetCorePackSettings()
                {
                    Configuration = configuration,
                    OutputDirectory = outputNuGet,
                    VersionSuffix = versionSuffix
                });
        }
    });

Task("PushPackage")
    .IsDependentOn("Package")
    .Does(() =>
{
    var packages = GetFiles(outputNuGet.Path.FullPath + "/*.nupkg");
    NuGetPush(packages, new NuGetPushSettings {
        Source = "https://nuget.org/",
        ApiKey = nugetApiKey,        
    });
});
    
Task("Default") 
    .IsDependentOn("Package");

RunTarget(target);