# Self-Hosted WebAPIi Windows service

This is a windows service which can be installed in our local system. This windows service is started in which self hosted web api is up and running so that you can consume the api for CRUD operations.
The Self-Hosted-Web-Api-Windows-Service/SelfHostedAPI/ has the business logic in which we can manage users with the API. All the data can be loaded from the file savedonn a disk.
The Self-Hosted-Web-Api-Windows-Service/SelfHostedAPI_Test/ is a test project created under the above console application and a reference has been added to it. This has unit testing for the main methods in the application.
The Self-Hosted-Web-Api-Windows-Service/SelfHostedAPI_Service/ is a Windows Service project which is also created under the above console application and a reference has been added.

#App.Config
Some variables have been wrtten as a configurable variable through app.config. You can also make changes in the app.config file.

#API
Api have been tested using postman agent an open source tool which can be used to test each and every controller methods such as GET(), POST(), PUT(), DELETE() and etc. postman url: https://www.postman.com/

#User Object
The user Object has the following fields such as Id, firstName, lastName, emailAddress, notes, creationTime.

#Installing windows service

1. Search “Command Prompt” and run as administrator
2. Fire the below command in the command prompt and press ENTER.
    cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 
3. Now Go to the SelfHostedAPI_Service project source folder > bin > Debug and copy the full path of your Windows Service exe file.
4. Open the command prompt and fire the below command and press ENTER.
    Syntax

    InstallUtil.exe + Your copied path + \your service name + .exe
    
    
After installing the windows service, you can go the service app and search for the service name and click start to start the windows service in which it acts as a self-hosted web api.


Thanks
