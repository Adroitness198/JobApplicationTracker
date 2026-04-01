[README.md](https://github.com/user-attachments/files/26420438/README.md)
# 📋 Job Application Tracker

A full-stack web application that helps job seekers stay organized by tracking their job applications in one place.

🔗 **Live Demo:** [https://jobtracker-adroit-dag2hzekefbpcfbq.southafricanorth-01.azurewebsites.net](https://jobtracker-adroit-dag2hzekefbpcfbq.southafricanorth-01.azurewebsites.net)

---

## ✨ Features

- 🔐 **User Authentication** — Register and login securely with ASP.NET Core Identity
- ➕ **CRUD Operations** — Create, view, edit and delete job applications
- 🔍 **Search & Filter** — Search by company name and filter by application status
- 📄 **Pagination** — Browse applications with paginated results
- 📊 **Dashboard** — Visual overview of your application statuses
- 📎 **Resume Upload** — Attach a resume to each application
- 📅 **Interview Date Tracking** — Log and track upcoming interview dates

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core MVC (.NET 8) |
| ORM | Entity Framework Core |
| Database | Azure SQL Database |
| Auth | ASP.NET Core Identity |
| Frontend | Bootstrap 5, Razor Views |
| Hosting | Azure App Service |
| Pagination | X.PagedList |

---

## 🚀 Getting Started (Local Setup)

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Azure SQL Database
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/Adroitness198/JobApplicationTracker.git
   cd JobApplicationTracker
   ```

2. **Set up your connection string**

   Update `appsettings.json` with your database connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=JobTrackerDB;..."
   }
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the app**
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001`

---

## 📸 Screenshots


---

## 👤 Author

**Sinethemba Mbatha**
- GitHub: [@Adroitness198](https://github.com/Adroitness198)

---

## 📄 License

This project is open source and available under the [MIT License](LICENSE).
