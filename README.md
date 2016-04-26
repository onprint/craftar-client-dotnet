# craftar-sdk-dotnet
Catchoom's CraftAR SDK for .NET
For more information on Catchoom APIs: [Catchoom Documentation](http://catchoom.com/documentation)

## Main features
This is the very first version of CraftAR SDK for .NET. Currently it supports very few features, but it will soon cover the main part of Catchoom management and search APIs.
Here is what it can do for you.

### Items
#### Create item
Create a new item, with a name given in parameter and the default collection Id from configuration.
#### Delete item
Delete the item having the id given in parameter.

### Images
#### Create image
Create a new image, with the image content byte array and item id given in parameter.
#### Delete image
Deletes the image having the id given in parameter.

### Image recognition
#### Search image
Searches for the image given in parameter as a HTTP Content.

## Configuration
The configuration needs are:
* **APIKey**: find it in your MyCraftAR account
* **DefaultCollectionId**: an existing collection
* **HostModify**: management API url, default is _https://my.craftar.net/api/v0_
* **HostSearch**: image recognition API url, default is _https://search.craftar.net/v1_
