
1 Overview

This is a Hello World! Type CefSharp sample that should work with
NuGet CefSharp Version 39.0.0.

The following steps are necessary to get this to work
(Tested with Visual Studio 2013 Express):

1> Load Solution in Visual Studio
2> Enable Nuget by clicking on the Solution root via
   ContextMenu>Enable NuGet Package Restore

3> Rebuild Solution
   
4> Close and Re>Open Visual Studio (not just the solution)
   You will otherwise not see the References to CefSharp and get corresponding errors

5> Clean Solution
   Rebuild and Execute solution (should go without error)

   > The window should load a test page generated via ResourceHandler
   > The 2 buttons can be used to jump between 2 pages.

1.1 ResourceHandler

This sample application shows how to use a ResourceHandler in CefSharp.
A ResourceHandler can associate a custom address:

- http://test/resource/load
- http://test/resource/loadUnicode

with a string that represents content via HTML. Have a look at:

public static void RegisterTestResources(IWebBrowser browser) in AppViewModel.cs
to see the registration of 2 custom address registrations. Browsing these is
realized within the TestUrlCommands implemented in the same class.

2 Using the Code

- The application starts-up in App.xaml.cs

- The MainWindow.xaml is constructed and displayed via start-up method in the App.xaml.cs
  
- The binding statements in the MainWindow.xaml, eg:
   <cefSharp:ChromiumWebBrowser Address="{Binding BrowserAddress}"
                                Title="{Binding BrowserTitle, Mode=OneWay}" />

  bind into the AppViewModel and comunicate with it via binding.  

- The AppViewModel implements all demo commands:
  TestUrlCommand, TestUrl1Command, ...

  to test browsing forth and back between 2 urls.
