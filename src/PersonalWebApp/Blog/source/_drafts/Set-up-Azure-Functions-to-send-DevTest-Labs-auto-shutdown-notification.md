---
title: Set up Azure Functions to send DevTest Labs auto-shutdown notification
tags:
categories:
---
In this post I would like to share my experience in integration Azure DevTest Labs and Azure Functions services to make my life easier. 
A few words about the problem, I use virtual machines, which are hosted in cloud (Microsoft Azure in my case), as my developer boxes. It means that I need powerfull instances and obviously they are not cheap. To minimize costs I selected a great service to manage my VMs, called [Azure DevTest Labs](https://azure.microsoft.com/en-us/services/devtest-lab/), where a key feature is ability to set policy-based automated starts and shutdowns of virtual machines, for example I scheduled for automatic start each day except Saturday and Sunday at 9:30 AM and automatic shutdown at 6:00 PM. This feature saves more than half of VM's price. But there is other side of the coin, it is very possible to be kicked out of the VM unexpectedly without a last chance to save my work. Azure Team gives an excellent feature that supports auto-shutdown notifications (15 minutes before the auto-shutdown triggered). What is more, the notification also provides links for each VM to skip the auto-shutdown for this time, snooze for an hour or 2 hours, so I can keep working on the VM.
In the current release, notification is sent through the [Webhooks endpoints]((https://en.wikipedia.org/wiki/Webhook), it is supported by various apps and allows to implement own way for sending notification.
There is an great [post](https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/) that walks you through how to get auto-shutdown notification from emails by using Azure Logic Apps. I have started with this approach, but I think Azure Logic Apps fits better for more complex solutions than this simple automation. In addition to this I like code-first integration than UI drag and drop programming. You can find mode details on [docs.microsoft.com](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs). Logic Apps service is not expensive, but with Azure Functions service I got the same functionality for free, so let's start!

## Set up web hook dev test labs

https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/
https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs

## Create azure functions

## implement logic

## summary