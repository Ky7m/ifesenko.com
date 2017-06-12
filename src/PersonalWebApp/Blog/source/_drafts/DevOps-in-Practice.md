---
title: DevOps in Practice
tags:
  - Azure
  - PowerShell
  - VSTS
categories:
  - DevOps
  - Azure
---

[Also available on TechNet UK Blog](https://blogs.technet.microsoft.com/uktechnet/2017/05/18/devops-in-practice/)

Within the past few years, business has changed. There has been a significant shift in the dynamics between products and customer relationships. In a modern agile world, it is important to deliver your product to the end user as quickly as possible, retrieve feedback, and then deliver a new version that includes all the requested features. 
In response to the rapid changes in business today, the optimisation of development processes, such as automating software deployment and infrastructure changes, can help organisations keep up with their business.
To achieve this, you need DevOps.

<!-- more -->

## DevOps automates automation

Each development process starts with an idea—we bring ideas to life with development. We write code, cover business logic with unit tests, save all our code change history in source control systems, build artifacts from source code, verify them, and then we have a product that can be delivered to the customer. Some people think that DevOps is just automation through Continuous Integration (CI) and Continuous Delivery (CD) practices, but DevOps is more about automating automation itself. In fact, a lot of engagements that require DevOps practices are connected to building and/or releasing automation.
For example, say you need an entire automated development process for a new project. You create a Git repository, then configure the CI process to build artifacts from the source code, and finally set up CD to deploy artifacts to target environments. It seems easy, but usually it is a set of similar steps across different projects, and you can automate this process to reduce manual work and exclude human errors.
If we use Visual Studio Team Services (VSTS) or Team Foundation Server (TFS) as a solution for collaboration hub and deploy to Microsoft Azure App Service or Docker container we are able to leverage the power of a [Yeoman generator](https://www.npmjs.com/package/generator-team) or [PowerShell module](https://www.powershellgallery.com/packages/Team). With either of these tools you eliminate manual work and are able to create the entire pipeline in a matter of minutes, instead of hours or days. Then all your team has to do is commit changes and watch them flow into the Azure App Service or a Docker container. I personally love VSTS, as does the rest of my team at SoftServe, and constantly use it for new projects—in minutes, I can run all the required services for teams to share code, track work, and ship software without having to install or configure a single server. This practice of using scripts to automatically create the “ready to go” framework for the CI/CD process gives our clients—and us—a solid time reduction at the start of project.

## DevOps is cost optimisation in modern environment

Typically, environment provisioning is a complex and time-consuming process, and so automating it becomes important. The approach to provisioning an environment varies across organisations, but let’s take the Infrastructure as Code (IaC) approach. On the one hand, adopting this practice and leveraging automation can drastically improve the cycle time and reduce the time to market. On the other hand, without a properly defined process or proactive monitoring, it’s easy to get a huge invoice from your cloud provider at the end of the month. Our team focuses on the engineering processes connected to resource management. Our first step is to verify whether they automate the deprovisioning of allocated DevTest resources for non-business hours. Then we receive a steady stream of requests to help with cloud cost optimisations, not just for production reasons but for staging and DevTest as well.
Speaking from experience, communication between developers and operations teams is imperative to success. In addition to infrastructure-monitoring using [Operations Management Suite](https://www.microsoft.com/en-us/cloud-platform/operations-management-suite), we also use application performance monitoring (APM). It allows us to identify the best configuration for optimal performance at the lowest possible cost, based on comprehensive system-level data analysis (such as peak CPU, memory, IOPS, and network usage). This the next step to understanding workload and spending optimisation. Typically for .NET based solutions we integrate [Application Insights](https://azure.microsoft.com/en-us/services/application-insights/).
If you use cloud-based resources there are many cases in which you’ll want to understand costs at a micro level—breaking down projected costs by category and individual resources as needed. In looking at the Microsoft Azure platform as a cloud provider, I would recommend taking a look at [PowerBI](https://powerbi.microsoft.com/en-us/) with the [Microsoft Azure Consumption Insights](https://powerbi.microsoft.com/en-us/documentation/powerbi-content-pack-azure-consumption-insights/) content pack. It is an excellent tool, and one of a set of proven tools that SoftServe uses internally.
For one of our clients, we succeeded in a cost reduction of up to 40%. Based on analytics from PowerBI augmented with data from [Azure Monitor](https://azure.microsoft.com/en-us/pricing/details/monitor/) we found “orphan” resources that could be safely deleted. We then computed instances that should be up and running only a few hours per day, and identified which can be scaled down to the lowest pricing tier or even deallocated. The integration with the APM tool demonstrated a real utilisation of compute resources in production and gave a full understanding of a usage patterns by end users. It allowed us to scale application services according to the defined usage patterns and drastically decrease costs.

## DevOps optimises development and testing processes

If you are considering moving to the cloud, it’s better to start from DevTest workloads than from line of business (LOB) applications. By the same token, successful companies should understand that often there is a delay in creating and managing environments for DevTest workloads. Moving DevTest workloads to the cloud solves issues connected to the collaboration between IT department and DevTest teams. But still, there are open issues that should be addressed (e.g., how to automatically enforce company policies for all requested workloads, how to apply quotas per team, how to set strictly automated shutdowns to minimise costs, etc.). For some clients, we had to build custom applications that tailor to required scenarios and address open issues. But before implementing any custom development, try to evaluate your scenario with an existing toolchain.
For example, Microsoft Azure has [Azure DevTest Labs](https://azure.microsoft.com/en-us/services/devtest-lab/). It is a service that helps to address many of common issues. It helps developers and testers quickly create environments in Microsoft Azure while minimising waste and maximising cost savings without adding too much process overhead. We have started use this service internally and now encourage all to take a look and spend some time to evaluate it against different scenarios to suit your needs.

## Conclusion

DevOps in practice is a journey, enabling the continuous delivery of value to you and to your end users. It allows teams be more productive and to get more done. With DevOps in place, teams can build more cost-effective application workloads and adopt the continuous optimisation of existing engineering processes.