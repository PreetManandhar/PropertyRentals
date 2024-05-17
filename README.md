# PropertyRentals
1.	Project Description
Case Study:
 Property Rental Management Web Site Project 
Description 
Property Rental Management Web Site allows any potential tenant to search an apartment that is suitable for his/her needs. The Web site also helps the property owner and the property manager to facilitate the management tasks of property rental. 

![image](https://github.com/PreetManandhar/PropertyRentals/assets/145702104/697b5b59-8686-43da-a8c1-06b4b15d4ee0)

2.	Project Development
Phase 1 Analysis
Users and Operations:

i.	Property Owner ,Administrator  
Create/Update/Delete/Search/List any property manager account  Update/Delete/Search/List any potential tenant account  
Full control of the Web Site 

ii.	Property Manager  
Perform CRUD operations related to properties 
 Perform CRUD operations related to apartments  
Keep track of apartments status  
Schedule potential tenants ‘appointments  
Respond to potential tenants ‘messages  
Report any event to the property owner when necessary 

iii.	Potential Tenants  
Create an on-line account through Property Rental Management Web Site  
View any apartment suitable for their needs  
Make an appointment with the property manager  
Send necessary messages to the property manager 

Technologies used for Client Side 
CSS3, HTML5 and JavaScript 



Technologies used for Server Side 
You can choose one of the following options: 
1. ASP.Net Core MVC (Recommended)  
• ASP.NET Core - Development Environment Setup 
• Creating an ASP.Net Core Project 
• Structure of an ASP.Net Core Project 
• ASP.Net Core – Dependency Injection
 • ASP.Net Core – Middleware 
• Developing ASP.Net Core MVC 
• Using Entity Framework Core (DB First)


Database :
Users Pk id 
Properties  pk PropertyId  fk OwnerId, ManagerId
Apartments pk ApartmentId fk PropertyId
Appointments pk AppointmentId fk ManagerId TenantId ApartmentId
Messages pk MessageId fk SenderId ReceiverId AppointmentId
Status pk StatusId fk ManagerId, ApartmentId


Steps I followed for project:

step1 
Install packages:
1. Framework
2. SqlServer
3. Tools
4. Relational

step2
Package manager console

PM> Scaffold-DbContext "Server=PREET-PC\SQLEXPRESS2022;Initial Catalog=RentalDB3;User=sa;Password=lasalle;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models 

step3
copy string from dbcontext and paste in appsettings.json

Modified
"{ 
Logging": { 
"LogLevel": { 
"Default": "Information", 
"Microsoft": "Warning", 
"Microsoft.Hosting.Lifetime": "Information" 
} 
}, 
"AllowedHosts": "*", 
"ConnectionStrings": { 
"RentalDB3": "Server=PREET-PC\\SQLEXPRESS2022;Initial Catalog=RentalDB3;User=sa;Password=lasalle;Integrated Security=True;TrustServerCertificate=True;"
} 
}

step 4
craete controllers and view

step 5
change program and create authentications.






Phase 3 Testing and Screen shots
Users - Owner, Manager, Tenant

Home page 
 

When I search for range of rent price, it gives me list of apartments within that range 
 

Anyone can see List of Properties and Apartments when clicked on navigation bar .
1.Page Property
 
2.Page Apartments
 
For security, I used sessions as in Login, Register and Logout. When I click on logout, it takes me back to login page.
 
Register 
 

My website has 3 users. 
1.	Owner		2. Manager 		3. Tenant
When I login with their credentials, it send user to their respective dashboard with operations they can do.
1.	As an owner:
  

Owner can perform CRUD for user management.
 



Owner can perform CRUD for Property management.
 
Owner can perform CRUD for Property management.
 

Overall, owner has full control of their website.(Accomplished)



2.	As a manager 
 
a.	CRUD for Property
 


b.	CRUD for Apartments
 
c.	Tracking of apartment status
 







d.	Schedule appointment with tenant
 
e.	Respond to messages
 











3.	As a tenant 
 









a.	Can only view list of apartments
 
b.	Make appointments with manager
 

c.	Send message to manager
 



3.	Conclusion
   
a.	Achievemnents
Following are the points that I learned from course:
	MVC Architecture: Assess understanding and implementation of the Model-View-Controller pattern.
	Routing and Middleware: Evaluate effectiveness of routing and middleware in request handling.
	Model Binding and Validation: Reflect on the ease of binding data to models and implementing validation.
	View Rendering: Assess efficiency and flexibility of view rendering using Razor syntax.
	Controller Actions and Filters: Evaluate organization and usage of controller actions and filters.
	Client-Side Scripting Integration: Assess integration with client-side scripting frameworks/libraries (if applicable).
	Testing: Evaluate the ease of writing and executing unit tests for controllers and views.







4.	Bibliography
	YouTube
	Google (https://learn.microsoft.com/en-us/aspnet/core/mvc/overview?view=aspnetcore-8.0)

