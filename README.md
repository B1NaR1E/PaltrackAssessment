# Paltrack Assessment

## Angular Demo App Spec

Angular Front End project with a .Net Core API Backend

- Angular App should have a Login page.
- Validate username.  Should be a valid email address.
- Validate Password. Should be at least 6 chars, with at least one Uppercase, Lowercase, Number and special character.
- Does not need to validate against a database. Just testing front end validation checks
- After login, display a DevExtreme grid component ([Angular Example](https://js.devexpress.com/Angular/Demos/WidgetsGallery/Demo/DataGrid/Overview/Light/)) with 5 columns and 10 rows of random data. Use https://generatedata.com/ to assist with creating some test data.

# Developer Notes

## Backend API
For the API I kept it simple and didn't want to overengineer the solution. I kept it as a monolith with no extra layers. I implemented
authentication using JWT token Bearer schema. Once logged in the generated token is stored in local storage and used to authorize the user to retrieve the data that is displayed in the Data Grid. 

## Angular Web App
I haven't touched Angular in a long time and had to brush up on the technology. Please excuse the way the app visually looks.
As no styling requirements were specified in the assessment spec, it is very plain and simple.

The link provided for the Angular Example of the DevExtreme Grid did not want to display and just kept loading
no matter how long I waited, so I went off of the [Getting Started With Data Grids](https://js.devexpress.com/Angular/Documentation/Guide/UI_Components/DataGrid/Getting_Started_with_DataGrid/) tutorial. 

The assessment spec mentioned nothing regarding
pagination, for that reason I did not add any pagination elements or functionality to the Data Grid.

## NB!!
Once the migrations for the identity database context is done running, I register a default user with the following details:
- **Username:** admin@admin.com
- **Password:** Admin@1811