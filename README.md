<!-- TOC -->
* [Preface](#preface)
* [Performance summary](#performance-summary)
  * [1 Concurrent connection](#1-concurrent-connection)
  * [50 Concurrent connections](#50-concurrent-connections)
  * [200 Concurrent connections](#200-concurrent-connections)
  * [500 Concurrent connections](#500-concurrent-connections)
<!-- TOC -->

# Preface
This is a simple example to check Node.js performance with different transports and handlers.

There is the simplest Express.js (Node v21.7.1, "express": "^4.19.2") server to maintain REST endpoints and gRPC server ("@grpc/grpc-js": "^1.10.4").

Both services are using the same data to prepare the response.

NBomber is used to test the performance of these servers.

For comparison, the .Net server (.Net 8) is added as well.

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
```
```
message Bar {
  double open = 1;
  double high = 2;
  double low = 3;
  double close = 4;
}
```

Computer:
CPU: `12th Gen Intel(R) Core(TM) i7-12700`

Memory:
```
	32,0 GB
	Speed:	2133 MHz
	Form factor:	DIMM
```

# Performance summary
The test's duration: 30 seconds + 10 seconds warmup

## 1 Concurrent connection
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |              |                  |               |                   |
| Node.js (Rest)            | 3002 |              |                  |               |                   |
| Express.js (Rest)         | 3003 |              |                  |               |                   |
| Node.js Cluster (Rest)    | 3009 |              |                  |               |                   |
| Express.js Cluster (Rest) | 3010 |              |                  |               |                   |
| Rust axum (Rest)          | 3005 |              |                  |               |                   |
| Rust actix-web (Rest)     | 3006 |              |                  |               |                   |
| Node.js (gRPC)            | 3004 |              |                  |               |                   |
| Node.js Cluster (gRPC)    | 3008 |              |                  |               |                   |
| .Net 8 (gRPC)             | 3001 |              |                  |               |                   |
| Rust (gRPC)               | 3007 |              |                  |               |                   |

## 50 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |              |                  |               |                   |
| Node.js (Rest)            | 3002 |              |                  |               |                   |
| Express.js (Rest)         | 3003 |              |                  |               |                   |
| Node.js Cluster (Rest)    | 3009 |              |                  |               |                   |
| Express.js Cluster (Rest) | 3010 |              |                  |               |                   |
| Rust axum (Rest)          | 3005 |              |                  |               |                   |
| Rust actix-web (Rest)     | 3006 |              |                  |               |                   |
| Node.js (gRPC)            | 3004 |              |                  |               |                   |
| Node.js Cluster (gRPC)    | 3008 |              |                  |               |                   |
| .Net 8 (gRPC)             | 3001 |              |                  |               |                   |
| Rust (gRPC)               | 3007 |              |                  |               |                   |

## 200 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |              |                  |               |                   |
| Node.js (Rest)            | 3002 |              |                  |               |                   |
| Express.js (Rest)         | 3003 |              |                  |               |                   |
| Node.js Cluster (Rest)    | 3009 |              |                  |               |                   |
| Express.js Cluster (Rest) | 3010 |              |                  |               |                   |
| Rust axum (Rest)          | 3005 |              |                  |               |                   |
| Rust actix-web (Rest)     | 3006 |              |                  |               |                   |
| Node.js (gRPC)            | 3004 |              |                  |               |                   |
| Node.js Cluster (gRPC)    | 3008 |              |                  |               |                   |
| .Net 8 (gRPC)             | 3001 |              |                  |               |                   |
| Rust (gRPC)               | 3007 |              |                  |               |                   |


## 500 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |              |                  |               |                   |
| Node.js (Rest)            | 3002 |              |                  |               |                   |
| Express.js (Rest)         | 3003 |              |                  |               |                   |
| Node.js Cluster (Rest)    | 3009 |              |                  |               |                   |
| Express.js Cluster (Rest) | 3010 |              |                  |               |                   |
| Rust axum (Rest)          | 3005 |              |                  |               |                   |
| Rust actix-web (Rest)     | 3006 |              |                  |               |                   |
| Node.js (gRPC)            | 3004 |              |                  |               |                   |
| Node.js Cluster (gRPC)    | 3008 |              |                  |               |                   |
| .Net 8 (gRPC)             | 3001 |              |                  |               |                   |
| Rust (gRPC)               | 3007 |              |                  |               |                   |

