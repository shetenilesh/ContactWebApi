Project is implemented using Dot Net Core, Entity Framework using in memory database, 
Log4Net for logging, MS Test for Unit tests along with Dependency Injection.

Solution name is - ContactsWebApi
There are 2 Projects

1. Contacts - Rest API created using Dot net core
Project structure as below - 
Automapper - for mapping Model properties with Entities
Controllers - for APIs
Interfaces - for repository interfaces which used for Unit Tests as well uning Dependency Injection
Models - 
API - for model properties and data annotations
Entities - for DB entities

Repositories - for storing data in db storage


2. Contacts.Test - for Unit tests using MS Test framework and Dependency Injection.

Build and Deployment - 
Hosting is handled using Swagger.
So ContactsWebApi.sln needs to be opened and then build solution. It will load all required Nuget packages.
Once build is done. need to press F5 in visual studio to launch web api solution.
Then below URL can be used to check API functionality on swagger - 
http://localhost:51229/swagger/index.html 


