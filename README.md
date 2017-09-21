# Design Hub

Design Hub is intended to be a github-eqsue solution for designers to enable a centralized online respository for projects and files that allows collaboration and version control. 

Design Hub was built using the following technologies
1. ASP.NET core MVC and Identity Framework
1. SQL Server and Entity Framework
1. Theming using Bootstrap
1. Package management using Bower

## Getting Started
There are a few things you need to have setup and ready to go before you can clone and work on this project. 

### Windows Users
This app was built in Visual Studio 2017, and thus the instructions will be written from that perspective. 

1. Download and install [Visual Studio 2017](https://www.visualstudio.com/vs/community/)
1. Upon installation - make sure you select and install the following packages 
  1. Under the `Workloads`tab, have "ASP.NET and Web Development" and ".NET Core cross-platform development" packages selected
  1. Under the `Individual Components` tab, make sure the ".NET Core Runtime" (Under `.Net` heading) and "Entity Framework 6 tools" (Under the SDK's, libraries and frameworks heading) are also selected and install
1. Clone and download this repo into a local directory. 
1. Upon startup, in the `Package Manager Console` (View -> Other Windows -> Package Manager Console) execute `add-migrations initial` and then `update database` - This will initialize everything for the first time on your machine.
1. Press `Ctrl + F5` to run a build

This should get you in a good place to be running locally on your machine. 

## Documentation 

Basic functionality will be outlined here. Refer to comments in the code for more detailed descriptions of methods and arguments. 

### App Overview
Design Hub was built to be a sort of combination of Github and Dropbox.
A place users can upload documents and keep track of different versions as changes need to be made. 

### Home Screen/Login
Upon visiting the site, a user is presented with a splash screen with a brief overview of the site and instructions on how to login. 
All login information is handled though Identity Framework and all methods concerning Login and user data can be found in `Controllers/AccountController.cs`.
Users can register from the home page or the `Register` tab in the nav bar. Users can also login to an existing account from the nav bar. 

### Projects 
Projects are the most abscracted layer in how files are organized. In the simplest sense documents(versions) are stored in document groups(files) are stored in projects.
All methods involving projects can be found in the `Controllers/Projects.cs` file. 

### Document Groups
A Document Group, in this context, refers to collection of documents. Think of it as a folder that contains all the versions of the files it is keeping track of. 
Document Groups are linked to projects through a ProjectDocumentGroup join table. Whenever a new Document Group is created, a new entry in the ProjectDocumentGroup join table is added. 
All methods involving DocumentGroups can be found in the `Controllers/DocumentGroups.cs` file. 

### Documents
These are the "versions" of files. When a new document is added, it is given the id of the Document Group is it attached to. The individual versions will be displayed in an accordian table underneath their respective document groups on the page. 