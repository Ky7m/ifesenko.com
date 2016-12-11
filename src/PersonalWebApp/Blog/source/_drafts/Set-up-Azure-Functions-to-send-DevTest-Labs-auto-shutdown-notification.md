---
title: Set up Azure Functions to send DevTest Labs auto-shutdown notification
tags:
categories: Azure
---
In this post I would like to share my experience in integration Azure DevTest Labs and Azure Functions services to make my life easier. 
A few words about the problem, I use virtual machines, which are hosted in cloud (Microsoft Azure in my case), as my developer boxes. It means that I need powerfull instances and obviously they are not cheap. To minimize costs I selected a great service to manage my VMs, called [Azure DevTest Labs](https://azure.microsoft.com/en-us/services/devtest-lab/), where a key feature is ability to set policy-based automated starts and shutdowns of virtual machines, for example I scheduled for automatic start each day except Saturday and Sunday at 9:30 AM and automatic shutdown at 6:00 PM. This feature saves more than half of VM's price. But there is other side of the coin, it is very possible to be kicked out of the VM unexpectedly without a last chance to save my work. Azure Team gives an excellent feature that supports auto-shutdown notifications (15 minutes before the auto-shutdown triggered). What is more, the notification also provides links for each VM to skip the auto-shutdown for this time, snooze for an hour or 2 hours, so I can keep working on the VM.
In the current release, notification is sent through the [Webhooks endpoints]((https://en.wikipedia.org/wiki/Webhook), it is supported by various apps and allows to implement own way for sending notification.
There is an great [post](https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/) that walks you through how to get auto-shutdown notification from emails by using Azure Logic Apps. I have started with this approach, but I think Azure Logic Apps fits better for more complex solutions than this simple automation. In addition to this I like code-first integration than UI drag and drop programming. You can find mode details on [docs.microsoft.com](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs). Logic Apps service is not expensive, but with Azure Functions service I got the same functionality for free, so let's start!

## Create a new Azure Function

For our task, we will use an HTTP-triggered Azure Function. You can get more information about Azure Functions via [offical documentation](https://azure.microsoft.com/en-in/services/functions/). I also recommend to watch a 2-minute overview video by Scott Hanselman:
[![Get Started with Azure Functions](https://img.youtube.com/vi/nCExarOuPAw/maxresdefault.jpg)](https://www.youtube.com/watch?v=nCExarOuPAw)
This is more than enough to start working with Azure Functions.
As I mentioned before we need to choose HTTP-triggered template:

{% asset_img HTTP-triggered-template.jpg "Azure Function setup" %}

Once Azure Function is created we need to save Function url value that will be required for the next step:

{% asset_img Azure-function-url.jpg "Azure Function url value" %}

Now we can proceed and integrate our Azure Function with DevTest Labs service.

## Enable auto-shutdown notification in DevTest Labs

It is very straightforward to enable auto-shutdown notification. We have already got a Webhook URL which the notification will send to. 
Below are the steps you will follow to enable auto-shutdown notification in your lab:
- Go to the Auto-shutdown settings of your lab.
- For the option Send notification 15 minutes before auto-shutdown, select Yes.

{% asset_img DevTestLabs-auto-shutdown-settings.jpg "Auto-shutdown notification settings" %}

- Input the Webhook URL.
- Click Save to save the settings.

https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/
https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs
http://www.dotnetcurry.com/visualstudio/1326/create-vsts-extension-azure-functions
http://blogs.lessthandot.com/index.php/enterprisedev/cloud/azure/csv-file-to-api-using-azure-functions-csvaas/

## implement logic

## summary