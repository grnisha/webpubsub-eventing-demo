param buildNumber string
@allowed([
  'West US2'
  'Central US'
  'East US2'
  'West Europe'
  'East Asia'
  'resourceGroup().location'
])
param location string = 'resourceGroup().location'
param nameSuffix string //= uniqueString(resourceGroup().id)

@minLength(3)
@maxLength(24)
@description('The name of the storage account')
param sgName string = 'stg${nameSuffix}'

@allowed([
  'Standard_LRS'
  'Standard_GRS'
])
param sku string = 'Standard_LRS'

param logName string = 'log-${nameSuffix}'
param appInsName string = 'appi-${nameSuffix}'
param planName string = 'asp-${nameSuffix}'
param planSku string ='Y1'
param planTier string ='Dynamic'
param funcAppName string = 'func-${nameSuffix}'
param pubsubName string = 'pubsub-${nameSuffix}'
param cosmosName string = 'cosmos-${nameSuffix}'
param swaName string = 'swa-${nameSuffix}'
param repoUrl string 

// WebPubSub
module webpubsubModule 'modules/webpubsub.bicep' = {
  name: 'webpubsub-${buildNumber}'
  params: {
    name:pubsubName
    location:location
  }
}

//Cosmos DB
module cosmosDbModule 'modules/cosmosdb.bicep' = {
  name: 'cosmosdb-${buildNumber}'
  params: {
    accountName: cosmosName
    location:location
  }
}
// Storage account
module storageAccountModule 'modules/storage.bicep' = {
  name: 'storageAccount-${buildNumber}'
  params: {
    sgName:sgName
    location:location
    sku:sku
  }
}

// Log Analytics
module logAnalyticsModule 'modules/loganalytics.bicep' = {
  name: 'log-${buildNumber}'
  params: {
    name: logName
    location:location
  }
}
// Application insights
module appInsightsModule 'modules/appinsights.bicep' = {
  name:'appInsights-${buildNumber}'
  params:{
    name:appInsName
    rgLocation:location
    workspaceResourceId:logAnalyticsModule.outputs.logAnalyticsWorkspaceId
  }
}

// App service plan
module aspModule 'modules/appserviceplan.bicep' = {
  name:'appServicePlan-${buildNumber}'
  params:{
    planName:planName
    planLocation:location
    planSku:planSku
    planTier:planTier
  }
}


// Function app (without settings)
module functionAppModule 'modules/functionapp.bicep' = {
  name: 'functionApp-${buildNumber}'
  params:{
    location:location
    functionAppName:funcAppName
    planName:aspModule.outputs.planId
  }
  dependsOn:[
    storageAccountModule
    aspModule
  ] 
}

// Static Web App
module staticWebAppModule 'modules/staticwebapp.bicep' = {
  name: 'staticWebApp-${buildNumber}'
  params:{
    location:location
    name :swaName
    functionAppName:funcAppName
    repoUrl:repoUrl
  }
  dependsOn:[
    functionAppModule
  ]
}

// Function app settings
module functionAppSettingsModule 'modules/functionappsettings.bicep' = {
  name: 'functionAppSettings-${buildNumber}'
  params: {
    appinsightsName: appInsName
    functionAppName: functionAppModule.outputs.prodFunctionAppName
    storageAccountName: sgName
    cosmosDbName: cosmosName
    webpubsubName: pubsubName
  }
  dependsOn:[
    functionAppModule
    appInsightsModule
    webpubsubModule
    cosmosDbModule
  ]
}
