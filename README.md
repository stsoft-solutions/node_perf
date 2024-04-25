<!-- TOC -->
* [Preface](#preface)
* [Performance summary](#performance-summary)
  * [1 Concurrent connection](#1-concurrent-connection)
  * [50 Concurrent connections](#50-concurrent-connections)
  * [200 Concurrent connections](#200-concurrent-connections)
  * [1000 Concurrent connections](#1000-concurrent-connections)
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
The test's duration: 2 minutes

## 1 Concurrent connection
| Framework             | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|-----------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)         | 3000 |         7003 |             0.14 |           238 |              4.19 |
| Node.js (Rest)        | 3002 |         8601 |             0,11 |           310 |              3.21 |
| Express.js (Rest)     | 3003 |         6078 |             0.16 |           285 |              3.49 |
| Rust axum (Rest)      | 3005 |         4922 |              0.2 |           180 |              5.52 |
| Rust actix-web (Rest) | 3006 |         9368 |             0.11 |           350 |              2.84 |
| Node.js (gRPC)        | 3004 |         6473 |             0.15 |           567 |              1.76 |
| .Net 8 (gRPC)         | 3001 |         9279 |             0.11 |          1201 |              0.84 |
| Rust (gRPC)           | 3007 |        11008 |             0.09 |           864 |              1.15 |

## 50 Concurrent connections
| Framework             | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|-----------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)         | 3000 |        43562 |             1.15 |          1181 |             42.81 |
| Node.js (Rest)        | 3002 |        20968 |             2.38 |           737 |             67.75 |
| Express.js (Rest)     | 3003 |         9929 |             5.03 |           690 |             72.39 |
| Rust axum (Rest)      | 3005 |        33666 |             1.48 |           847 |             59.85 |
| Rust actix-web (Rest) | 3006 |        49753 |                1 |          1541 |             34.82 |
| Node.js (gRPC)        | 3004 |        16724 |             2.99 |           649 |             76.96 |
| .Net 8 (gRPC)         | 3001 |        60841 |             0.83 |          2003 |              25.3 |
| Rust (gRPC)           | 3007 |        29976 |             1.67 |          1892 |             26.99 |

## 200 Concurrent connections
| Framework             | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|-----------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)         | 3000 |        39885 |             5.01 |          1339 |            154.95 |
| Node.js (Rest)        | 3002 |        20291 |             9.84 |           761 |            262.99 |
| Express.js (Rest)     | 3003 |         9658 |            20.68 |           704 |            284.34 |
| Rust axum (Rest)      | 3005 |        31954 |             6.25 |           961 |            225.14 |
| Rust actix-web (Rest) | 3006 |        44391 |             4.48 |          1646 |            108.79 |
| Node.js (gRPC)        | 3004 |        16948 |            11.79 |           667 |            300.25 |
| .Net 8 (gRPC)         | 3001 |        62595 |             3.23 |          2059 |            101.11 |
| Rust (gRPC)           | 3007 |        25499 |             7.84 |          1693 |            126.28 |


## 500 Concurrent connections
| Framework             | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|-----------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)         | 3000 |        33857 |            14.78 |          2997 |            372.43 |
| Node.js (Rest)        | 3002 |        18578 |             26.9 |           774 |            653.52 |
| Express.js (Rest)     | 3003 |         9052 |            55.17 |           700 |            715.04 |
| Rust axum (Rest)      | 3005 |        30494 |            16.41 |          1443 |            526.16 |
| Rust actix-web (Rest) | 3006 |        40168 |            12.76 |          2199 |            249.48 |
| Node.js (gRPC)        | 3004 |        13435 |            37.17 |           622 |            804.39 |
| .Net 8 (gRPC)         | 3001 |        54473 |             9.36 |          1933 |            275.07 |
| Rust (gRPC)           | 3007 |        27666 |            18.06 |          1651 |            323.77 |
