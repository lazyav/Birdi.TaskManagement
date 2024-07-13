1. Create a database in MS SQL Server

2. Run the script in TaskManagement.sql (present in this repo).
   
3. Update the user id and password for Database Connection in appsettings.json.

4. Build the project. We should be good to go.

5. As authentication is not integrated with Swagger. The API testing can be done either removing [Authorize] attribute in the controller or using PostMan.
