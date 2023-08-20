# Get Resume Counter: Azure Function

## Overview

> This document provides a detailed description of the "GetResumeCounter" Azure Function. The function is designed to track site visit count and is linked to Azure Cosmos DB for data storage.

## Implementation Details
### Dependencies

* Newtonsoft.Json: For JSON serialization and deserialization.
* Microsoft Azure WebJobs Extensions: For the HttpTrigger and CosmosDB attributes.
* Microsoft.Extensions.Logging: For logging information and errors.

### Function Signature

``` csharp
[FunctionName("GetResumeCounter")]
public static HttpResponseMessage Run(
    [HttpTrigger(AuthorizationLevel.Function, "get","post", Route = null)] HttpRequest req,
    [CosmosDB(
        databaseName: "azureresume",
        containerName: "Counter",
        Connection = "AzureResumeConnectionString", 
        Id = "1",
        PartitionKey ="1")] Counter counter,
    [CosmosDB(
        databaseName: "azureresume",
        containerName: "Counter",
        Connection = "AzureResumeConnectionString",
        Id = "1",
        PartitionKey ="1")] out Counter updatedCounter,
    ILogger log)
```
### Function Logic

1. <b>Logging the Request</b>: The function starts by logging that it has processed a request.

2. <b>Null Check</b>: If the `counter` object retrieved from Cosmos DB is null, an error is logged, and an Internal Server Error response is returned.

3. <b>Updating Counter</b>: The `counter` object's count property is incremented by 1, and the updated counter is assigned to the `updatedCounter` object.

4. <b>Response</b>: The function serializes the original `counter` object to JSON and returns an HTTP OK response containing the JSON.

### Counter Class

The `Counter` class represents the data model for the counter, containing two properties:

* <b>Id</b>: The unique identifier for the counter.
* <b>Count</b>: The current count value.

```csharp
public class Counter
{
    [JsonProperty(PropertyName = "id")]
    public string Id {get; set;}
    [JsonProperty(PropertyName = "count")]
    public int Count {get; set;}
}
```

## Functionality

The GetResumeCounter function is triggered by HTTP GET or POST requests. It retrieves the current counter value from Azure Cosmos DB, increments it, and updates the database. The function then returns the original counter value as a JSON response.