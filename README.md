# Content Parser API

### Wymagania
- .NET 8 SDK lub nowszy

### Przywrócenie zależności
```bash
dotnet restore
```

### Uruchomienie projektu
```bash
dotnet run --project src/ContentParser.Api
```

### Swagger UI
Po uruchomieniu przejdź pod adres: `http://localhost:5000/swagger` (lub port HTTPS wyświetlony w konsoli)

### Uruchomienie testów
```bash
dotnet test
```

### Przykład użycia
`POST /api/v1/parse-content`
Nagłówek: `Content-Type: application/json`

Żądanie:
```json
{
  "type": "INTERNAL_JSON",
  "content": "W3siaWQiOjEsInByb2R1Y3QiOiJMYXB0b3AiLCJwcmljZSI6MTIwMH0seyJpZCI6MiwicHJvZHVjdCI6Ik1vdXNlIiwicHJpY2UiOjUwfV0="
}
```

Odpowiedź (`200 OK`):
```json
{
  "processedRowsCount": 2,
  "data": [
    {
      "id": "1",
      "product": "Laptop",
      "price": "1200"
    },
    {
      "id": "2",
      "product": "Mouse",
      "price": "50"
    }
  ]
}
```

Obsługiwane typy: `CSV`, `INTERNAL_JSON`.

---



### Requirements
- .NET 8 SDK or newer

### Restore dependencies
```bash
dotnet restore
```

### How to Run
```bash
dotnet run --project src/ContentParser.Api
```

### Swagger UI
Once running, open: `http://localhost:5000/swagger` (or the HTTPS port shown in console)

### How to Run Tests
```bash
dotnet test
```

### Usage Example
`POST /api/v1/parse-content`
Header: `Content-Type: application/json`

Request:
```json
{
  "type": "INTERNAL_JSON",
  "content": "W3siaWQiOjEsInByb2R1Y3QiOiJMYXB0b3AiLCJwcmljZSI6MTIwMH0seyJpZCI6MiwicHJvZHVjdCI6Ik1vdXNlIiwicHJpY2UiOjUwfV0="
}
```

Response (`200 OK`):
```json
{
  "processedRowsCount": 2,
  "data": [
    {
      "id": "1",
      "product": "Laptop",
      "price": "1200"
    },
    {
      "id": "2",
      "product": "Mouse",
      "price": "50"
    }
  ]
}
```

Supported types: `CSV`, `INTERNAL_JSON`.