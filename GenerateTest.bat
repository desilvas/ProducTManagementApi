mkdir .coverage

dotnet test ProductManagement.Domain.Service.Test /p:Threshold=100 /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="../.coverage/Data.opencover.xml"
reportgenerator "-reports:.coverage/Data.opencover.xml" "-targetdir:.coverage-report" -reporttypes:HTML;

PAUSE

