# Application Insights for Semantic Logging.

Send data from [Semantic Logging](https://github.com/mspnp/semantic-logging) to [Application Insights](https://azure.microsoft.com/services/application-insights/) for display, analysis and diagnostics.

With [semantic logging](https://msdn.microsoft.com/library/dn440729.aspx), you call trace methods named for the event - for example, `log.GameWon(score)` instead of `log.InfoFormat("won a game, score {0}", score)`.  The [Semantic Logging Application Block](https://github.com/mspnp/semantic-logging) (SLAB) provides a useful framework on which you can create custom log methods and couple them to one or more sinks.

The purpose of this project is to integrate Semantic Logging with Application Insights. Log messages are sent to the Application Insights portal. Application Insights provides a powerful diagnostic search tool, which allows you to search and correlate associated events, including data from client, server and other components of your application. It also shows you charts of trends in performance and usage over time. By using SLAB to send the logs, you introduce an additional level of clarity to your trace code.


## Contributors ##
  - <a href='https://github.com/fidmor89'>Fidel Esteban Morales Cifuentes</a>.
  - <a href='https://github.com/chirislash'>Luis Roberto Rosales Enriquez</a>.
  - <a href='https://github.com/chepix10'>Jose Luis Morales Ruiz</a>.
  - <a href='https://github.com/josemen'>Jose Carlos Mendez</a>.
  - <a href='https://github.com/jarodriguez08'>Jorge Andres Rodriguez Cuevas</a>.
  - <a href='https://github.com/herbertharriola'>Herberth Francisco Arriola</a>.
  - <a href='https://github.com/oscargarciacolon'>Oscar Garcia Colon - Teacher and Facilitator</a>.
  

Special Thanks to 
Andrew Oakley for all his help with this project.


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

This packages is intended for users of Semantic Logging that want to seamlessly convert logs captured by SL and send them to Application Insights service on Azure. More information about Application Insights can be found <a href='http://azure.microsoft.com/documentation/articles/app-insights-get-started/'>here</a>. 
The active Semantic Logging repository by Microsoft Microsoft patterns & practices can be found <a href='https://github.com/mspnp/semantic-logging'>here</a>. 

The current version of the nuget package can be found <a href='https://www.nuget.org/packages/SLAB_AI/'>here</a>. 

## Installation ##

Using package manager console:

PM> Install-Package SLAB_AI -Pre

Or using Visual Studio:
- Go to: Tools -> NuGet Package Manager -> Manage NuGet Packages
- Under Online select nuget.org
- Select Include Prerelease in the dropdown menu
- Search for: SLAB_AI
- Click Install



## Changelog ##

  1.0.0 = Initial Version
  
  1.0.1 = Minor fixes 
  
  1.0.2 = Performance improvements 
  
  1.0.3 = Bug fixes 
  
  1.0.4 = Fix an issue when the payload value is null

  
