# 🏋️‍♂️ Calisthenix - Calisthenics Workout Tracker

Calisthenix is a full-stack web application for exploring, tracking, and managing calisthenics workouts and exercises.
Built with **ASP.NET Core 8 (Web API)** and a **React frontend**, it enables users to register, log workouts, add exercises, and interact through comments.

---

## 🚀 Technologies Used

### Backend
- ASP.NET Core 8 (Web API)
- Entity Framework Core
- Microsoft SQL Server
- JWT Authentication
- ASP.NET Identity
- xUnit + Moq (Unit Testing)

### Frontend
- React + Vite
- React Router DOM
- CSS Modules
- Framer Motion (animations)
- Responsive Design

---

## 🧠 Key Features

✅ Register/Login with JWT Authentication  
✅ Role-based Authorization (User / Admin)  
✅ Browse all exercises with filters + pagination  
✅ View detailed exercise info  
✅ Create workouts and assign exercises to them  
✅ Add, edit, and delete your own exercises  
✅ Comment system with thumbs-up reactions  
✅ Edit and delete your comments  
✅ Responsive and modern UI  
✅ Secure backend with input validation, exception handling, and XSS/CSRF protection  
✅ 65%+ Unit Test Coverage for all core logic  
✅ GitHub version-controlled (5+ days, 20+ commits)

---

## 🧪 Unit Testing

- All service methods tested (auth, workout, exercise, comment)
- In-memory database used for testing
- Exception paths and edge cases covered


---

## 🔐 Security Features

- ✅ SQL Injection prevention via EF Core
- ✅ XSS protection (React escapes by default)
- ✅ Stateless architecture with JWT (no CSRF issues)
- ✅ Parameter validation on all endpoints
- ✅ Escaped user content on render

---

## 📚 SoftUni Project Requirements Checklist

| Requirement | Status |
|-------------|--------|
| ASP.NET Core 6/8 | ✅ |
| Microsoft SQL Server | ✅ |
| 5+ Entity Models | ✅ |
| 5+ Controllers | ✅ |
| 10+ UI Pages | ✅ |
| Responsive Design | ✅ |
| User Roles (User/Admin) | ✅ |
| REST API + React Frontend | ✅ |
| Pagination | ✅ |
| Filtering/Search | ✅ |
| Error Handling | ✅ |
| Custom 404 / 500 pages | ✅ (Handled in frontend) |
| Dependency Injection | ✅ |
| Unit Testing (65%+) | ✅ |
| GitHub: 5+ commit days / 20+ commits | ✅ |

---

## 🏆 Bonus Features

✨ Comment system with thumbs-up reactions  
✨ React frontend using Vite  
✨ JWT Authentication from scratch  
✨ Animated transitions with Framer Motion  
✨ Lazy-loaded images  
✨ Instant UI updates without reloading  

---

## 🧪 How to Run Locally

### Backend (ASP.NET Core)
```bash
cd Calisthenix.Server
dotnet build
dotnet run
