# HealthLoggApp (Console Version)

**HealthLoggApp** is a console-based application designed for logging daily health and wellness data. The core objective of this project is to implement robust **Object-Oriented Programming (OOP)** practices, adhere strictly to **SOLID principles**, and maintain a clean, layered **project architecture**.

---

## 🛠️ Technologies Used

* **C#** with **.NET 8**
* **Entity Framework Core**
* **SQL Server** (LocalDb or Express)
* **Clean Architecture** (Layered)
* **Console UI**

---

## 📦 Getting Started

Follow these steps to get the project up and running:

### 1. Clone the Repository

```bash
git clone <repository-url>
```

### 2. Set Up the Database

**Run database migrations**:

```bash
dotnet ef database update
```

This creates the `HealthLoggApp` database.

**Insert mock data**:

Open **SQL Server Management Studio (SSMS)** and run the provided SQL script:

```
/Solution Items/MockDataInsert.sql
```

This script populates the database with mock data, including user accounts and daily logs (Sleep, Exercise, Wellness Check-Ins, and Caffeine intake).

---

## 🔍 Project Purpose

This project serves two primary goals:

* **Logging Health Data**: Allows users to track daily variables such as sleep patterns, physical exercise, caffeine consumption, mood, and more.

* **Robust Architecture**: Emphasizes a clear, structured approach with separation of concerns and high-quality code, employing established OOP and software design principles.

---

## 🌱 Future Goals

In the future, this project is intended to evolve into a comprehensive web application, including features such as:

* User account management
* Interactive graphs and detailed analytics
* Weather and air quality integrations
* A frontend built using technologies like **Blazor** or **ASP.NET Core MVC**

---

## 🧪 Test Data

The provided mock data includes randomized daily entries for each user, covering:

* Sleep logs
* Exercise records
* Wellness Check-Ins
* Caffeine consumption

Additionally, weather and air quality data are pre-loaded into the database to enrich the testing experience.
