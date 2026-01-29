# 🍔 FoodieDash - Modern Food Ordering System

![.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

> **FoodieDash** is a full-stack web application designed to streamline the online food ordering process. Built with robustness and scalability in mind, it bridges the gap between culinary businesses and hungry customers through an intuitive, responsive interface.

---

## 📑 Table of Contents
- [📖 Overview](#-overview)
- [✨ Key Features](#-key-features)
- [🏗️ Tech Stack & Architecture](#-tech-stack--architecture)
- [💻 User Roles](#-user-roles)
- [🚀 Getting Started](#-getting-started)
- [📸 Application Previews](#-application-previews)
- [🔮 Future Roadmap](#-future-roadmap)
- [📬 Contact](#-contact)

---

## 📖 Overview

**FoodieDash** solves the problem of complex food ordering by offering a simplified, visually appealing platform. It leverages the **Model-View-Controller (MVC)** architectural pattern to ensure a clean separation of concerns, making the codebase easy to maintain and scale.

The application allows users to browse diverse food categories, manage a dynamic shopping cart, and securely authenticate. For administrators, it provides a dedicated dashboard to manage the catalog (Menu Items and Categories) without touching the database directly.

---

## ✨ Key Features

### 🎨 User Experience (Frontend)
* **Responsive Design:** Fully optimized for Desktop, Tablet, and Mobile using Bootstrap 5 grid systems.
* **Dynamic UI Interactions:** Custom CSS animations, sliding navigation underlines, and interactive buttons.
* **Real-time Cart Updates:** The navigation bar updates the cart count instantly as items are added.
* **Rich Menu Browsing:** Users can filter items by categories and view detailed descriptions and prices.

### ⚙️ Backend Logic
* **Secure Authentication:** Implements **ASP.NET Core Identity** for secure User Registration, Login, and Logout processes.
* **Role-Based Authorization:** Custom logic ensures that sensitive Admin areas are completely inaccessible to standard users.
* **Data Integrity:** Uses **Entity Framework Core** for efficient database transactions and data consistency.

---

## 🏗️ Tech Stack & Architecture

This project is built on the Microsoft technology stack, ensuring high performance and security.

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Framework** | ASP.NET Core MVC 6.0+ | The core framework for handling HTTP requests and routing. |
| **Language** | C# | Used for all backend logic and controller actions. |
| **Frontend** | Razor Views (cshtml), Bootstrap 5 | Used for rendering dynamic HTML and responsive styling. |
| **Database** | SQL Server | Relational database for storing Users, Orders, and Menu Data. |
| **ORM** | Entity Framework Core | Handles object-relational mapping (Code-First approach). |
| **Icons** | Bootstrap Icons | Provides modern, vector-based iconography. |

---

## 💻 User Roles

The application features distinct interfaces based on the logged-in user:

### 1. Guest User
* View Home Page.
* Browse Menu.
* Register/Sign Up.
* Log In.

### 2. Registered Customer
* **All Guest features.**
* Add items to Cart.
* View Cart Summary.
* Access Profile (Logout).

### 3. Administrator (Super User)
* **All Customer features.**
* **Category Management:** Create, Edit, or Delete food categories (e.g., Pizza, Burgers).
* **Menu Management:** Add new dishes, set prices, and upload details.
* *Note: Admin access is currently restricted to the username "Afroz".*

---

## 🔮 Future Roadmap

* [ ] **Payment Gateway Integration:** Stripe or PayPal for real transactions.
* [ ] **Order History:** Allow users to view past orders.
* [ ] **Admin Dashboard:** Graphical charts for sales analysis.
* [ ] **Email Notifications:** Send receipts upon checkout.

---

## 📬 Contact

**Afroz** - Full Stack Developer

* **GitHub:** [github.com/yourusername](https://github.com/AFROZ81)
* **Email:** [alamamaan308@gmail.com]

---
*Made with ❤️ and C#*
