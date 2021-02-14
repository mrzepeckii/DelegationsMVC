# Delegation app
> It's an application for settling employees from business trips and project management

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Features](#features)
* [Status](#status)

## General info
The application is designed to improve the operation of small and medium-sized companies. It enables the settlement of delegations by employees and budget management for a selected project. 
The application can be connected with the accounting department, thanks to which the approval and settlement of business trips can be automatic. The application allows you to create employee accounts for each employee and assign them an appropriate role. Thanks to this solution, it is possible to extend the application with new functionalities in the future.
All funcionality is available in the Features section, but the application is still being developed. 
This application is built in Clean Architecture.

## Technologies
* .NET Core 3.1
* ASP.NET, HTML, JS, MSSQL
* REST API
* Depedency Injection
* Entity Framework Core 3.1.8
* LINQ
* Fluent Validation 9.2.2
* AutoMapper 10.0.0
* XUnit
* Moq
* Fluent Assertions
* IronPDF
* Google Authentication

## Features
* Delegations managment - CRUD operations for employees
* Projects managment - CRUD operations for chief
* Creating/editing employee account - every employee has his own account with basic informations e.g. vehicles, contact details 
* Delegation flow from creating to acceptance and printing
* Delegations managment for chief and accountant - acceptance/cancel/check 
* Creating reports from delegations - create report (as pdf) and print
* Viewing ang managment employee/delegation/projects lists with DataTables
* Every user can create his account with google account authentication

## Status
Project is _in progress_ - has to create basic frontend. 

