I think you might need SSDT installed for this to work. Have not tested without it.


1. Create project (I made a unit test project just for demo purposes)
2. Install EntityFramework Nuget Package
3. Look at the Toolbar for "View"
4. View > SQL Server Object Explorer
5. A pane titled "SQL Server Object Explorer" should have appeared, for me it was on the left side of Visual Studio
6. Click the dropdown arrow next to "SQL Server" text
7. There should be a "(localdb)\MSSQLLocalDB (<a bunch of text about versioning>)"
8. Click the dropdown arrow next to the text in the previous step
9. Right Click "Databases"  > "Add new database"
10. Database Name: "DemoDatabase"
11. For "Database Location" I nvaigated to location to my project, since I want the final database file 
  to be in my projects directory.
12. Now I'm going to add a table just to show EntityFramework is working
13. Click the dropdown arrow next to "DemoDatabase"
14. Right Click "Tables" > Add new Table...
15. Add new row in the "dbo.Table [Design]" window that popped up
16. Add "person_name" in the second row, column "Name"
    - I don't care about the data type or any other specifics, just that there is another column
17. Click the "Update" text below the "dbo.Table [Design]" text
18. A window should have popped up
19. Click the "Update Database" button
20. Now the database should have been updated
21. Open the "SQL Server Object Explorer" window again
22. Right Click "DemoDatabase" > Detach
23. The DemoDatabase should have disappeared
24. However, the mdf and ldf files should still beat the designated "Database Location" you set earlier
25. For me the mdf file and ldf file are in my local project folder

The MDF file is the database that we will be connecting to with EntityFramework.

We have created a database now.
Now let's create the EntityFramework classes. 

1. Right Click your project > Add > New Item...
2. Select "Data" on the far left tree structure> ADO.NET Entity Data Model > (Optional) Change name > Add
3. Select "Code First from database" > Next > New Connection...
4. There is textbox labeled "Data Source:", on the far right of that textb ox is a "Change..." button, click it
5. Select the "Microsoft SQL Server Database File" > OK
6. There is a text box labeled "Database file name (new or existing):" , click the button labeled "Browse..." 
next to the textbox
7. Locate the "DemoDatabase.mdf" > Open
8. Click the "Test Connection" button on the far bottom left to check if you can connect to the file
9. Ideally you got a window that says "Test connection succeeded" > click ok on that window
10. Click "OK" button on the "Connection Properties" window
11. Click the "Next >" button
12. Check the box next to the "Tables" text > Finish
13. You should now have two new files adde to your project
    -Model1.cs
    -Table.cs

Now we write some code to save something in that local MDF file

```
        [TestMethod]
        public void TestMethod1()
        {
            Model1 model = new Model1();

            var tableData = model.Tables.Create();

            tableData.Id = 1;
            tableData.person_name = "name";

            model.Tables.Add(tableData);
            model.SaveChanges();
        }
```

After running that test method you should have saved data to that MDF file.

To verify data was saved:
1. Go to the menu bar and look for "Tools" > Connect to Database
2. Click the button "Change..." next to the "Data Source..."
3. Select "Microsoft SQL Server Database File" > OK
4. Click the "Browse..." button next to trhe "Database file name (nhew or exisisting):"
5.Look for the "DemoDatabase.mdf" > Open

** Visual Studio might say it is in use. I just kept trying and it eventually worked

6. You should see a pane labeled "Server Explorer"
7. Data Connections > DemoDatabase.df > Tables
8. Right Click "Table" > Show Table data
9. You should see one row saved off in the "dbo.Table [Data]" window with ID is 1 and person_name is "name"


You have now added a sql database as a file in your project. 
I am thinking of using this mostly for Unit Tests. 
That way when the project is cloned, it has the schema with whatever data is needed to start working.

Otherwise you have to set up the whole database environment on a brand new computer if someone is hired or you move.
I think having self contained projects is a lot easier for people to just work.


****

I had tons of issues when I made an mdf file from SSMS and tried using it with VS17.
Different versions of the DB since my SSMS was newer than VS17.
Apparently the server (localdb)\msSqlLocalDB is a server that is installed when VS is installed.
That way developers dont have to also install SQLExpress.

Since the localdb server is installed with VS then any db made there should work find with your project.

