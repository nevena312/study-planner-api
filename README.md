# Study Planner

## Opis projekta

Study Planner je web aplikacija namenjena studentima za organizaciju učenja, praćenje ispita, upravljanje zadacima i kreiranje planova učenja.

Aplikacija omogućava korisniku da:

* vodi evidenciju predmeta,
* evidentira ispite i rokove,
* kreira zadatke povezane sa predmetima,
* organizuje zadatke kroz planove učenja,
* automatski generiše planove učenja na osnovu prioriteta i rokova,
* koristi MCP (Model Context Protocol) integraciju za interakciju sa aplikacijom putem AI agenta.


# Tehnologije

## Backend

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication

## Frontend

* React
* Vite
* Axios
* React Router
* Bootstrap

## MCP

* ModelContextProtocol (MCP)
* Codex CLI


# Arhitektura sistema

Sistem se sastoji iz tri dela:

```text
React Frontend
        |
        v
ASP.NET Core Web API
        |
        v
PostgreSQL Database
```

MCP server predstavlja dodatni sloj koji omogućava komunikaciju AI agenta sa aplikacijom:

```text
Codex CLI
        |
        v
StudyPlanner.McpServer
        |
        v
StudyPlanner.Api
        |
        v
PostgreSQL
```


# Funkcionalnosti aplikacije

## Autentifikacija

* Registracija korisnika
* Prijava korisnika
* JWT autentifikacija
* Zaštita API endpointa


## Predmeti (Subjects)

Korisnik može:

* dodati predmet,
* pregledati sve predmete,
* izmeniti predmet,
* obrisati predmet.


## Ispiti (Exams)

Korisnik može:

* dodati ispit,
* pregledati ispite,
* menjati podatke o ispitu,
* obrisati ispit,
* pregledati predstojeće ispite.

Podržani tipovi ispita:

* Kolokvijum
* Ispit
* Projekat
* Domaci


## Zadaci (Study Tasks)

Korisnik može:

* dodati zadatak,
* izmeniti zadatak,
* obrisati zadatak,
* pregledati zadatke,
* filtrirati zadatke po statusu,
* filtrirati zadatke po prioritetu.

### Statusi

* Planned
* In Progress
* Completed

### Prioriteti

* Low
* Medium
* High


## Planovi učenja (Study Plans)

Korisnik može:

* kreirati plan učenja,
* menjati plan učenja,
* obrisati plan učenja,
* pregledati zadatke u okviru plana.


## Dashboard

Dashboard prikazuje:

* broj predmeta,
* broj ispita,
* broj aktivnih zadataka,
* broj završenih zadataka,
* naredni ispit,
* listu predstojećih ispita,
* listu aktivnih zadataka.

Dashboard kartice predstavljaju navigaciju ka odgovarajućim sekcijama sistema.


# Smart Study Plan Generator

Aplikacija poseduje funkcionalnost automatskog generisanja plana učenja.

Prilikom generisanja:

1. Pronalaze se svi nezavršeni zadaci koji nisu raspoređeni u postojeći plan.
2. Zadaci se sortiraju prema:

   * roku izvršenja,
   * prioritetu.
3. Kreira se novi plan učenja.
4. Zadaci se automatski povezuju sa kreiranim planom.

Na taj način korisnik može brzo da organizuje svoje aktivnosti bez ručnog raspoređivanja svakog zadatka.


# Dependency Injection

Dependency Injection korišćen je u backend i MCP delu sistema.

## Backend

Za registraciju i korišćenje:

* DbContext-a
* JWT servisa
* ASP.NET Core servisa

Primer:

```csharp
builder.Services.AddDbContext<StudyPlannerDbContext>();
```

## MCP

Za registraciju:

* AuthState
* AuthService
* StudyPlannerApiService

Primer:

```csharp
builder.Services.AddSingleton<AuthState>();

builder.Services.AddHttpClient<AuthService>();

builder.Services.AddHttpClient<StudyPlannerApiService>();
```


# MCP Integracija

MCP server omogućava korišćenje aplikacije preko AI agenta.

## Implementirani MCP alati

### Autentifikacija

* `login_user`

### Dashboard

* `get_dashboard_summary`

### Ispiti

* `get_upcoming_exams`

### Zadaci

* `get_pending_tasks`
* `get_tasks_by_subject`
* `complete_task`
* `create_task`

### Predmeti

* `get_subjects`

### Planovi učenja

* `get_study_plans`
* `get_tasks_by_plan`
* `create_study_plan`
* `generate_study_plan`


# Pokretanje projekta

## Backend

```bash
cd StudyPlanner.Api
dotnet run
```

Backend se pokreće na:

```text
https://localhost:7112
```


## Frontend

```bash
cd study-planner-client
npm install
npm run dev
```

Frontend se pokreće na:

```text
http://localhost:5173
```


## MCP Server

```bash
cd StudyPlanner.McpServer
dotnet run
```

Nakon pokretanja MCP server može biti povezan sa Codex CLI klijentom.


# Struktura projekta

```text
StudyPlanner.Api
│
├── StudyPlanner.Api
│   ├── Controllers
│   ├── DTOs
│   ├── Models
│   ├── Data
│   ├── Enums
│   └── Services
│
├── StudyPlanner.McpServer
│   ├── Models
│   ├── Services
│   └── Tools
│
└── study-planner-client
    ├── src
    │   ├── pages
    │   ├── services
    │   ├── components
    │   └── routes
```
