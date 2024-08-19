# Ops Manager - Software Requirement Specification (SRS) Implementation

## Introduction

Ops Manager is an advanced application system designed to streamline and optimize the operations of Power Distribution Companies. The system supports various critical functions including electricity billing, meter reading, enumeration, customer management, asset management, and more. By incorporating automation and real-time data access, Ops Manager aims to increase efficiency, accuracy, and customer satisfaction.

## Purpose

The primary goal of Ops Manager is to enhance the efficiency, accuracy, and speed of power companies' operations, ensuring maximum throughput with minimal errors.

## Scope

The scope of Ops Manager includes the following functionalities:
1. Bill Distribution
2. Evaluation
3. Asset Management (e.g., DSS, DTs)
4. Enumeration
5. Complaint Management
6. Performance Monitoring
7. User Management
8. Disconnection Management
9. Reconnection Management
10. Automated Billing
11. Automated Meter Reading
12. Manual Meter Reading
13. Data Validation
14. Customer Management
15. Tariff Management
16. Online Payment Processing
17. Notifications/Alerts
18. Analytics
19. Customer Support
20. Security Management
21. Real-time Data Updates
22. Data Export/Reporting

## Modules

### Authentication Module
This module provides secure access to the system's functionalities. Only authenticated users can access the application.

### Onboarding Module
Admins can onboard new users and assign roles through this module.

### Meter Reading Module
This module allows users to capture meter readings either automatically or manually. Supervisors approve or reject the readings before they are used for billing.

### Billing Module
Generates customer bills based on meter readings and sends them via various channels.

### Bill Distribution Module
Logs and confirms the distribution of bills to customers, with electronic distribution options available.

### Evaluation Module
Evaluates customer details for accuracy, ensuring correct tariff assignment and revenue protection.

### Asset Management Module
Tracks and manages company assets, including maintenance schedules and operational status.

### Enumeration Module
Adds new customers to the system and regularizes existing customer information.

### Complaint Management Module
Manages customer complaints and assigns them to appropriate personnel for resolution.

### Performance Module
Monitors and evaluates staff performance based on task completion and efficiency.

### User Management Module
Admins can manage user accounts, including adding, updating, or deleting users.

### Disconnection Module
Manages the disconnection of defaulting customers based on set conditions.

### Reconnection Module
Manages the reconnection of customers who have met the conditions for reconnection.

### Customer Management Module
Manages customer details, billing history, tariff plans, and more.

### Tariff Management Module
Allows configuration and management of different tariffs and billing plans.

### Online Payment Module
Facilitates customer payments through various online channels.

### Notification/Alert Module
Sends automated notifications to customers about bills, payments, and other relevant information.

### Analytics Module
Provides detailed reports on consumption, payments, and other key metrics.

### Customer Support Module
Manages customer support tickets and assigns them to appropriate users for resolution.

## Proposed Solution

The proposed solution includes a web application, a mobile application, and API integration. The mobile app is used for capturing manual meter readings, while the web application handles billing, reporting, customer management, tariff management, and customer support. Notifications are handled by a server-side service.

## Requirements

### Functional Requirements

#### Web Application
- **Authentication**: Secure login and logout functionality.
- **Dashboard**: Summary of activities across various modules.
- **Billing**: Initiate and send customer bills.
- **Bill History**: View and filter billing history; download results.
- **Meter Reading**: View, approve/reject readings; filter and download data.
- **Enumeration**: View and filter enumeration reports; download data.
- **Evaluation**: View and filter evaluation reports; download data.
- **Asset Management**: Update asset information and manage assets.
- **Complaint Management**: Monitor and assign complaints for resolution.
- **Disconnection**: Manage disconnection lists based on set conditions.
- **Reconnection**: Manage reconnection lists based on set conditions.
- **Performance Monitoring**: Track and evaluate staff performance; download reports.
- **User Management**: Add, update, delete, or manage user accounts.
- **Customer Management**: View and manage customer details; filter and download data.
- **Tariff Management**: View, add, update, delete tariff details.
- **Analytics**: View and download detailed reports.
- **Notifications**: Send notifications to customers.
- **Customer Support**: Manage and assign support tickets.

#### Mobile Application
- **Authentication**: Secure login and logout functionality.
- **Meter Reading**: Capture and submit meter readings with photos.
- **Bill Distribution**: Log and confirm bill distribution.
- **Evaluation**: Perform customer evaluations.
- **Enumeration**: Perform enumeration for new and existing customers.
- **Complaint Management**: Log complaints about services or equipment.
- **Disconnection**: Create disconnection tickets.
- **Reconnection**: Create reconnection tickets.
- **Profile Management**: View and manage user profiles.

### Non-Functional Requirements
- **Security**: Protect APIs and ensure unauthorized access is prevented.
- **Availability**: Ensure the application is accessible 24/7.
- **User Interface**: Provide a simple, clean, and user-friendly interface.

## Installation

### Prerequisites
- **.NET SDK**: Latest version installed.
- **MySQL**: Database setup with appropriate credentials.
- **Visual Studio**: Preferred IDE for running the application.

### Setup Instructions
1. **Clone the Repository**:
   ```bash
   git https://github.com/techmonarch/Ops-Manager-New-Backend.git
