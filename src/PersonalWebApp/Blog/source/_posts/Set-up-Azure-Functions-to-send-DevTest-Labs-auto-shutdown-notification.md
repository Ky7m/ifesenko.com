---
title: Set up Azure Functions to send DevTest Labs auto-shutdown notification
tags:
  - Azure Functions
  - Serverless
  - DevTest
  - SendGrid
categories:
  - Azure
date: 2016-12-13 10:37:25
---


In the following post, I would like to share my experience in integration of Azure DevTest Labs and Azure Functions services that made my life easier. 

<!-- more -->

A few words about the problem: I use virtual machines, hosted in cloud (Microsoft Azure in my case), as my developer boxes. It means that I need powerful instances and they are obviously not cheap. To minimize costs I selected a great service to manage my VMs called [Azure DevTest Labs](https://azure.microsoft.com/en-us/services/devtest-lab/), its key feature is the ability to set policy-based automated starts and shutdowns of virtual machines. For example, I scheduled an automatic start at 9:30 AM and an automatic shutdown at 6:00 PM every day except Saturday and Sunday. This feature saves more than half of VM’s price. Anyway, there is the other side of the coin – there is a possibility to be unexpectedly kicked out of the VM without a chance to save my work. Azure Team gives an excellent feature that supports auto-shutdown notifications (15 minutes before the auto-shutdown is triggered). Moreover, the notification also provides links for each VM to skip auto-shutdown this time, snooze for 1 or 2 hours, so I can keep working on the VM. 
In the current release, notification is sent through the [Webhooks endpoints](https://en.wikipedia.org/wiki/Webhook), it is supported by various apps and allows to implement one’s own way of sending notification. This might be a [post](https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/), which walks you through getting an auto-shutdown notification from emails by using Azure Logic Apps. I have started with this approach, but I think Azure Logic Apps fits better for more complex solutions than this simple automation. In addition, I like code-first integration more than UI drag and drop programming (you can find mode details on [docs.microsoft.com](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs)). Logic Apps service is not expensive, but with Azure Functions service I got the same functionality for free, so let’s start!

## Create a new Azure Function

We will use an HTTP-triggered Azure Function for our task. You can get more information about Azure Functions via [official documentation](https://azure.microsoft.com/en-in/services/functions/). I also recommend you to watch a 2-minute overview video by Scott Hanselman:

{% youtube nCExarOuPAw %}

This is more than enough to start working with Azure Functions. As I mentioned before we need to choose HTTP-triggered template:

{% asset_img HTTP-triggered-template.png "Azure Function setup" %}

Once Azure Function is created we need to save Function URL value required for the next step:

{% asset_img Azure-function-url.png "Azure Function url value" %}

Now we can proceed and integrate our Azure Function with DevTest Labs service.

## Enabling auto-shutdown notification in DevTest Labs

It is very easy to enable auto-shutdown notification. We already have a Webhook URL to which the notification will be send. Below are the steps you should follow to enable auto-shutdown notification in your lab:

- Go to the Auto-shutdown settings of your lab.
- For the Send notification 15 minutes before auto-shutdown option, select Yes.

{% asset_img DevTestLabs-auto-shutdown-settings.png "Auto-shutdown notification settings" %}

- Input the Webhook URL.
- Click Save to save the settings.

Now we can get back to Azure Function and implement logic that would trigger this notification and send an email.

## How to send an email from Azure Function

Firstly, we need to understand what request schema is. I have used built-in log functionality of Azure Function to capture JSON schema that is used by caller and to deserialize using Json.NET. It requires adding the reference at the top of file:

```
#r "Newtonsoft.Json"
```

After that we can use JsonConvert class:

```cs
var jsonContent = await req.Content.ReadAsStringAsync();
log.Info($"Request:{jsonContent}");
dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);
```

An example of the request body would look like this (I cut some sensitive information):

```json
{
   "skipUrl":"{Skip this autoshutdown long url}",
   "delayUrl60":"{One hour delay long url}",
   "delayUrl120":"{Two hours delay long url}",
   "vmName":"CloudStation",
   "guid":"a0242323-234f-4ff6-23dc-dafc26sdf50f",
   "owner":"{Your email address}",
   "eventType":"AutoShutdown",
   "text":"Azure DevTest Labs notification: The virtual machine CloudStation in lab hpcdevenvironments with subscriptionId {Your subscription id} is scheduled for automatic shutdown in 15 minutes. Machine user is {Your email address}. <https://prod.skipdelay.vsdth.visualstudio.com/skip?...|Skip> this autoshutdown. <https://prod.skipdelay.vsdth.visualstudio.com/delay?...|Delay one hour>. <https://prod.skipdelay.vsdth.visualstudio.com/delay? ...|Delay two hours>.",
   "subscriptionId":"{Your subscription id}",
   "resourceGroupName":"hpcdevenvironmentsrg1234",
   "labName":"hpcdevenvironments"
}
```

We are able to get all the needed for sending notification information from the schema. There are two options to do this. First option is just to extract value from “text” field and send as a body of an email. Second option is to build your own template. I prefer to use a second one. The C# .NET code for implementation of this option would look the following way:

```cs
var subject = $"Planned shutdown of {data.vmName}";
var body = $"{data.vmName} is scheduled to shutdown in 15 minutes. \n\n"+
$"Delay shutdown for 1 hour: {data.delayUrl60} \n\n"+
$"Delay shutdown for 2 hours: {data.delayUrl120} \n\n"+
$"Skip this shutdown: {data.skipUrl} \n\n";
```

Finally, we are ready to send an email. Among all of the options available, I really like [SendGrid](https://sendgrid.com/) service. It allows you to send 12k emails per month for free. If you are already registered and have an account, you need to generate an access key on the [API Keys page](https://app.sendgrid.com/settings/api_keys). Giving access to “Mail Send” action only is enough for our purposes:

{% asset_img SendGrid-api-key.png "SendGrid API Key" %}

Once we get Api key we can import the SendGrid NuGet package into our Function by creating a project.json file that contains the following:

```json
{
  "frameworks": {
    "net46":{
      "dependencies": {
        "Sendgrid": "9.1.1"
      }
    }
   }
}
```

After clicking the Save button you will see the NuGet packages restore process starting in Logs output window.

{% asset_img Add-project-json.png "Add project.json file" %}

In order to send an email using SendGrid API include the following namespaces:

```cs
using SendGrid;
using SendGrid.Helpers.Mail;
```
and then we can use this code snippet:

```cs
var apiKey = "{Your SendGrid API Key}";
var client = new SendGridClient(apiKey);
var myEmail = new EmailAddress("someone@example.com");
var mail = MailHelper.CreateSingleEmail(myEmail, myEmail, subject, body, string.Empty);
var response = await client.SendEmailAsync(mail);
```

The code should basically work right away, the only thing we need to do is to enhance code with detailed logs. 

```cs
#r "Newtonsoft.Json"

using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    var jsonContent = await req.Content.ReadAsStringAsync();
    log.Info($"Request:{jsonContent}");
    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonContent);

    var subject = $"Planned shutdown of {data.vmName}";
    var body = $"{data.vmName} is scheduled to shutdown in 15 minutes. \n\n"+
    $"Delay shutdown for 1 hour: {data.delayUrl60} \n\n"+
    $"Delay shutdown for 2 hours: {data.delayUrl120} \n\n"+
    $"Skip this shutdown: {data.skipUrl} \n\n";
    
    var apiKey = "{Your SendGrid API Key}";
    var client = new SendGridClient(apiKey);
    var myEmail = new EmailAddress("someone@example.com");
    var mail = MailHelper.CreateSingleEmail(myEmail, myEmail, subject, body, string.Empty);
     
    log.Info(mail.Serialize());
     
    var response = await client.SendEmailAsync(mail);
    log.Info($"Response: {response.StatusCode} {response.Headers}");
     
    return req.CreateResponse(HttpStatusCode.OK);
}
```

For the implementation phase that is all we need. And now it is about time to test functionality.

## Test Azure Function

Azure portal has embedded tab to test your Function:

{% asset_img Test-azure-function.png "Test Azure Function" %}

Alternative way is to install [Visual Studio Tools for Azure Functions](https://blogs.msdn.microsoft.com/webdev/2016/12/01/visual-studio-tools-for-azure-functions/). It is a wrapper for [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-cli), but Visual Studio extension and CLI tool only currently work on Windows, since the underlying Functions Host is not yet cross-platform.
In our case Azure portal is more than enough. I took a request captured before and put it as body to test Function and clicked Run. Within just a couple of seconds, depending on which SMTP relay / provider you are using, you should receive an email being sent from your Azure Function App.

{% asset_img Planned-shutdown-notification.png "Planned shutdown notification" %}

## Summary

Azure Functions service is pretty useful and helps a lot in automation. There are some additional areas out there where I use Azure Functions and I will describe them in future blog posts. I hope this post has been useful. Stay tuned!