https://learn.microsoft.com/en-us/dotnet/core/sdk/file-based-apps

Build applications
------------------
Compile your file-based app by using the dotnet build command: dotnet build file.cs

The SDK generates a virtual project and builds your application. 
By default, the build output goes to the system's temporary directory under <temp>/dotnet/runfile/<appname>-<appfilesha>/bin/<configuration>/.
Use the --output option with the dotnet build command to specify a different path. 
To define a new default output path, set the OutputPath property at the top of your file by using the directive: #:property OutputPath=./output.

Clean build outputs
-------------------
Remove build artifacts by using the dotnet clean command: dotnet clean file.cs

Delete cache for file-based apps in a directory: dotnet clean file-based-apps

Use the --days option with the preceding command to specify how many days an artifact folder needs to be unused before removal. The default number of days is 30.

Publish applications
--------------------
File-based apps enable native AOT publishing by default, producing optimized, self-contained executables. 
Disable this feature by adding #:property PublishAot=false at the top of your file.

Use the dotnet publish command to create an independent executable: dotnet publish file.cs

The default location of the executable is an artifacts directory next to the .cs file, with a subdirectory named after the application. 
Use the --output option with the dotnet publish command to specify a different path.

Package as tool
---------------
Package your file-based app as a .NET tool by using the dotnet pack command: dotnet pack file.cs

File-based apps set PackAsTool=true by default. Disable this setting by adding #:property PackAsTool=false at the top of your file.

Convert to project
------------------
Convert your file-based app to a traditional project by using the dotnet project convert command: dotnet project convert file.cs

This command makes a copy of the .cs file and creates a .csproj file with equivalent SDK items, properties, and package references based on the original file's #: directives. 
Both files are placed in a directory named for the application next to the original .cs file, which is left untouched.

Restore dependencies
--------------------
Restore NuGet packages referenced in your file by using the dotnet restore command: dotnet restore file.cs

By default, restore runs implicitly when you build or run your application. 
However, you can pass --no-restore to both the dotnet build and dotnet run commands to build or run without implicitly restoring.