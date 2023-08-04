# Azure Web Pubsub - Azure Function App - Static Website

## Introduction

This demo shows how to use the service bindings in Azure Functions to integrate with Azure Cosmos DB and Azure Web Pubsub  Service to send real-time messages when new events appear on an Azure Cosmos DB change feed. The client in this demo is a static website.

- **Azure Cosmos DB**: It provides a globally distributed, multi-model database service used to store data.
- **Azure Azure Web Pubsub  Service**: It enables real-time communication between the server and connected clients using web sockets.
- **Azure Functions**: It hosts the Products API, which sends events to connected clients using SignalR.
- **Azure Static Website**: It hosts the Blazor client application and provides a static web hosting service.


## Prerequisites

To build this sample locally,

 - [Visual Studio Code](https://code.visualstudio.com/) installed on one of the supported platforms.
 - [Azure Functions extension](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azurefunctions).
 - [Azure Tools extension pack](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vscode-node-azure-pack)
 - An active [Azure subscription](https://learn.microsoft.com/en-us/azure/guides/developer/azure-developer-guide#understanding-accounts-subscriptions-and-billing).
 - [.NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) for the blazor client

An active Azure subscription. If you don't yet have an account, you can create one from the extension in Visual Studio Code.

## Deploy to Azure

Deploy the infrastructure using github action workflow [deploy-main-infra.yml](https://raw.githubusercontent.com/grnisha/signalr-eventing-demo/main/.github/workflows/deploy-main-infra.yml) and the applications using [deploy-apps.yml](https://raw.githubusercontent.com/grnisha/signalr-eventing-demo/main/.github/workflows/deploy-apps.yml)

Please configure the following secrets to use github actions.
  - AZURE_CREDENTIALS - Service principle used to authenticate to Azure. Please check [documentation](https://learn.microsoft.com/en-us/azure/developer/github/connect-from-azure?tabs=azure-portal%2Cwindows#create-a-service-principal).
  - AZURE_RG - Azure Resource Group Name.
  - AZURE_SUBSCRIPTION - Azure Subscription Id.
  - LOCATION - Azure Region. 
  - NAME_SUFFIX - A unique name suffix.



