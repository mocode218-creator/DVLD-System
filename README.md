# 🚗 DVLD – Driving & Vehicle License Department Management System

<div align="center">

![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-WinForms-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2016%2B-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Architecture](https://img.shields.io/badge/Architecture-3--Tier-orange?style=for-the-badge)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

**A desktop application that simulates the full workflow of a driving license department: applications, tests, issuance, renewal, replacement, detention, and international licenses.**

Built with C# WinForms, ADO.NET, and SQL Server using a clean 3-Tier Architecture.

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Features](#-features)
- [Architecture](#-architecture)
- [Tech Stack](#-tech-stack)
- [Database Schema](#-database-schema)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [Usage](#-usage)
- [Screenshots](#-screenshots)
- [Core Business Logic](#-core-business-logic)
- [Roadmap](#-roadmap)
- [License](#-license)
- [Author](#-author)

---

## 🎯 Overview

**DVLD** manages the complete lifecycle of a driving license, from the first application through vision, written, and street tests, to issuance, renewal, replacement, and detention.

| Layer | Responsibility | Technology |
|-------|----------------|------------|
| **Presentation** | Forms and user controls | WinForms |
| **Business Logic** | Entities, rules, validation | C# classes |
| **Data Access** | CRUD and queries | ADO.NET + SQL Server |

---

## ✨ Features

### 👥 People Management
- Full CRUD on people records
- Photo upload with GUID-based file naming
- Filtering by name, national number, gender, nationality, phone, and email
- Validation of national number uniqueness and email format

### 🔐 User Management
- Login with an optional "Remember Me"
- Add, update, delete, and activate/deactivate users
- Change password with current-password verification
- Link users to existing people

### 📄 Application Management
- 7 application types with configurable fees
- Status tracking: New / Cancelled / Completed
- Prevents duplicate active applications for the same license class

### 🚦 Local Driving License Workflow

`New Application` → `Vision Test` → `Written Test` → `Street Test` → `Issue License`

- Schedule, edit, and retake tests with automatic retake fees
- Enforces test order (Written requires Vision, Street requires Written)
- Prevents re-taking passed tests
- Locks appointments once a test is taken

### 🌍 International License
- Issued to drivers holding an active Local License **Class 3**
- Prevents duplicate active international licenses
- 1-year validity; the previous license is deactivated on renewal

### 🔄 License Services
- **Renew** expired licenses (validity based on license class)
- **Replace** lost or damaged licenses
- **Detain** licenses with a fine, and **Release** them with application tracking
- Full license history (Local + International)

### 🧪 Tests & 👨‍✈️ Drivers
- Manage test types and fees; track trials per application
- Filter appointments by type and status
- Browse drivers, view their license history and personal details

### 📊 Smart Data Grids
- Dynamic column filtering on every list (ID, name, Yes/No/All)
- Live record count

---

## 🏗️ Architecture

```text
┌──────────────────────────────────────────────────────────┐
│ PRESENTATION LAYER (WinForms)                            │
│ Forms (frmXxx) + User Controls (ctrlXxx)                 │
│ • Session via clsGlobal.CurrentUser                      │
│ • Helpers: clsFormat, clsValidatoin, util                │
└──────────────────────────────────────────────────────────┘
                            ↓
┌──────────────────────────────────────────────────────────┐
│ BUSINESS LOGIC LAYER (C#)                                │
│ clsPerson, clsUser, clsLicense, clsTest, ...             │
│ • Inheritance: clsApplication                            │
│     ├─ clsLocalDrivingLicenseApplication                 │
│     └─ clsInternationalLicense                           │
│ • enMode pattern (AddNew / Update)                       │
│ • Navigation + computed properties, business rules       │
└──────────────────────────────────────────────────────────┘
                            ↓
┌──────────────────────────────────────────────────────────┐
│ DATA ACCESS LAYER (ADO.NET)                              │
│ Static clsXxxData classes                                │
│ • SqlConnection / SqlCommand / SqlDataReader             │
│ • Connection via clsDataAccessSettings.ConnectionString  │
└──────────────────────────────────────────────────────────┘
                            ↓
                     SQL Server Database
```

---

## 🛠️ Tech Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| C# | 7.3+ | Language |
| .NET Framework | 4.8 | Runtime |
| WinForms | – | UI |
| ADO.NET | – | Data access |
| SQL Server | 2016+ (Express is fine) | Database |
| Visual Studio | 2019+ | IDE |

---

## 🗄️ Database Schema

<details>
<summary><b>15 Tables</b> (click to expand)</summary>

| Table | Key Columns |
|-------|-------------|
| `Countries` | CountryID, CountryName |
| `People` | PersonID, FirstName, SecondName, ThirdName, LastName, NationalNo, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath |
| `Users` | UserID, PersonID, UserName, Password, IsActive |
| `Drivers` | DriverID, PersonID, CreatedByUserID, CreatedDate |
| `ApplicationTypes` | ApplicationTypeID, ApplicationTypeTitle, ApplicationFees |
| `Applications` | ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID |
| `LicenseClasses` | LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees |
| `LocalDrivingLicenseApplications` | LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID |
| `Licenses` | LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID |
| `InternationalLicenses` | InternationalLicenseID, ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID |
| `DetainedLicenses` | DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID |
| `TestTypes` | TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees |
| `TestAppointments` | TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID |
| `Tests` | TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID |

</details>

<details>
<summary><b>5 Views</b> (click to expand)</summary>

| View | Combines |
|------|----------|
| `Drivers_View` | Drivers + People + active licenses count |
| `ApplicationsList_View` | Applications + Person + Type + Status |
| `LocalDrivingLicenseApplications_View` | Local application + Class + Person + Status |
| `TestAppointments_View` | Appointments + Test type + Person |
| `detainedLicenses_View` | Detained + License + Person + Release info |

</details>

---

## 📁 Project Structure

```text
DVLD/
├── DVLD_DataAccess/                  # Data Access Layer
│   ├── cls*Data.cs                   # One static class per entity
│   ├── clsDataAccessSettings.cs      # Connection string
│   └── DVLD_DataAccess.csproj
│
├── DVLD_BuisnessLayer/               # Business Logic Layer
│   ├── cls*.cs                       # One entity class per table
│   ├── clsInternationalLicense.cs    # Inherits clsApplication
│   ├── clsLocalDrivingLicenseApplication.cs  # Inherits clsApplication
│   └── DVLD_BuisnessLayer.csproj
│
└── DVLD_PrsentationLayer/            # Presentation Layer
    ├── Applications/
    │   ├── Application_Types/
    │   ├── International_License/
    │   ├── Local_Driving_License/
    │   ├── Renew_Local_License/
    │   ├── ReplaceLostOrDamagedLicense/
    │   └── Rlease_Detained_License/
    ├── Drivers/
    ├── Global_Classes/               # clsFormat, clsGlobal, clsValidatoin, util
    ├── Licenses/                     # Controls, Detain, International, Local
    ├── Login/
    ├── people/Controls/
    ├── Tests/                        # Controls, Test_Types
    ├── User/
    ├── Mainfrm.cs
    ├── Program.cs
    └── DVLD_PrsentationLayer.csproj
```

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.8
- SQL Server 2016 or later (Express Edition works)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/mocode218-creator/DVLD-System.git
   cd DVLD-System
   ```

2. **Restore the database**
   - Open SQL Server Management Studio (SSMS)
   - Run `Database/DVLD_Database.sql` to create the `DVLD` database with tables, views, and sample data

3. **Configure the connection string** in `DVLD_DataAccess/clsDataAccessSettings.cs`
   ```csharp
   // Windows Authentication (recommended)
   public static string ConnectionString =
       "Server=.;Database=DVLD;Integrated Security=True;";

   // or SQL Authentication
   // "Server=.;Database=DVLD;User Id=sa;Password=YOUR_PASSWORD;"
   ```
   > ⚠️ Never commit real credentials to GitHub.

4. **Build and run**
   - Open `DVLD.sln`, set `DVLD_PrsentationLayer` as the startup project, then press **F5**

### Default Login

| Username | Password |
|----------|----------|
| `Msaqer` | `123456` |

> Demo credentials only. Change them before any real use.

---

## 🎮 Usage

### Main Menu

```text
📁 Applications
   ├── Driving Licenses Services
   │   ├── New Driving License
   │   ├── Manage Applications
   │   ├── Detain / Release / Manage Detained Licenses
   │   ├── Renew Driving License
   │   └── Replacement for Lost or Damaged License
   ├── International License (New / Manage)
   ├── Manage Application Types
   └── Manage Test Types
👥 People
👨‍✈️ Drivers
👤 Users
🔄 Account (Current User Info / Change Password / Sign Out)
```

### Typical Workflow: Getting a License

1. **Add a person**: People → Add New Person
2. **Create an application**: Applications → New Driving License
3. **Vision test**: right-click → Schedule Tests → Vision Test → Take Test
4. **Written test**: unlocked after passing Vision
5. **Street test**: unlocked after passing Written
6. **Issue license**: right-click → Issue Driving License (First Time)

---

## 📸 Screenshots

> Add your screenshots to `docs/screenshots/` and they will show up here.

| Login | Main Dashboard |
|:-----:|:--------------:|
| ![Login](docs/screenshots/login.png) | ![Main](docs/screenshots/main.png) |

| Manage People | Local License Application |
|:-------------:|:-------------------------:|
| ![People](docs/screenshots/people.png) | ![Local](docs/screenshots/local.png) |

| Schedule Test | License Info |
|:-------------:|:------------:|
| ![Test](docs/screenshots/test.png) | ![License](docs/screenshots/license.png) |

---

## 🧠 Core Business Logic

<details>
<summary><b>Enums</b> (click to expand)</summary>

```csharp
public enum enApplicationType
{
    NewDrivingLicense = 1,
    RenewDrivingLicense = 2,
    ReplaceLostDrivingLicense = 3,
    ReplaceDamagedDrivingLicense = 4,
    ReleaseDetainedDrivingLicsense = 5,
    NewInternationalLicense = 6,
    RetakeTest = 7
}

public enum enIssueReason
{
    FirstTime = 1,
    Renew = 2,
    DamagedReplacement = 3,
    LostReplacement = 4
}

public enum enTestType
{
    VisionTest = 1,
    WrittenTest = 2,
    StreetTest = 3
}
```

</details>

### Key Business Rules

| Rule | Enforced In |
|------|-------------|
| One active application per person per license class | `clsApplication.GetActiveApplicationIDForLicenseClass` |
| Vision before Written, Written before Street | `crlScheduleTest._HandlePrviousTestConstraint` |
| All 3 tests must be passed before issuing a license | `frmIssueDriverLicenseFirstTime` |
| International license requires a Class 3 local license | `frmNewInternationalLicenseApplication` |
| One active international license per driver | `clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID` |
| Only expired licenses can be renewed | `clsLicense.IsLicenseExpired` |
| A detained license cannot be detained again | `clsLicense.IsDetained` |

---

## 🗺️ Roadmap

- [ ] Hash passwords (e.g., PBKDF2/BCrypt) instead of storing plain text
- [ ] Move the connection string to `App.config`
- [ ] Use parameterized queries everywhere and add unit tests for the business layer
- [ ] Role-based permissions
- [ ] Export reports (PDF / Excel) and print license cards
- [ ] Migrate to ASP.NET Core Web API + modern UI

---

## 📜 License

Licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Author

**Moamen Mohamed**, Back-End .NET Developer

- GitHub: [mocode218-creator](https://github.com/mocode218-creator)
- LinkedIn: [moamen-mohamed](https://www.linkedin.com/in/moamen-mohamed-0bb938328/)
- Email: mocode218@gmail.com

---

<div align="center">

⭐ If you found this project helpful, please give it a star! ⭐

Made with ❤️ using C# and WinForms

</div>
