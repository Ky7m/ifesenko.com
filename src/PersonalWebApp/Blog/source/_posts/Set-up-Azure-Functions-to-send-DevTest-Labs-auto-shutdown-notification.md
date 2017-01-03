---
title: Set up Azure Functions to send DevTest Labs auto-shutdown notification
date: 2016-12-13 10:37:25
tags: [Azure Functions, Serverless, DevTest, SendGrid]
categories: [Azure, DevOps]
---
In this post I would like to share my experience in integration Azure DevTest Labs and Azure Functions services to make my life easier. A few words about the problem, I use virtual machines, which are hosted in cloud (Microsoft Azure in my case), as my developer boxes. It means that I need powerful instances and obviously, they are not cheap. To minimize costs I selected a great service to manage my VMs, called [Azure DevTest Labs](https://azure.microsoft.com/en-us/services/devtest-lab/), where a key feature is ability to set policy-based automated starts and shutdowns of virtual machines, for example I scheduled for automatic start each day except Saturday and Sunday at 9:30 AM and automatic shutdown at 6:00 PM. This feature saves more than half of VM's price. But there is other side of the coin, it is very possible to be kicked out of the VM unexpectedly without a last chance to save my work. Azure Team gives an excellent feature that supports auto-shutdown notifications (15 minutes before the auto-shutdown triggered). What is more, the notification also provides links for each VM to skip the auto-shutdown for this time, snooze for an hour or 2 hours, so I can keep working on the VM.
In the current release, notification is sent through the [Webhooks endpoints](https://en.wikipedia.org/wiki/Webhook), it is supported by various apps and allows to implement own way for sending notification. This might be an useful [post](https://blogs.msdn.microsoft.com/devtestlab/2016/08/30/set-up-devtest-labs-to-send-auto-shutdown-notification/) that walks you through how to get auto-shutdown notification from emails by using Azure Logic Apps. I have started with this approach, but I think Azure Logic Apps fits better for more complex solutions than this simple automation. In addition to this I like code-first integration than UI drag and drop programming (you can find mode details on [docs.microsoft.com](https://docs.microsoft.com/en-us/azure/azure-functions/functions-compare-logic-apps-ms-flow-webjobs)). Logic Apps service is not expensive, but with Azure Functions service I got the same functionality for free, so let's start!

## Create a new Azure Function

For our task, we will use an HTTP-triggered Azure Function. You can get more information about Azure Functions via [official documentation](https://azure.microsoft.com/en-in/services/functions/). I also recommend you to watch a 2-minute overview video by Scott Hanselman:

{% youtube nCExarOuPAw %}

This is more than enough to start working with Azure Functions. As I mentioned before we need to choose HTTP-triggered template:

{% asset_img HTTP-triggered-template.png "Azure Function setup" %}

Once Azure Function is created we need to save Function url value that will be required for the next step:

{% asset_img Azure-function-url.png "Azure Function url value" %}

Now we can proceed and integrate our Azure Function with DevTest Labs service.

## Enable auto-shutdown notification in DevTest Labs

It is very straightforward to enable auto-shutdown notification. We have already got a Webhook URL which the notification will send to. Below are the steps you will follow to enable auto-shutdown notification in your lab:

- Go to the Auto-shutdown settings of your lab.
- For the option Send notification 15 minutes before auto-shutdown, select Yes.

{% asset_img DevTestLabs-auto-shutdown-settings.png "Auto-shutdown notification settings" %}

- Input the Webhook URL.
- Click Save to save the settings.

Now we can get back to Azure Function and implement logic that will handle this notification and send an email.

## How to send an email from Azure Function

First of all, we need to understand what schema of request is. I have used built-in log functionality of Azure Function to capture JSON schema that is used by caller and deserialize using Json.NET. It requires to add the reference at the top of file:

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

From this schema we are able to get all information what we need to send notification. There are two options to do this. First option is just to extract value from "text" field and send as body of email. Second option is to build your own template. I prefer a second one. The C# .NET code for implementatin of this option would look like this:

```cs
var subject = $"Planned shutdown of {data.vmName}";
var body = $"{data.vmName} is scheduled to shutdown in 15 minutes. \n\n"+
$"Delay shutdown for 1 hour: {data.delayUrl60} \n\n"+
$"Delay shutdown for 2 hours: {data.delayUrl120} \n\n"+
$"Skip this shutdown: {data.skipUrl} \n\n";
```

Finally, we are ready to send an email. From all available options I really like [SendGrid](https://sendgrid.com/) service. It allows you to send 12k emails per month for free. If you are already registered and have account, you need to generate an access key on [API Keys page](https://app.sendgrid.com/settings/api_keys). Give access only to "Mail Send" action, it is enough for our purposes:

{% asset_img SendGrid-api-key.png "SendGrid API Key" %}

Once we get Api key we can import the SendGrid nuget package into our Function by creating a project.json file that contains this following:

```json
{
  "frameworks": {
    "net46":{
      "dependencies": {
        "Sendgrid": "8.0.5"
      }
    }
   }
}
```

After click Save button you will see starting of NuGet packages restore process in Logs output window.

{% asset_img Add-project-json.png "Add project.json file" %}

To send an email using SendGrid API include following namespaces:

```cs
using SendGrid;
using SendGrid.Helpers.Mail;
```
and then we can use this code snippet:

```cs
var apiKey = "{Your SendGrid API Key}";
var sg = new SendGridAPIClient(apiKey);
var myEmail = new Email("{Your email address}");
var content = new Content("text/plain",body);
var mail = new Mail(myEmail, subject, myEmail, content).Get();
var response = await sg.client.mail.send.post(requestBody: mail);
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
    var sg = new SendGridAPIClient(apiKey);
    var myEmail = new Email("{Your email address}");
    var content = new Content("text/plain",body);
    var mail = new Mail(myEmail, subject, myEmail, content).Get();
    
    log.Info(mail);
    
    var response = await sg.client.mail.send.post(requestBody: mail);
    log.Info($"Response: {response.StatusCode} {await response.Body.ReadAsStringAsync()}");
    
    return req.CreateResponse(HttpStatusCode.OK);
}
```
That is all for implementation phase and now is good time to test functionality.

## Test Azure Function

Azure portal has embedded tab to test your Function:

{% asset_img Test-azure-function.png "Test Azure Function" %}

Alternative way is to install [Visual Studio Tools for Azure Functions](https://blogs.msdn.microsoft.com/webdev/2016/12/01/visual-studio-tools-for-azure-functions/). It is a wrapper for [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-cli), but Visual Studio extension and CLI tool only currently work on Windows, since the underlying Functions Host is not yet cross-platform.
In our case Azure portal is more than enough. I took captured before request and put as body to test Function and click Run. Within just a couple of seconds, depending on which SMTP relay / provider you are using, you should receive an email being sent from your Azure Function App.

{% asset_img Planned-shutdown-notification.png "Planned shutdown notification" %}

## Summary

Azure Functions service is pretty useful and helps a lot in automation. There are some additional areas out there where I use Azure Functions and I will describe them in next blog posts. I hope this blog post has been useful. Stay tuned!