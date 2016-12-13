---
title: Hello World or Hexo Blog & ASP.NET Core Web App
date: 2016-11-13 17:57:25
tags: [Hexo, Static Generator, ASP.NET Core]
categories: .NET
---

Welcome to my blog and this is my first post! 
Nowadays you have a lot of options ([List of Top Open-Source Static Site Generators](https://www.staticgen.com)) and tools to start your own blog, but I have already personal web site and main requirement was to stick to current technology stack and tools as much as possible.
I did research and decided to use [Hexo](https://hexo.io/) as blog engine. Next step was an integration to ASP.NET Core application and this experience I want to share with you.

## Hexo Blog & ASP.NET Core web application

I am going to share entire process step by step from zero to complete solution. Feel free to skip steps which you already did.

### Create a new ASP.NET Core web application

I hope that you are familiar with ASP.NET Core and get SDK installed, otherwise check [dot.net](https://dot.net) resource.
But create a new web application is very easy:

``` bat
mkdir AspCoreAndHexo
cd AspCoreAndHexo
dotnet new -t Web
dotnet restore
dotnet run
```

Once you have done you can navigate to [http://localhost:5000](http://localhost:5000) and check that site is running. If you see following picture you are good.
{% asset_img aspnetcoretemplate.jpg "ASP.NET Core web application" %}

### Setup Hexo and initialize a new blog

Installation and configuraion is even easy then previous step. There are only two prerequisites:

* Git
* NodeJs

I recommend to visit [official guide](https://hexo.io/docs/index.html) in case of issues. Once you have installed and configured them we need only one quick step:

``` bat
npm install -g hexo-cli
```

and we have everything to start blogging.

### Integrate Hexo into ASP.NET Core web application

Create a new directory (e.g. Blog) in your ASP.NET Core application and initialize Hexo in this directory:

``` bat
mkdir Blog
```

Go to newly created directory:

``` bat
cd Blog
```

And initialize Hexo under this directory:

``` bat
hexo init
npm install
```

After this step you will see next structure in your directory:

``` bat
.
├── _config.yml
├── node_modules
├── package.json
├── scaffolds
├── source
|   ├── _drafts
|   └── _posts
└── themes
```

Now you are ready to start blogging with Hexo, but we need to integrate two applications in one.
Open *_config.yml* file ([more info](https://hexo.io/docs/configuration.html)) and find *public_dir* setting, you need point to "*../wwwroot/blog*" (pay attention to path segment separator, it allows to build right path across POSIX and Windows platforms).
Also update *url* setting:

``` yaml
root: /blog/
public_dir: ../wwwroot/blog
```

After that you can generate static content:

``` bat
hexo clean
hexo generate
```

Next step is to configure ASP.NET Core web application.

### ASP.NET Core web application configuration

To configure web application you need some text editor, I prefer [Visual Studio Code](https://code.visualstudio.com).
{% asset_img vscode.jpg "Visual Studio Code" %}

Open *project.json* file and check that there is package *Microsoft.AspNetCore.StaticFiles* (if you do not have you can easily install from [Nuget](https://www.nuget.org/packages/Microsoft.AspNetCore.StaticFiles))
Then open *Startup.cs* file and find *Configure* method, in this section we need to add a few lines of code:

``` csharp
app.UseFileServer(new FileServerOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"wwwroot\blog")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/blog"),
    EnableDirectoryBrowsing = false
});
```

More details about UseFileServer you can find [docs.microsoft.com](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files#usefileserver), shortly UseFileServer combines the functionality of UseStaticFiles, UseDefaultFiles, and UseDirectoryBrowser.

Finally, you can run ASP.NET Core application with new code and go to the [http://localhost:5000/blog](http://localhost:5000/blog/) and you will see that your new blog is here:
{% asset_img hexoandaspnetcore.jpg "Hexo Blog & ASP.NET Core web application" %}

That is all what I want to share in my "Hello World" post. New and more technical posts are upcoming.