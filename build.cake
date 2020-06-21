#addin "nuget:?package=Cake.Coveralls&version=0.10.1"
#addin "nuget:?package=Cake.Coverlet&version=2.4.2"
#addin "nuget:?package=Cake.Git&version=0.21.0"
#addin "nuget:?package=Cake.ReSharperReports&version=0.11.1"
#addin "nuget:?package=Cake.Sonar&version=1.1.25"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var configuration = Argument("configuration", "Debug");
var revision = EnvironmentVariable("BUILD_NUMBER") ?? Argument("revision", "9999");
var target = Argument("target", "Default");
var buildWithDupFinder = EnvironmentVariable("BUILD_WITH_DUPFINDER") ?? "ON";
var buildWithInspectCode = EnvironmentVariable("BUILD_WITH_INSPECTCODE") ?? "ON";
var buildWithUnitTesting = EnvironmentVariable("BUILD_WITH_UNITTESTING") ?? "ON";


//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define git commit id
var commitId = "SNAPSHOT";

// Define product name and version
var product = "Htc.Vita.Mod.Desktop";
var companyName = "HTC";
var version = "0.10.2";
var semanticVersion = string.Format("{0}.{1}", version, revision);
var ciVersion = string.Format("{0}.{1}", version, "0");

// Define copyright
var copyright = string.Format("Copyright © 2017 - {0}", DateTime.Now.Year);

// Define timestamp for signing
var lastSignTimestamp = DateTime.Now;
var signIntervalInMilli = 1000 * 5;

// Define path
var solutionFile = File(string.Format("./source/{0}.sln", product));

// Define directories.
var distDir = Directory("./dist");
var tempDir = Directory("./temp");
var generatedDir = Directory("./source/generated");
var packagesDir = Directory("./source/packages");
var nugetDir = distDir + Directory(configuration) + Directory("nuget");
var homeDir = Directory(EnvironmentVariable("USERPROFILE") ?? EnvironmentVariable("HOME"));
var testDataDir = homeDir + Directory(".htc_test");
var reportDotCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/dotCover/AnyCPU");
var reportDotCoverDirX86 = distDir + Directory(configuration) + Directory("report/dotCover/x86");
var reportOpenCoverDirAnyCPU = distDir + Directory(configuration) + Directory("report/OpenCover/AnyCPU");
var reportOpenCoverDirX86 = distDir + Directory(configuration) + Directory("report/OpenCover/x86");
var reportXUnitDirAnyCPU = distDir + Directory(configuration) + Directory("report/xUnit/AnyCPU");
var reportXUnitDirX86 = distDir + Directory(configuration) + Directory("report/xUnit/x86");
var reportReSharperDupFinder = distDir + Directory(configuration) + Directory("report/ReSharper/DupFinder");
var reportReSharperInspectCode = distDir + Directory(configuration) + Directory("report/ReSharper/InspectCode");

// Define signing key, password and timestamp server
var signKeyEnc = EnvironmentVariable("SIGNKEYENC") ?? "NOTSET";
var signPass = EnvironmentVariable("SIGNPASS") ?? "NOTSET";
var signSha1Uri = new Uri("http://timestamp.digicert.com");
var signSha256Uri = new Uri("http://timestamp.digicert.com");

// Define coveralls update key
var coverallsApiKey = EnvironmentVariable("COVERALLS_APIKEY") ?? "NOTSET";

// Define nuget push source and key
var nugetApiKey = EnvironmentVariable("NUGET_PUSH_TOKEN") ?? EnvironmentVariable("NUGET_APIKEY") ?? "NOTSET";
var nugetSource = EnvironmentVariable("NUGET_PUSH_PATH") ?? EnvironmentVariable("NUGET_SOURCE") ?? "NOTSET";

// Define sonarcloud key
var sonarcloudApiKey = EnvironmentVariable("SONARCLOUD_APIKEY") ?? "NOTSET";


//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Fetch-Git-Commit-ID")
    .ContinueOnError()
    .Does(() =>
{
    var lastCommit = GitLogTip(MakeAbsolute(Directory(".")));
    commitId = lastCommit.Sha;
});

Task("Display-Config")
    .IsDependentOn("Fetch-Git-Commit-ID")
    .Does(() =>
{
    Information("Build target: {0}", target);
    Information("Build configuration: {0}", configuration);
    Information("Build commitId: {0}", commitId);
    if ("Release".Equals(configuration))
    {
        Information("Build version: {0}", semanticVersion);
    }
    else
    {
        Information("Build version: {0}-CI{1}", ciVersion, revision);
    }
});

Task("Clean-Workspace")
    .IsDependentOn("Display-Config")
    .Does(() =>
{
    CleanDirectory(distDir);
    CleanDirectory(tempDir);
    CleanDirectory(generatedDir);
    CleanDirectory(packagesDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean-Workspace")
    .Does(() =>
{
    NuGetRestore(string.Format("./source/{0}.sln", product));
});

Task("Generate-AssemblyInfo")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    CreateDirectory(generatedDir);
    var file = "./source/generated/SharedAssemblyInfo.cs";
    var assemblyVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        assemblyVersion = ciVersion;
    }
    CreateAssemblyInfo(
            file,
            new AssemblyInfoSettings
            {
                    Company = companyName,
                    Copyright = copyright,
                    FileVersion = assemblyVersion,
                    InformationalVersion = assemblyVersion,
                    Product = string.Format("{0} : {1}", product, commitId),
                    Version = version
            }
    );
});

Task("Run-Sonar-Begin")
    .WithCriteria(() => !"NOTSET".Equals(sonarcloudApiKey))
    .IsDependentOn("Generate-AssemblyInfo")
    .Does(() =>
{
    SonarBegin(
            new SonarBeginSettings {
                    Key = "ViveportSoftware_vita_mod_desktop_csharp",
                    Login = sonarcloudApiKey,
                    OpenCoverReportsPath = "**/*.OpenCover.xml",
                    Organization = "viveportsoftware",
                    Url = "https://sonarcloud.io"
            }
    );
});

Task("Build-Assemblies")
    .IsDependentOn("Run-Sonar-Begin")
    .Does(() =>
{
    var settings = new DotNetCoreBuildSettings
    {
            Configuration = configuration
    };
    DotNetCoreBuild("./source/", settings);
});

Task("Prepare-Unit-Test-Data")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Build-Assemblies")
    .Does(() =>
{
    if (!DirectoryExists(testDataDir))
    {
        CreateDirectory(testDataDir);
    }
    if (!FileExists(testDataDir + File("TestData.Md5.txt")))
    {
        CopyFileToDirectory("source/" + product + ".Tests/TestData.Md5.txt", testDataDir);
    }
    if (!FileExists(testDataDir + File("TestData.Sha1.txt")))
    {
        CopyFileToDirectory("source/" + product + ".Tests/TestData.Sha1.txt", testDataDir);
    }
});

Task("Run-Unit-Tests-Under-AnyCPU-1")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Prepare-Unit-Test-Data")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirAnyCPU);
    var testFilePattern = "./temp/" + configuration + "/" + product + ".Tests/bin/AnyCPU/net452/*.Tests.dll";
    var xUnit2Settings = new XUnit2Settings
    {
            HtmlReport = true,
            NUnitReport = true,
            OutputDirectory = reportXUnitDirAnyCPU,
            Parallelism = ParallelismOption.None
    };

    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                testFilePattern,
                                xUnit2Settings
                        );
                },
                new FilePath(reportDotCoverDirAnyCPU.ToString() + "/" + product + ".html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }.WithFilter("+:*")
                .WithFilter("-:BouncyCastle.*")
                .WithFilter("-:xunit.*")
                .WithFilter("-:*.NunitTest")
                .WithFilter("-:*.Tests")
                .WithFilter("-:*.XunitTest")
        );
    }
    else
    {
        XUnit2(
                testFilePattern,
                xUnit2Settings
        );
    }
});

Task("Run-Unit-Tests-Under-AnyCPU-2")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Run-Unit-Tests-Under-AnyCPU-1")
    .Does(() =>
{
    CreateDirectory(reportOpenCoverDirAnyCPU);
    DotNetCoreTest(
            "./source/" + product + ".Tests/" + product + ".Tests.AnyCPU.csproj",
            new DotNetCoreTestSettings
            {
            },
            new CoverletSettings
            {
                    CollectCoverage = true,
                    CoverletOutputDirectory = reportOpenCoverDirAnyCPU,
                    CoverletOutputFormat = CoverletOutputFormat.opencover,
                    CoverletOutputName = product + ".OpenCover.xml"
            }
    );
});

Task("Run-Unit-Tests-Under-X86")
    .WithCriteria(() => "ON".Equals(buildWithUnitTesting))
    .IsDependentOn("Run-Unit-Tests-Under-AnyCPU-2")
    .Does(() =>
{
    CreateDirectory(reportXUnitDirX86);
    var testFilePattern = "./temp/" + configuration + "/" + product + ".Tests/bin/x86/net452/*.Tests.dll";
    var xUnit2Settings = new XUnit2Settings
    {
            HtmlReport = true,
            NUnitReport = true,
            OutputDirectory = reportXUnitDirX86,
            Parallelism = ParallelismOption.None,
            UseX86 = true
    };

    if(IsRunningOnWindows())
    {
        DotCoverAnalyse(
                tool =>
                {
                        tool.XUnit2(
                                testFilePattern,
                                xUnit2Settings
                        );
                },
                new FilePath(reportDotCoverDirX86.ToString() + "/" + product + ".html"),
                new DotCoverAnalyseSettings
                {
                        ReportType = DotCoverReportType.HTML
                }.WithFilter("+:*")
                .WithFilter("-:BouncyCastle.*")
                .WithFilter("-:xunit.*")
                .WithFilter("-:*.NunitTest")
                .WithFilter("-:*.Tests")
                .WithFilter("-:*.XunitTest")
        );
    }
    else
    {
        XUnit2(
                testFilePattern,
                xUnit2Settings
        );
    }
});

Task("Run-Sonar-End")
    .WithCriteria(() => !"NOTSET".Equals(sonarcloudApiKey))
    .IsDependentOn("Run-Unit-Tests-Under-X86")
    .Does(() =>
{
    SonarEnd(
            new SonarEndSettings
            {
                    Login = sonarcloudApiKey
            }
    );
});

Task("Run-DupFinder")
    .WithCriteria(() => "ON".Equals(buildWithDupFinder))
    .IsDependentOn("Run-Sonar-End")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        DupFinder(
                string.Format("./source/{0}.sln", product),
                new DupFinderSettings
                {
                        OutputFile = new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".xml"),
                        ShowStats = true,
                        ShowText = true,
                        SkipOutputAnalysis = true,
                        ThrowExceptionOnFindingDuplicates = false
                }
        );
        ReSharperReports(
                new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".xml"),
                new FilePath(reportReSharperDupFinder.ToString() + "/" + product + ".html")
        );
    }
});

Task("Run-InspectCode")
    .WithCriteria(() => "ON".Equals(buildWithInspectCode))
    .IsDependentOn("Run-DupFinder")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        InspectCode(
                string.Format("./source/{0}.sln", product),
                new InspectCodeSettings
                {
                        OutputFile = new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".xml"),
                        SkipOutputAnalysis = true,
                        SolutionWideAnalysis = true,
                        ThrowExceptionOnFindingViolations = false,
                        Verbosity = InspectCodeVerbosity.Off
                }
        );
        ReSharperReports(
                new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".xml"),
                new FilePath(reportReSharperInspectCode.ToString() + "/" + product + ".html")
        );
    }
});

Task("Sign-Assemblies")
    .WithCriteria(() => "Release".Equals(configuration) && !"NOTSET".Equals(signPass) && !"NOTSET".Equals(signKeyEnc))
    .IsDependentOn("Run-InspectCode")
    .Does(() =>
{
    var currentSignTimestamp = DateTime.Now;
    Information("Last timestamp:    " + lastSignTimestamp);
    Information("Current timestamp: " + currentSignTimestamp);
    var totalTimeInMilli = (DateTime.Now - lastSignTimestamp).TotalMilliseconds;

    var signKey = "./temp/key.pfx";
    System.IO.File.WriteAllBytes(signKey, Convert.FromBase64String(signKeyEnc));
    var signToolSignSettingsSha1 = new SignToolSignSettings
    {
            CertPath = signKey,
            Password = signPass,
            TimeStampUri = signSha1Uri
    };
    var signToolSignSettingsSha256 = new SignToolSignSettings
    {
            AppendSignature = true,
            CertPath = signKey,
            DigestAlgorithm = SignToolDigestAlgorithm.Sha256,
            Password = signPass,
            TimeStampDigestAlgorithm = SignToolDigestAlgorithm.Sha256,
            TimeStampUri = signSha256Uri
    };

    var file = string.Format("./temp/{0}/{1}/bin/net45/{1}.dll", configuration, product);

    if (totalTimeInMilli < signIntervalInMilli)
    {
        System.Threading.Thread.Sleep(signIntervalInMilli - (int)totalTimeInMilli);
    }
    Sign(
            file,
            signToolSignSettingsSha1
    );
    lastSignTimestamp = DateTime.Now;

    System.Threading.Thread.Sleep(signIntervalInMilli);
    Sign(
            file,
            signToolSignSettingsSha256
    );
    lastSignTimestamp = DateTime.Now;

    file = string.Format("./temp/{0}/{1}/bin/netstandard2.0/{1}.dll", configuration, product);

    if (totalTimeInMilli < signIntervalInMilli)
    {
        System.Threading.Thread.Sleep(signIntervalInMilli - (int)totalTimeInMilli);
    }
    Sign(
            file,
            signToolSignSettingsSha1
    );
    lastSignTimestamp = DateTime.Now;

    System.Threading.Thread.Sleep(signIntervalInMilli);
    Sign(
            file,
            signToolSignSettingsSha256
    );
    lastSignTimestamp = DateTime.Now;
});

Task("Build-NuGet-Package")
    .IsDependentOn("Sign-Assemblies")
    .Does(() =>
{
    CreateDirectory(nugetDir);
    var nugetPackVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        nugetPackVersion = string.Format("{0}-CI{1}", ciVersion, revision);
    }
    Information("Pack version: {0}", nugetPackVersion);
    var settings = new DotNetCorePackSettings
    {
            ArgumentCustomization = (args) =>
            {
                    return args.Append("/p:Version={0}", nugetPackVersion);
            },
            Configuration = configuration,
            NoBuild = true,
            OutputDirectory = nugetDir
    };

    DotNetCorePack("./source/" + product + "/", settings);
});

Task("Update-Coverage-Report")
    .WithCriteria(() => !"NOTSET".Equals(coverallsApiKey))
    .IsDependentOn("Build-NuGet-Package")
    .Does(() =>
{
    CoverallsIo(
            reportOpenCoverDirAnyCPU.ToString() + "/" + product + ".OpenCover.xml",
            new CoverallsIoSettings
            {
                    RepoToken = coverallsApiKey
            }
    );
});

Task("Publish-NuGet-Package")
    .WithCriteria(() => "Release".Equals(configuration) && !"NOTSET".Equals(nugetApiKey) && !"NOTSET".Equals(nugetSource))
    .IsDependentOn("Update-Coverage-Report")
    .Does(() =>
{
    var nugetPushVersion = semanticVersion;
    if (!"Release".Equals(configuration))
    {
        nugetPushVersion = string.Format("{0}-CI{1}", ciVersion, revision);
    }
    Information("Publish version: {0}", nugetPushVersion);
    var package = string.Format("./dist/{0}/nuget/{1}.{2}.nupkg", configuration, product, nugetPushVersion);
    NuGetPush(
            package,
            new NuGetPushSettings
            {
                    ApiKey = nugetApiKey,
                    Source = nugetSource
            }
    );
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Update-Coverage-Report");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
