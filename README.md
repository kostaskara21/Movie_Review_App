# 🎬 Movie Review App (ASP.NET MVC  Project)

This is a simple **Movie Review App** built using **ASP.NET MVC**, showcasing basic **CRUD operations**, **Authentication/Role-Based Authorization**, clean code architecture using the **Repository Pattern**, **Entity Framework Core** for database management, and **Cloudinary** integration for image storage. 
It's also **test-driven**, with unit tests written using **xUnit**, **FluentAssertions** for expressive assertions, and **FakeItEasy** to mock controller logic and services. These tests check behaviors like  helping ensure controller logic behaves correctly in different scenarios. 

In this app, Users can create their own movies, upload movie images to the cloud, leave comments on other movies, edit their own profiles, and view other users along with the movies they've created. **Admins** have full control and can edit or delete any movie, while **Regular Users** can only modify their own.

---


## 🚀 Features

- 🧑‍💻 **User Authentication & Authorization**
  - Login/Register with **ASP.NET Identity**
  - Role-based authorization (**Admin** and **User**)
  
- 🎥 **Movies**
  - Users can create, view, update, and delete *their own* movies
  - Admins can manage **all** movies
  - Each movie has a description, image, and reviews

- 📝 **Reviews**
  - Users can leave reviews for any movie
  - View all reviews for each movie

- 🖼️ **Cloudinary Integration**
  - All movie images are uploaded and stored in **Cloudinary**

- 👥 **User Profiles**
  - View All users
  - Visit user profiles to see their created movies
  - Edit their information

- 🧹 **Code Structure**
  - Follows the **MVC architecture**
  - Implements **Repository Pattern** for database interactions


- 🧪 **Testing**
  - Unit tests written using **xUnit** to test logic and services

- 💾 **Database**
  - Uses **SQL Server** for data persistence

---

## 📷 Screenshots 
![Homepage Screenshot](https://res.cloudinary.com/dh7hux54e/image/upload/v1745006708/GitHub_qlyj0a.png)

---

## 🛠️ Tech Stack

- ASP.NET MVC (.NET Core)
- Razor Views
- Entity Framework Core
- ASP.NET Identity
- SQL Server
- Cloudinary (for image hosting)
- xUnit (for unit testing)

---
