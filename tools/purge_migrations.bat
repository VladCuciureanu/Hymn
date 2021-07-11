@echo off
cd ../src
dotnet ef database drop -f --startup-project Hymn.Services.Api --project Hymn.Infra.Data --context HymnContext -v
dotnet ef migrations remove --startup-project Hymn.Services.Api --project Hymn.Infra.Data --context HymnContext -v
dotnet ef migrations remove --startup-project Hymn.Services.Api --project Hymn.Infra.Data --context EventStoreContext -v
dotnet ef migrations remove --startup-project Hymn.Services.Api --project Hymn.Infra.CrossCutting.Identity --context IdentityContext -v
pause
exit