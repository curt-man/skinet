# SkiNet E-Commerce Project (Udemy Course Follow-Along)

This repository is a follow-along project for the Udemy course: [Build an E-Commerce App with .NET Core and Angular](https://www.udemy.com/course/learn-to-build-an-e-commerce-app-with-net-core-and-angular). The course instructor is the original creator of this project, and I'm currently on Chapter 14 of the course, learning and practicing along the way.

> **Important:** This is not an original project of mine. I am following the course to learn .NET 8 and Angular 18, and this repository contains code and instructions provided by the course instructor.

## Project Overview

SkiNet is an e-commerce platform built using:
- **Backend:** .NET 8
- **Frontend:** Angular 18

### Features (as of Chapter 14)
1. **User Authentication and Authorization:** Secure login system with JWT-based authentication.
2. **Product Catalog:** Product browsing with features such as sorting, filtering, and pagination.
3. **Shopping Cart:** Add, remove, and modify items in the cart.
4. **Database Integration:** Uses SQL Server for storing data and Redis for caching.
5. **Docker Support:** Easily set up and run the app with Docker.

As I progress through the course, I will implement additional features, such as payment handling, order management, product reviews, and advanced filtering mechanisms.

---

## How to Run the Project Locally

To run the project locally, follow the steps provided below. Note that these instructions are directly taken from the course, and any configurations or setup is part of the learning experience.

### Prerequisites
You need the following tools installed on your machine:
1. Docker
2. .NET SDK v8
3. NodeJS (at least v20.11.1)
4. [Git](https://git-scm.com)

### Steps

1. Clone the repository:
    ```bash
    git clone https://github.com/TryCatchLearn/skinet-2024.git
    cd skinet-2024
    ```

2. Restore the .NET and npm packages:
    ```bash
    dotnet restore
    cd client
    npm install
    ```

3. Configure Stripe settings for payment processing (optional):
   - Add your Stripe API keys to a new file `appsettings.json` inside the `API` folder (see the original repo for more details).

4. Start the SQL Server and Redis containers:
    ```bash
    docker compose up -d
    ```

5. Run the API:
    ```bash
    cd API
    dotnet run
    ```

6. (Optional) Run the Angular client app:
    ```bash
    ng serve
    ```

7. Visit the app locally:
   - API: https://localhost:5001
   - Angular Client: https://localhost:4200 (for development mode)
---

## Progress and Contributions

Since this is a course-based project, contributions are not expected, as I'm solely focused on learning and replicating the instructor's code. However, feel free to follow along if you're taking the same course!
