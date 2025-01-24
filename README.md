# CollegeStudentXtramile Project

This project consists of two main parts: the backend server and the frontend client. The backend is built using .NET 8, and the frontend is built using Vue 3 with Vite.

## Project Structure

- `CollegeStudentXtramile.Server`: The backend server project using ASP.NET Core Web API.
- `collegestudentxtramile.client`: The frontend client project using Vue 3.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (which includes npm)
- [Visual Studio](https://visualstudio.microsoft.com/) (recommended for backend development)
- [VSCode](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (recommended for frontend development)
- [PostgreSQL](https://www.postgresql.org/download/) (for database setup)

## Setup Instructions

### Backend (Server)

1. Navigate to the backend project directory:
2. Restore the .NET dependencies:
3. Build the project:
4. Set up the PostgreSQL database:
   - Ensure PostgreSQL is installed and running.
   - Update the connection string in `appsettings.json` to point to your PostgreSQL instance.
   - Apply the migrations to set up the database schema:
5. Run the project

### Frontend (Client)

1. Navigate to the frontend project directory:
2. Install the npm dependencies:
3. Compile and hot-reload for development

## Running the Full Application

1. Start the backend server by following the backend setup instructions.
2. Start the frontend client by following the frontend setup instructions.

## Running the Application in Visual Studio

1. Open the solution file in Visual Studio.
2. Set the startup projects:
   - Right-click on the solution in Solution Explorer and select "Set Startup Projects...".
   - In the dialog, select "Multiple startup projects".
   - Set the action for `CollegeStudentXtramile.Server` to "Start".
   - Set the action for `collegestudentxtramile.client` to "Start".
3. Click the Start button (or press F5) to run both the backend and frontend projects.

## Additional Information

- For detailed configuration of the frontend, see the [Vite Configuration Reference](https://vite.dev/config/).
- For detailed configuration of the backend, refer to the .NET documentation.

This README is intended to help you set up and run the project for the interview application. If you have any questions, please feel free to reach out.
