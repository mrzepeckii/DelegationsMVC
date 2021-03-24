# Delegation and project managemnt application
> It's an application for settling employees from business trips and project management created with ASP.NET.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Features](#features)
* [Status](#status)
* [Sreenshots](#screenshots)

## General info
The application is designed to improve the operation of small and medium-sized companies. It enables the settlement of delegations by employees and budget management for a selected project. 
The application can be connected with the accounting department, thanks to which the approval and settlement of business trips and clients/projects managment can be automatic. The application allows you to create employee accounts for each employee and assign them an appropriate role. Thanks to this solution, it is possible to extend the application with new functionalities in the future.
Most of functionalities are available in the Features section. 
This application is built in Clean Architecture and the Service-Repository pattern.

## Technologies
* .NET Core 3.1
* ASP.NET, HTML5, CSS3, JS, MSSQL
* WebAPI
* Depedency Injection
* Entity Framework Core 3.1.8
* LINQ
* Fluent Validation 9.2.2
* AutoMapper 10.0.0
* IronPDF 2020.12.3
* XUnit 2.4.0
* Moq 4.16.0
* Fluent Assertions 5.10.3
* Bootstrap 4.5.2

## Features
* Delegations managment - CRUD operations for employees
* Projects managment - CRUD operations for chief
* Creating/editing employee account - every employee has his own account with basic informations e.g. vehicles, contact details 
* Delegation flow from creating to acceptance and printing
* Delegations managment for chief and accountant - acceptance/cancel/check 
* Creating reports from delegations - create report (as pdf) and print
* Viewing and managment employee/delegation/projects lists with DataTables
* Every user can create his account with google account authentication
* Each user has a role that determines his permissions
* Viewing and managment roles assigned to users.

## Screenshots
### Landing page
![Landing page](/DelegationsMVC.Web/wwwroot/images/ScreenShots/welcome_page.PNG)
### Login page
![Login page](/DelegationsMVC.Web/wwwroot/images/ScreenShots/login_page.PNG)
### User's account data edit
![Account edit](/DelegationsMVC.Web/wwwroot/images/ScreenShots/EditEmployee_page.PNG)
### Users's delegation list
![Delegation list](/DelegationsMVC.Web/wwwroot/images/ScreenShots/delegation_list.PNG)
### Create/edit delegation
![Create delegation](/DelegationsMVC.Web/wwwroot/images/ScreenShots/delegation_create.PNG)
### Show delegation
![Show delegation](/DelegationsMVC.Web/wwwroot/images/ScreenShots/delegation_show.PNG)
### Show client and edit projects
![Show client](/DelegationsMVC.Web/wwwroot/images/ScreenShots/show_client.PNG)

### [More screenshots](https://github.com/mrzepeckii/DelegationsMVC/tree/master/DelegationsMVC.Web/wwwroot/images/ScreenShots)

## Status
Project is finished, but not closed. In the future, it is possible to expand the application in additional functionalities.
