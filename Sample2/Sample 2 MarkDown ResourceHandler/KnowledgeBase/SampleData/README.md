# KB GitHub Repository

The KnowledgeBase project is stored in a separate GitHub repository at:
- [https://github.com/Dirkster99/KB](https://github.com/Dirkster99/KB "https://github.com/Dirkster99/KB")

This project is aimed at replacing CHM file viewers in an MVVM/WPF compliant way.
It will be based on CefSharp and Lucene.Net but is far from completion.

## References

### CefSharp on GitHub

- [https://github.com/cefsharp/CefSharp](https://github.com/cefsharp/CefSharp "https://github.com/cefsharp/CefSharp")
- [https://github.com/cefsharp/CefSharp.MinimalExample](https://github.com/cefsharp/CefSharp.MinimalExample "https://github.com/cefsharp/CefSharp.MinimalExample")

### Helpful Blog Posts by Chris Kent

- [http://thechriskent.com/tag/ischemehandler/](http://thechriskent.com/tag/ischemehandler/ "http://thechriskent.com/tag/ischemehandler/")
- [http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/](http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/ "http://thechriskent.com/2014/04/21/use-local-files-in-cefsharp/")
- [http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/](http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/ "http://thechriskent.com/2014/08/18/embedded-chromium-in-winforms/")

### Tip

- Add an alternative (preview nuget) package source via
  VS 2013>Tools>Nuget Package Manager>Package Manager Settings

  Nuget Package > Package Manager Sources (click Plus icon on top right)
  Name: CefSharp Preview MyGet.Org
  Source: https://www.myget.org/F/cefsharp/

  Select the:
              'Include Prerelease' drop down option in the
              'Manage Nuget Packages' window when downloading package in Online section