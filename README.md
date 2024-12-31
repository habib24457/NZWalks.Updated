**NZWalks (New Zone Walks)**  
New Zone Walk is an .Net Core Web API that I have been developing to build a web platform for walk enthusiast. 
End user can suggest a new walking region and set the difficulty, for other users to follow.
## Database Schema

### Walk Table
| Column Name     | Data Type | Nullable? |
|------------------|-----------|-----------|
| Id              | GUID      | No        |
| Name            | string    | No        |
| Description     | string    | No        |
| LengthInKM      | string    | No        |
| WalkImageUrl    | string    | Yes       |
| RegionId        | string    | No        |
| Difficulty      | string    | No        |

### Region Table
| Column Name     | Data Type | Nullable? |
|------------------|-----------|-----------|
| Id              | GUID      | No        |
| Code            | number    | No        |
| Name            | string    | No        |

### Difficulty Table
| Column Name     | Data Type | Nullable? |
|------------------|-----------|-----------|
| Id              | GUID      | No        |
| Level           | string    | No        |

**Relations**
Walk and Region has 1:1
Walk and Difficulty has 1:1

**Technologies Used**  

* Framework: .NET Core 7
* Entity Framework Core
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
