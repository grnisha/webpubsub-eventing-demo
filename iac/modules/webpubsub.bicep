@description('The globally unique name of the Web pubsub resource to create.')
param name string = 'pubsub-${uniqueString(resourceGroup().id)}'

@description('Location for the SignalR resource.')
param location string = resourceGroup().location

@description('The pricing tier of the Web Pubsub resource.')
@allowed([
  'Free_F1'
  'Standard_S1'
  'Premium_P1'
])
param pricingTier string = 'Free_F1'

@description('The number of Web pubsub Units.')
@allowed([
  1
  2
  5
  10
  20
  50
  100
])
param capacity int = 1

resource signalR 'Microsoft.SignalRService/webPubSub@2023-02-01' = {
  name: name
  location: location
  sku: {
    capacity: capacity
    name: pricingTier
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    tls: {
      clientCertEnabled: false
    }
    networkACLs: {
      defaultAction: 'Deny'
      publicNetwork: {
        allow: [
          'ClientConnection'
          'ServerConnection'
          'RESTAPI'
          'Trace'
        ]
      }
      privateEndpoints: []
    }
    publicNetworkAccess: 'Enabled'
    disableLocalAuth: false
    disableAadAuth: false
  }
}

