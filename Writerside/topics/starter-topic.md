# About node_perf

Simple projects to test RPS with different frameworks

REST API
- GET /bar/100
- GET /bar/5000

gRPC
- GetBar100
- GetBar5000

*Data item*
```
message Bar {
  double open = 1;
  double high = 2;
  double low = 3;
  double close = 4;
}
```

Response
```
message BarsResponse {
  string symbol = 1; 
  repeated Bar bars = 2;
}
```

|                       | Port | RPS | Mean ms |   
|-----------------------|------|-----|---------|
| .Net 8 (Rest)         | 3000 |     |         |
| Node.js (Rest)        | 3001 |     |         |
| Express.js (Rest)     |      |     |         |
| Rust actix-web (Rest) |      |     |         |
| Rust axum (Rest)      |      |     |         |
| Node.js (gRPC)        |      |     |         |
| .Net 8 (gRPC)         |      |     |         |
| Rust (gRPC)           |      |     |         |
