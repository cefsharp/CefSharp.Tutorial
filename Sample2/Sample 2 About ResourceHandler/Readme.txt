
1 Overview

This is a Hello World! Type CefSharp sample that should work with
NuGet CefSharp Version 37.0.0.

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

- The constructor in App.xaml.cs contains an Cef.Initialize(); statement

- The MainWindow.xaml is constructed and displayed via the StartupUri="MainWindow.xaml"
  statement in the App.xaml
  
- The constructor of the MainWindow.xaml contains a constructor to create the
  AppViewModel and attach it to the DataContext of the MainWindow
  
- The binding statements in the MainWindow.xaml, eg:
   <cefSharp:ChromiumWebBrowser Address="{Binding BrowserAddress}"
                                Title="{Binding BrowserTitle, Mode=OneWay}" />

  bind into the AppViewModel and comunicate with it via binding.  

- The AppViewModel implements 2 commands:
  TestUrlCommand, TestUrl1Command

  to test browsing forth and back between 2 urls.

2.1 Code behind in MainWindow.xaml.cs

The constructor of the MainWindow.cs class calls the
AppViewModel.RegisterTestResources(this.browser);
method to register the custom URLs for the ResourceHandler.

The AppViewModel instance is attached to the MainWindow's DataContext (for binding)
and the events: StatusMessage and NavStateChanged are registered with their corresponding
methods.

3 Known Issue

Each Url can be used at most once.
The Output (TW) in VS Studio shows an exception in mscorelib

> Switch catch exception on in debugger and the exception reads something like:
  DataStream was already closed...???

  This is a known issue which is already fixed, so you either need to get the current
  sources and do a local build instead of getting the Nuget version or you will have to
  update the Nuget version (once the fix was realeased to Nuget).
  https://github.com/cefsharp/CefSharp/commit/54b1520761da125b29322670504e98a2eb56c855
