#!packages/FAKE/tools/FAKE.exe
#r @"packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"
let testDir  = "./test/"

Target "Clean" (fun _ ->
  CleanDirs [buildDir; testDir]
)

Target "BuildApp1" (fun _ ->
  !! "src/app1/**/*.fsproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuild-Output: "
)

Target "BuildApp2" (fun _ ->
  !! "src/app2/**/*.fsproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuild-Output: "
)

Target "BuildTest" (fun _ ->
  !! "src/test/**/*.fsproj"
    |> MSBuildDebug testDir "Build"
    |> Log "BuildTest-Output: "
)

Target "Test" (fun _ ->
  !! (testDir + "/*.dll")
    |> NUnit (fun p ->
        {p with
           DisableShadowCopy = true;
           OutputFile = testDir + "TestResults.xml" })
)

Target "Default" (fun _ ->
  trace "Default Target."
)

"Clean"
  ==> "BuildApp1"
  ==> "BuildApp2"
  ==> "BuildTest"
  ==> "Test"
  ==> "Default"

RunTargetOrDefault "Default"