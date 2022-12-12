## Azure Application Insights

#### Adicionar pacote nuget
Microsoft.ApplicationInsights.AspNetCore
Microsoft.Extensions.Logging.ApplicationInsights

#### Adicionar configuração no appsettings (Microsoft.ApplicationInsights.AspNetCore >= 2.15.0)
"ApplicationInsights": {
  "ConnectionString": "Copy connection string from Application Insights Resource Overview",
  "EnableAdaptiveSampling": false,
  "EnablePerformanceCounterCollectionModule": false
},

#### Adicionar configuração no appsettings (Microsoft.ApplicationInsights.AspNetCore < 2.15.0)
"ApplicationInsights": {
  "ConnectionString": "Copy connection string from Application Insights Resource Overview"
},