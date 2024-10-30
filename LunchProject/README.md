# LUNCH SPOTS API 🍔🥗🍕🍣🍛🥤
## Introducing a solution to the age-old question <br> ✨"WHERE SHOULD WE GO FOR LUNCH?"✨

## Overview
Lunch Spots API is a simple service built in ASP.NET Core, designed to let users maintain a list of lunch spots and most
importantly, decide where to eat. This project is intended for Leonie's learning purposes, utilising CRUD operations, 
JSON-based data storage, and ASP.NET Core API best practices.

Each lunch spot includes following information:

```json
{
  "Name": "Boots", 
  "PriceRange": "$",
  "AveragePortionSize": "small",
  "MinutesWalkAway": 4,
  "SuitableForLeonie": true,
  "SuitableForSahir": true,
  "SuitableForJanet": true
}
```

## 🏁Getting Started

1. Ensure you have [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
2. Clone the repository
3. Follow the instructions found [here](https://awazevr.atlassian.net/wiki/spaces/CP/pages/566395096/Github+Nuget+Source) for nuget

## 👷🏼‍♀️Build

Execute `dotnet build LunchProject.sln`

## 🧪Test

Execute `dotnet test LunchProject.sln`

