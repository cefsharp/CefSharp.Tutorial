1 Overview

This is a Hello World! Type CefSharp sample that should work with
NuGet CefSharp Version 37.0.0 or later.

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

   > Wait a while until the page loads
   
2 Using the Code

This sample application realy is a bare bone minimal WPF CefSharp application.
It consists of a:

2.1 App.xaml file that configures the MainWindow.xaml to be loaded on start-up.

2.2 MainWindow.xaml file that create a Chrome browser instance with the following XAML:
    <cefSharp:ChromiumWebBrowser Grid.Row="0" Address="..." />

See CodeProject article for all documentation details:
http://www.codeproject.com/Articles/881315/Display-HTML-in-WPF-and-CefSharp-Tutorial-Step-of
