# Application Insights for Semantic Logging.
Main development repository for Semantic Logging integration with Application Insights.

The purpose of this project is to integrate Semantic Logging with Application Insights.


## Contributors ##
  - Fidel Esteban Morales Cifuentes
  - Luis Roberto Rosales Enriquez
  - Jose Luis Morales Ruiz
  - Jose Carlos Mendez
  - Jorge Andres Rodriguez Cuevas
  - Herberth Francisco Arriola 


Tags: Application Insights Semantic Logging Nuget

Requires: 
- Newtonsoft.Json
- EnterpriseLibrary.SemanticLogging
- Microsoft.ApplicationInsights

Tested on:
- Newtonsoft.Json (≥ 7.0.1)
- EnterpriseLibrary.SemanticLogging (≥ 2.0.1406.1)
- Microsoft.ApplicationInsights (≥ 0.17.1-beta)

License: GNU GPL 3.0

License URI: http://opensource.org/licenses/GPL-3.0

Copyright 2015 The SL Integration with Application Insights Project Developers.





## Description ##

This packages is intended for users of Semantic Login that want to seamlessly convert logs captured by SL and send them to Application Insights service on Azure. More information about Application Insights can be found <a href='http://azure.microsoft.com/en-us/documentation/articles/app-insights-get-started/'>here</a>. 
The active Semantic Logging repository by Microsoft Microsoft patterns & practices can be found <a href='https://github.com/mspnp/semantic-logging'>here</a>. 

The current version of the nuget package can be found <a href='https://www.nuget.org/packages/SLAB_AI/'>here</a>. 

## Installation ##

Using package manager console:

PM> Install-Package SLAB_AI -Pre

Using Visual Studio:
- Go to: Tools -> NuGet Package Manager -> Manage NuGet Packages
- Under Online select nuget.org
- Select Include Prerelease in the dropdown menu
- Search for: SLAB_AI
- Click Install



## Changelog ##

  1.0.0 = Initial Version
  
  1.0.1 = Minor fixes (latest version)
