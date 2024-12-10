**NZWalks (New Zone Walks)**  

NZWalks is a .NET Core Web API that helps users discover and suggest new walking zones. The project currently provides a backend API for managing walking zone data, with plans to add a user interface in the near future. Whether you're looking for a new walking path or want to share one with others, NZWalks makes it easier to explore and contribute to walking zones.

**Features**  

* Discover New Zones: Search for walking zones based on user suggestions (API endpoint).
* Contribute Suggestions: Add your own walking zones to the database for others to explore (API endpoint).
* Database Integration: All data is stored in a lightweight SQLite database.
* Robust Unit Testing: Ensures API reliability with comprehensive xUnit tests.  

**Technologies Used**  

* Framework: .NET Core 7
* Database: SQLite
* Testing Framework: xUnit  

**Installation**  

To get started with NZWalks, follow these steps:

1. Clone the repository:

`bash
Copy code
git clone https://github.com/your-username/NZWalks.git
cd NZWalks`
2. `Restore dependencies:
bash
Copy code
dotnet restore`  
3. Build the application:

`bash
Copy code
dotnet build
Run database migrations:`

`bash
Copy code
dotnet ef database update`  

4. Run the application:
`bash
Copy code
dotnet run`  
5. Run unit tests (Optional but recommended):


`bash
Copy code
dotnet test`  

6. Usage 
Access the API at http://localhost:5000 (or your configured port).
