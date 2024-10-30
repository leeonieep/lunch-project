# LUNCH SPOTS API 🍔 🥗 🍕 🍛 🍣 🥤
## Introducing a solution to the age-old question <br> ✨ "WHERE SHOULD WE GO FOR LUNCH?" ✨

## ▶️ Overview
Lunch Spots API is a simple service built in ASP.NET Core, designed to let users maintain a list of lunch spots and most
importantly, decide where to eat. This project is intended for Leonie's learning purposes, utilising CRUD operations, 
JSON-based data storage, and ASP.NET Core API best practices.

## 🏁 Getting Started

1. Ensure you have [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed
2. Clone the repository
3. Build `dotnet build LunchProject.sln` the project
4. Test `dotnet test LunchProject.sln` the project

## ❇️ Testing with Swagger

Run the project and navigate to [Swagger](http://localhost:5000/docs/index.html) to interact with the API endpoints.

## 📍 Endpoints

## POST /spot/add
**Description:** Adds a new lunch spot to the JSON file, validating name to prevent duplicates.<br>
**Request Body:** JSON object with lunch spot properties.<br>
**Response:** Returns 201 Created if successful.

## POST /spot/find
**Description:** Returns a lunch spot that matches provided criteria.<br>
**Request Body:** JSON object with lunch spot requirements.<br>
**Response:** Returns the details of lunch spots that match criteria.

## DELETE /spot/delete/{name}
**Description:** Deletes a lunch spot by its name.<br>
**Parameters:** The name of the lunch spot to delete.<br>
**Response:** Returns 200 OK if deleted, or 404 Not Found if the spot doesn’t exist.

## 💾 Data 

The "database" for this project is a simple JSON file, lunchspots.json, where all lunch spot records are stored. While 
this is not as robust as a true database, it’s a start.

```json
Each lunch spot includes following information:

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
