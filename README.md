# Task Management App (.NET 9 + Blazor WASM)

A simple task manager built with **.NET 9.0**, **Blazor WebAssembly**, and **ASP.NET Core Web API** using **EF Core + SQLite**.  

## Features
- Add, edit, delete tasks
- Toggle completion status

## Run locally
```bash
git clone https://github.com/<your-username>/<repo>.git
cd TaskManagement/TaskManagement.Server

# Apply database migrations
dotnet ef database update --project "..\TaskManagement.SharedData\TaskManagement.SharedData.csproj" --startup-project ".\TaskManagement.Server.csproj"

# Run API + Blazor UI
dotnet run

## License
This project is licensed under the [MIT License](LICENSE).
