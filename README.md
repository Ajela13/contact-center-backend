# Backend - Contact Center WebSocket API

This backend project provides an API for managing agents and clients in a contact center environment. It includes WebSocket connections for real-time updates on agents and clients' statuses. The server allows clients to filter agents based on wait time and receive continuous updates for agents and clients via WebSocket connections.

## Technologies Used:

- **C#** (.NET Core)
- **ASP.NET Web API**
- **WebSockets**
- **Entity Framework (for database management)**

## Endpoints

### 1. **GET /clients**

Fetches a list of clients with optional filtering based on wait time.

#### Query Parameters:

- `minWaitTime`: Minimum wait time (inclusive) for the client to be returned.
- `maxWaitTime`: Maximum wait time (inclusive) for the client to be returned.

#### Example Request:

GET /clients?minWaitTime=5&maxWaitTime=10

#### Response:

```json
[
  {
    "id": 1,
    "name": "Client 1",
    "waitTime": 8
  },
  {
    "id": 2,
    "name": "Client 2",
    "waitTime": 6
  }
]
```

### Status Codes:

200 OK: Successfully fetched clients.
400 Bad Request: Invalid query parameters.

### 2. **GET /agents**

Fetches a list of agents with optional filtering based on their status.

#### Query Parameters:

status: The status of the agent. Possible values: available, busy, paused.

#### Example Request:

GET /agents?status=available

### Response:

```json
[
  {
    "id": 1,
    "name": "Agent 1",
    "status": "available",
    "waitTime": 10
  },
  {
    "id": 2,
    "name": "Agent 2",
    "status": "available",
    "waitTime": 5
  }
]
```

### Status Codes:

200 OK: Successfully fetched agents.
400 Bad Request: Invalid query parameters.

### How to Run the Backend Locally

Clone the repository:

```
git clone <repository_url>
cd <repository_folder>
```

Install dependencies: Make sure you have .NET Core SDK installed. If not, download it from here.

Build and run the application:

```
dotnet build
dotnet run
```

Access the API:

The API will be available at http://localhost:5248/.

You can use Postman or cURL to interact with the REST API.

The WebSocket endpoints are available at ws://localhost:5248/ws/clients and ws://localhost:5248/ws/agents.
