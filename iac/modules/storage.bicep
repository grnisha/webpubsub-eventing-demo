param sgName string
param location string = resourceGroup().location
param sku string
param kind string = 'StorageV2'
param tier string = 'Hot'

resource stg 'Microsoft.Storage/storageAccounts@2021-02-01' = {
  name: sgName
  location: location
  kind: kind
  sku:{
    name:sku
  }
  properties:{
    accessTier: tier
  }
}

