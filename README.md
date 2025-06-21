# 🏗️ dotnet-ModularMonolith-template

Welcome to the **dotnet-ModularMonolith-template** repository! This project provides a modern template for building scalable and maintainable enterprise-grade applications using .NET 9. It follows clean architecture and Domain-Driven Design (DDD) principles, ensuring a robust foundation for your software solutions.

[![Download Releases](https://img.shields.io/badge/Download%20Releases-Here-brightgreen)](https://github.com/gifted2be/dotnet-ModularMonolith-template/releases)

## 📦 Overview

The **dotnet-ModularMonolith-template** offers a comprehensive starting point for developers looking to create modular monolithic applications. This template includes essential features like:

- **Dynamic Module Loading**: Add or remove modules at runtime without downtime.
- **JWT Authentication**: Secure your API with JSON Web Tokens.
- **Rate Limiting**: Control the flow of requests to your API.
- **Logging**: Capture and analyze logs using Serilog.
- **Health Checks**: Monitor the health of your application.
- **Swagger Integration**: Automatically generate API documentation.
- **Multi-Database Support**: Work with MSSQL, PostgreSQL, and SQLite.

This template is ideal for teams that want to build applications with modular flexibility while maintaining the simplicity of a monolithic architecture.

## 📋 Features

### 1. Clean Architecture

The template follows clean architecture principles, separating concerns into layers. This structure promotes testability and maintainability. 

### 2. Domain-Driven Design (DDD)

By using DDD, the template encourages a focus on the core domain of your application. This approach helps in creating a shared understanding among team members.

### 3. Dynamic Module Loading

Easily add or remove modules as needed. This feature allows for flexibility in development and deployment, making it easier to adapt to changing requirements.

### 4. JWT Authentication

Secure your application with JWT. This method provides a stateless authentication mechanism, making it suitable for modern web applications.

### 5. Rate Limiting

Implement rate limiting to protect your API from abuse. This feature helps maintain performance and ensures fair usage.

### 6. Logging with Serilog

Capture logs with Serilog for easy analysis and debugging. The logging configuration is straightforward and customizable.

### 7. Health Checks

Monitor the health of your application with built-in health checks. This feature provides insights into the application's status and helps identify issues early.

### 8. Swagger Documentation

Automatically generate API documentation using Swagger. This feature makes it easier for developers to understand and use your API.

### 9. Multi-Database Support

Choose between MSSQL, PostgreSQL, and SQLite based on your project's needs. This flexibility allows you to work with the database technology that best suits your requirements.

## 🚀 Getting Started

To get started with the **dotnet-ModularMonolith-template**, follow these steps:

### Prerequisites

Ensure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A suitable IDE (Visual Studio, Visual Studio Code, etc.)
- Docker (for containerization)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/gifted2be/dotnet-ModularMonolith-template.git
   cd dotnet-ModularMonolith-template
   ```

2. Restore the dependencies:

   ```bash
   dotnet restore
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

### Configuration

Configure your application by editing the `appsettings.json` file. This file contains settings for JWT, database connections, logging, and more.

### Running Tests

To run the tests, use the following command:

```bash
dotnet test
```

## 🛠️ Usage

Once the application is running, you can access the API at `http://localhost:5000`. You can explore the API documentation at `http://localhost:5000/swagger`.

### Example API Calls

Here are some example API calls you can make:

- **Authenticate User**:

   ```http
   POST /api/auth/login
   ```

- **Get Health Status**:

   ```http
   GET /health
   ```

- **List Modules**:

   ```http
   GET /api/modules
   ```

## 📊 Monitoring

Use the health check endpoint to monitor the application's health. The endpoint provides information about the status of various components.

## 🔒 Security

The template uses JWT for authentication. Make sure to configure your JWT settings in the `appsettings.json` file. 

### Example JWT Configuration

```json
"Jwt": {
  "Key": "your_secret_key",
  "Issuer": "your_issuer",
  "Audience": "your_audience",
  "ExpireMinutes": 60
}
```

## 📄 Documentation

Refer to the [Swagger documentation](http://localhost:5000/swagger) for detailed API usage instructions.

## 📈 Logging

The template uses Serilog for logging. Configure your logging settings in the `appsettings.json` file.

### Example Logging Configuration

```json
"Serilog": {
  "Using": [ "Serilog.Sinks.Console" ],
  "MinimumLevel": "Information",
  "WriteTo": [
    {
      "Name": "Console"
    }
  ]
}
```

## 🐳 Docker Support

This template includes Docker support. You can build and run the application in a container.

### Building the Docker Image

To build the Docker image, run:

```bash
docker build -t dotnet-modular-monolith .
```

### Running the Docker Container

To run the Docker container, use:

```bash
docker run -d -p 5000:80 dotnet-modular-monolith
```

## 🔗 Links

- [Releases](https://github.com/gifted2be/dotnet-ModularMonolith-template/releases)
- [Documentation](http://localhost:5000/swagger)

## 🗂️ Topics

This repository covers various topics relevant to modern application development:

- api-template
- aspnetcore
- clean-architecture
- csharp
- ddd
- docker
- domain-driven-design
- dotnet
- dotnet9
- health-check
- jwt-authentication
- modular-architecture
- modular-monolith
- mssql
- postgresql
- rate-limiting
- serilog
- sqlite
- swagger

## 🎉 Contributing

We welcome contributions! If you want to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them.
4. Push your changes to your forked repository.
5. Create a pull request.

## 📜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## 📬 Contact

For any inquiries or issues, feel free to reach out through the repository's issues section or contact the maintainer.

## 📅 Roadmap

We plan to add more features and improvements in the future. Some of the planned enhancements include:

- Improved testing strategies.
- More comprehensive documentation.
- Additional integrations with third-party services.

## 🔄 Changelog

Check the [Releases](https://github.com/gifted2be/dotnet-ModularMonolith-template/releases) section for updates and changes made to the project.

Thank you for checking out the **dotnet-ModularMonolith-template**! We hope it serves as a solid foundation for your next project.