# About node_perf

Simple projects to test RPS with different frameworks

REST API
- GET /bar/100
- GET /bar/5000

gRPC
- GetBar100
- GetBar5000

Response
```
message BarsResponse {
  string symbol = 1; 
  repeated Bar bars = 2;
}

message Bar {
  double open = 1;
  double high = 2;
  double low = 3;
  double close = 4;
}
```

| Framework             | Port | RPS | Mean ms |   
|-----------------------|------|-----|---------|
| .Net 8 (Rest)         | 3000 |     |         |
| Node.js (Rest)        | 3002 |     |         |
| Express.js (Rest)     | 3003 |     |         |
| Rust axum (Rest)      | 3005 |     |         |
| Rust actix-web (Rest) | 3006 |     |         |
| Node.js (gRPC)        | 3004 |     |         |
| .Net 8 (gRPC)         | 3001 |     |         |
| Rust (gRPC)           | 3007 |     |         |
