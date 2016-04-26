# craftar-client-dotnet
Catchoom's CraftAR APIs implementation for .NET
For more information on Catchoom APIs: [Catchoom Documentation](http://catchoom.com/documentation)

## Main features
This is the very first version of CraftAR APIs implementation for .NET. Currently it supports very few features, but it will soon cover the main part of CraftAR [Management API](http://catchoom.com/documentation/management-api/) and [Image Recognition API](http://catchoom.com/documentation/image-recognition-api/).
Here is what it can do for you.

### Items
#### Create item
Create a new item, with a ItemRequest given in parameter.If no collection Id is given, it will use the default collection Id in configuration.
#### Delete item
Delete the item having the id given in parameter.

### Images
#### Create image
Create a new image, with a ImageRequest given in parameter.
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
