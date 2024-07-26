# PennStateSoft
A meeting scheduling system.

### Getting Started
Type **Update-Database -context ApplicationDbContext** in your NuGet Package Manager console in visual studio.

Also type **Update-Database -context UserComplaints** and **Update-Database -context ComplaintReplies**.

Do the same for additional database contexts such as MeetingContext. 

Each additional context will need a migration.

Type **Add-Migration <"YourMigrationName"> -context <"YourContextName">** in your Nuget Package Manager console 
and proceed to update the database with your new context.
