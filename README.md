# Compact Store API Samples

## Overview

Sample server and client created to help understanding the Compact Store API (CS API). .NET Core is used.

## ImportOrderClientSample

The sample provides a Windows Forms application for importing orders and items.
The main logic for communicating with CS API is implemented in class Weland.Cs.Api.Sample.ImportClient.Services.ImportService

## ExportOrderServerSample

The sample provides a ASP.NET API application for receiving export orders. The application can be hosted in IIS or as a stand alone application running Kestrel.
The main server logic is implemented in class namespace Weland.Cs.Api.Sample.ExportOrderServer.Controllers.ExportOrdersController
