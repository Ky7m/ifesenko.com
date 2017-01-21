---
title: Windows Containers or Dockerize an existing ASP.NET MVC 5 application
tags:
  - Windows Containers
  - Docker
  - ASP.NET MVC
categories:
  - DevOps
date: 2017-01-18 14:39:03
---


The purpose of this blog post is to describe the process of migrating existing ASP.NET MVC 5 (this approach is valid for ASP.NET Web Forms as well) application to Windows Containers, some problems and workarounds.
A few words about application, it is a part of [SoftServe’s Survey & Form Builder accelerator project](https://www.youtube.com/watch?v=B0VSRPCU8BU) (since this is a public available product I can share some information), specifically, the server-side part that sits between front-end layer and data storage (relational database). Technology stack is set of standard components: ASP.NET Web Api 2, Entity Framework, Swagger, RESTful/OData, Application Insights and MSSQL as data storage.
Containers support is very important for us to achieve following:

- Boost continuous delivery to the cloud (IaaS/PaaS) or/and on-premises: it is not very complicated, but for each deployment target you need to specify special steps, the containers are more effective, standardized and repeatable way.
- An isolated place where an application can run without affecting the rest of the system and without the system affecting the application: it is very important to perform different integration tests, experiments and so on (e.g. does system support the PostgreSQL database version X.X.X instead of MSSQL).
- Streamlined application development process. An application that runs the same on your laptop, on colleague’s laptop, on the server, on the cloud. In addition, if you were inside a container, it would look very much like you were inside a freshly installed physical computer or a virtual machine. The application cannot be affected by configuration errors.

List is not completed and you can easily add much more items, but these ones are most essential for us at this moment.

## Windows Containers

Assuming you have [Docker](https://www.docker.com/products/docker#/windows) running on Windows 10 (Professional or Enterprise 64-bit) or Windows Server 2016, and Docker Compose installed I assume that you are familiar with Containers technology, otherwise check [official documentation](https://docs.microsoft.com/en-us/virtualization/windowscontainers/about/). 
Let's take a look to available container OS image for ASP.NET. The image can be found on official [Docker hub page](https://hub.docker.com/r/microsoft/aspnet/). This image contains:

- Windows Server Core as the base OS
- IIS 10 as Web Server
- .NET Framework 4.6.2 (or 3.5)
- .NET Extensibility for IIS

Container image sits on top of [microsoft/iis](https://hub.docker.com/r/microsoft/iis/) image. In other words, we have a following structure:

{% asset_img ASPNETDockerImageStructure.png "ASP.NET Docker Image Structure" %}

Therefore this image is a perfect base for building Web Applications using Web Forms, MVC, Web API and SignalR.

## Add Docker project support

There are several approaches to package an application into a container image.

#### Approach 1: Building image from existing ASP.NET web servers using Image2Docker
 
One of available approaches is to use [Image2Docker tool](https://github.com/docker/communitytools-image2docker-win). You need to provide a virtual machine disk image (VHD, VHDX or WIM). The tool will look at the disk image for known artifacts, extract them to a list and generates based on it a Dockerfile. The Dockerfile uses the [microsoft/windowsservercore image](https://hub.docker.com/r/microsoft/windowsservercore/) as the base and installs all the artifacts the tool found on the VM disk. 
Pay your attention that final container image might contain redundant artifacts or does not include required ones (if you have some special artifacts, tool scans for IIS & ASP.NET apps, MSMQ, DNS, DHCP, Apache, MSSQL Server only). Whatever, it can be a good star for some workloads.

#### Approach 2: Building the application inside a container

This approach helps a lot to deal with different tools, technologies, versions, etc. without polluting continuous delivery infrastructure. In this case, you have to incorporate a base image with the development platform as well as the runtime. It means that image contains the source code of the application and compiles it as one step in building the container image. The big plus is the entire development platform that can build your application on any machine with installed Docker. On the other hand, it requires much more space and increase container size to include all the dev tools needed to build the application. For sure, it is possible to sweep up them after the end of the image build or copy artifacts from the build output to a separate container image (in other words, build Docker image in Docker container). This process is more complex and requires more efforts from the development team to build this pipeline (if you could not find official or created by community images, e.g. [official images for building ASP.NET Core applications](https://hub.docker.com/r/microsoft/aspnetcore-build/). Usually, I prefer a less complex and more effective approach - build the application and copy all assets to target container image. 

#### Approach 3: Building the application outside and then dockerize it
 
From my experience this approach is more trivial, because development teams already know how to build the application and what runtime is needed to run it properly. Perhaps, that process is already automated. 
From the container image we expect only runtime to run application bits. It gives a smaller and more optimized container image.
If you migrating existing ASP.NET MVC or ASP.NET Web Forms applications to Windows Containers I would recommend to use current approach as your start point and better experience.

##  Application build

The first step is to get all the assets that you will need to load into an image in one place. I guess that is simplest part, because you did it many times before. Usually for ASP.NET Web projects teams often use [The Publish Web Site Tool](https://msdn.microsoft.com/en-us/library/377y0s6t.aspx#Anchor_2). Personally, I prefer to automate that with [Cake (C# Make)](http://cakebuild.net/) build system. Once all assets, which are required to run the application, in one place you can go ahead.

In my case, I have "Publish" directory with all the needed bits.

{% asset_img TheAssets.png "The Assets" %}

##  Image build

For this step you need to create a Dockerfile that contains instructions for the base image, any additional components, the application you want to run, and other configuration. The Dockerfile is the input to the docker build command, which creates the image.

``` docker
FROM microsoft/aspnet
ARG source=.
WORKDIR /inetpub/wwwroot
COPY ${source} .
```

I store the Dockerfile on the same level as application project file and on publish copy to assets directory (it simplifies next steps):

{% asset_img TheAssetsWithDockerFile.png "The Assets with Dockerfile" %}

Now you are set to run *[docker build](https://docs.docker.com/engine/reference/commandline/build/)* command to create the image that will run ASP.NET application. To do this open a CMD or PowerShell window, and type the following command in the directory with the assets and Dockerfile:

``` powershell
docker build -t surveyserver-api .
```

This command builds the new image by following the instructions in Dockerfile:

``` powershell
PS C:\...\SurveyServer\Publish> docker build -t surveyserver-api .
Sending build context to Docker daemon 18.92 MB
Step 1/4 : FROM microsoft/aspnet
 ---> 08897a3b116a
Step 2/4 : ARG source=.
 ---> Running in d1f0d8eb0835
 ---> 1696dfc14061
Removing intermediate container d1f0d8eb0835
Step 3/4 : WORKDIR /inetpub/wwwroot
 ---> Running in 358eeb78165e
 ---> 36dfff378cef
Removing intermediate container 358eeb78165e
Step 4/4 : COPY ${source} .
 ---> 645a7dbe97f1
Removing intermediate container 1c0d4c907340
Successfully built 645a7dbe97f1
```

Next step is a *[docker run](https://docs.docker.com/engine/reference/run/)* command:

``` powershell
docker run -d -p 80:80 --name surveyserver-api surveyserver-api
```

Once the container starts you need to find its IP address so that you can connect to running container:

``` powershell
docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" surveyserver-api
```

When you have IP address:

``` powershell
PS C:\...\SurveyServer\Publish> docker run -d -p 80:80 --name surveyserver-api surveyserver-api
ffcfc8bdbe59055dadd71ce0409acd171c50b12de91fae5d2ece4d098ac6ad6f
PS C:\...\SurveyServer\Publish> docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" surveyserver-api
172.28.90.45
```

You can open in browser your application:

{% asset_img DockerizedWebApi.png "ASP.NET Application in Windows container" %}

When you are done, and you want to stop your container, issue a [docker stop](https://docs.docker.com/engine/reference/commandline/stop/) command:

``` powershell
docker stop surveyserver-api
```

To remove the container, run a [docker rm](https://docs.docker.com/engine/reference/commandline/rm/) command:

``` powershell
docker rm surveyserver-api
```

To remove the image - [docker rmi](https://docs.docker.com/engine/reference/commandline/rmi/) command:

``` powershell
docker rmi surveyserver-api
```

Likewise, you have the running in container application, but it is not working application at this moment, because there is a unresolved third-party dependency to MSSQL database.
Next step is to connect application to MSSQL Windows container. 

## Connecting the application to MSSQL server

If you do not have ["Microsoft SQL Server Express for Windows Containers"](https://hub.docker.com/r/microsoft/mssql-server-windows-express/) image on local machine then run next command first:

``` docker
docker pull microsoft/mssql-server-windows-express
```

Image is about 12 GB and takes some time to download and extract on your machine.
As you understand, I am going to use one more container and build connection with the application container image, so it is good time for Docker Compose. [Docker Compose](https://docs.docker.com/compose/) is a great way for defining and running complex multi-container applications. Docker Compose tool has own configuration file - docker-compose.yml.
If you perform these steps on Windows Server 2016, probably, you do not have pre-installed docker-compose engine, but you can simply download and install with next PowerShell command: 

``` powershell
Invoke-WebRequest https://dl.bintray.com/docker-compose/master/docker-compose-Windows-x86_64.exe -UseBasicParsing -OutFile $env:ProgramFiles\docker\docker-compose.exe
```

Definition of docker compose file is also very simple and does not need line-by-line explanation:

``` yaml
version: '2.1'

services:
  surveyserver-db:
    image: microsoft/mssql-server-windows-express
    container_name: surveyserver-db
    environment:
      sa_password: "yWr2LQQNuBKjeHkrpXC"
      ACCEPT_EULA: Y
    ports:
      - "1433:1433"

  surveyserver-api:
    image: surveyserver-api
    container_name: surveyserver-api
    build:
      context: .
      dockerfile: Dockerfile
    environment:
      SurveyDb: "Server=surveyserver-db,1433;Database=SurveyDb;User Id=sa;Password=yWr2LQQNuBKjeHkrpXC;"
    depends_on:
      - "surveyserver-db"
    ports:
      - "80:80"

networks:
  default:
    external:
      name: nat
```

I want to focus on several pieces of configuration that are very important.

#### Connection string to MSSQL database server

The server name for the database in the connection string is *surveyserver-db* that is the name of the service in the Compose file.

``` yaml
environment:
      SurveyDb: "Server=surveyserver-db,1433;Database=SurveyDb;User Id=sa;Password=yWr2LQQNuBKjeHkrpXC;"
```

Docker has built-in Service Discovery mechanism that allows multi-container services to be discovered and referenced to each other by name. Through this mapping, DNS resolution in the Docker abstracts away the added complexity of managing multiple container endpoints. You could replace the database with a new container which had a different IP address, and the application would still work because it resolves the container by name.

> There is known issue with Windows container name DNS resolution. [Issue #27499](https://github.com/docker/docker/issues/27499) gives a problem definition and workaround as a temporary solution.

I faced this issue on Windows 10 and sometimes on different VMs with Windows Server 2016. To get the stable work you need a small Windows tweak in the Dockerfile definition for any images which will be using the DNS service:

``` docker
FROM microsoft/aspnet
ARG source=.
WORKDIR /inetpub/wwwroot
COPY ${source} .

# Workaround for Windows container name DNS resolution issue
RUN powershell -Command Set-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters' -Name ServerPriorityTimeLimit -Value 0 -Type DWord

```

#### Docker Network configuration

Due to a known limitation, you have to use the existing NAT network, which is created by default by Docker on Windows. At present Windows only supports one NAT network.

``` yaml
networks:
  default:
    external:
      name: nat
```

Otherwise, Docker Compose is trying to create a new one, which would fail with error:

``` bat
Creating network "publish_default" with the default driver
ERROR: HNS failed with error : The parameter is incorrect.
```

#### Application configuration settings

How to store configuration settings is always a difficult topic. In the .NET world, you have several ways to store configurations and there is no best solution for all situations. 
In this particular case I recommend to consider two approaches:

- Update settings in web.config file from runtime context during container provisioning - use PowerShell script to update web.config/app.config files ([good example of this technique](http://www.protosystem.net/post/2009/06/01/Using-Powershell-to-manage-application-configuration.aspx))
- Read settings from environment variables directly and fallback to web.config file - perhaps, it requries simple code changes. 

I selected the second variant with direct read from environment variables and it requires additional changes.
Firstly, any environment variables you pass in container won't be visible to applications running in IIS, because Docker creates process-level variables and IIS exposes machine-level variables only. That is why you need override [entrypoint](https://docs.docker.com/engine/reference/builder/#/entrypoint) with custom script, which will promote environment variables from process level to machine level and restore [default entrypoint](https://github.com/Microsoft/iis-docker/blob/master/windowsservercore/Dockerfile#L7).
You can take [this PowerShell script](https://github.com/sixeyed/nerd-dinner/blob/dockerize-part2/docker/web/bootstrap.ps1) and put into the root directory:

{% asset_img TheAssetsWithScript.png "ASP.NET Application final structure" %}

> Important! Exposing environment variables from process level to machine level is not good practice and you should be careful. It might be security hole (violation of [least privilege principle](http://en.wikipedia.org/wiki/Principle_of_least_privilege)) in case of uncontrolled environment, because they can be easily accidentally leaked, e.g. log environment variables when third-party applications crash or some monitoring tools collect them as well. In our case, we have full control on environment's isolation.

Last step is to add support of new configuration mechanism in the existing application:

``` csharp
public string GetAppSetting(string appSettingName)
{
    return GetSettingFromEnvironmentVariable(appSettingName) ??
        ConfigurationManager.AppSettings[appSettingName];
}

public string GetConnectionString(string connectionStringName)
{
    return GetSettingFromEnvironmentVariable(connectionStringName) ?? 
        ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
}

public string GetSettingFromEnvironmentVariable(string configKey)
{
    return Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.Process) ??
        Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.User) ??
        Environment.GetEnvironmentVariable(configKey, EnvironmentVariableTarget.Machine);
}
```

Finally, you can read the connection strings and application settings from the ASP.NET application.

## Compose & Run Distributed Application

To run the entire solution you just start the containers using [docker-compose up](https://docs.docker.com/compose/reference/up/) command from the location where your docker-compose.yml file is:

``` powershell
docker-compose up --build
```

Compose will start both containers and attach to each other. First run will take a bit longer to build *surveyserver-api* image.

``` powershell
PS C:\...\SurveyServer\Publish> docker-compose up --build
Building surveyserver-api
Step 1/6 : FROM microsoft/aspnet
 ---> 088e495e5e54
Step 2/6 : ARG source=.
 ---> Running in 37d73e358529
 ---> aa423352e4bc
Removing intermediate container 37d73e358529
Step 3/6 : WORKDIR /inetpub/wwwroot
 ---> Running in 53a3018b4a50
 ---> f0ccba28003b
Removing intermediate container 53a3018b4a50
Step 4/6 : COPY ${source} .
 ---> 2ff0a921290f
Removing intermediate container b08ec8806d6c
Step 5/6 : RUN powershell -Command Set-ItemProperty -Path 'HKLM:\SYSTEM\CurrentControlSet\Services\Dnscache\Parameters'
-Name ServerPriorityTimeLimit -Value 0 -Type DWord
 ---> Running in dcea1ada101e
 ---> f8f29d76af1d
Removing intermediate container dcea1ada101e
Step 6/6 : ENTRYPOINT powershell ./bootstrap.ps1
 ---> Running in 3f54d4d0cffb
 ---> 30693676d244
Removing intermediate container 3f54d4d0cffb
Successfully built 30693676d244
Creating surveyserver-db
Creating surveyserver-api
Attaching to surveyserver-db, surveyserver-api
surveyserver-db     | VERBOSE: Starting SQL Server
surveyserver-db     | VERBOSE: Changing SA login credentials
surveyserver-db     | VERBOSE: Started SQL Server.
```

Again, you need to docker inspect the container to get the IP address:

``` powershell
docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" surveyserver-api
```

Good time to run integration tests and verify that all tests are "green":

{% asset_img IntegrationTests.png "Integration Tests" %}

At this point you have the fully working application that uses benefits of Containers, which are mentioned at the beginning of this blog post. 

In this blog post, I do not touch how to spin up a database in different ways and for different enviroments (Dev,Staging,Production), because it is a big topic. My recommendation is to check [Docker Labs repository](https://github.com/docker/labs/blob/master/windows/sql-server/part-3.md). According this reference you can easily modify the Compose file to use a host mount for the database volume, so your data is safe even if you remove the containers and create new ones.

## Summary

In this topic, you have seen the process of migration, how to move, split (if it is needed) and run an existing ASP.NET MVC application in a Windows Server container. The existing application was modified with very small changes related to configuration approach, but it is optional and you can migrate even with zero changes. Leveraging Windows Server Containers is the simplest journey to innovate your existing applications and apply modern development practices.

## Links 
- [Getting Started with Windows Containers](https://github.com/docker/labs/tree/master/windows/windows-containers)
- [Windows Containers Documentation](https://docs.microsoft.com/en-us/virtualization/windowscontainers/index)
- [Dockerfile on Windows](https://docs.microsoft.com/en-us/virtualization/windowscontainers/manage-docker/manage-windows-dockerfile)
- [SQL Server Lab](https://github.com/docker/labs/tree/master/windows/sql-server)
- [MSDN Forums](https://social.msdn.microsoft.com/Forums/en-US/home?forum=windowscontainers)
- [Debug ContainerHost](https://github.com/Microsoft/Virtualization-Documentation/tree/live/windows-server-container-tools/Debug-ContainerHost)