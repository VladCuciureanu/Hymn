@echo off
cd ../src
set /p MigrationName=Enter migration title without spaces: 
dotnet ef migrations add Hymn_%MigrationName% --startup-project Hymn.Services.Api --project Hymn.Infra.Data --context HymnContext -v
dotnet ef migrations add EventStore_%MigrationName% --startup-project Hymn.Services.Api --project Hymn.Infra.Data --context EventStoreContext -v
dotnet ef migrations add Identity_%MigrationName% --startup-project Hymn.Services.Api --project Hymn.Infra.CrossCutting.Identity --context IdentityContext -v
pause
exit