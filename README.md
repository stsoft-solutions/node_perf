<!-- TOC -->
* [Preface](#preface)
* [Performance summary](#performance-summary)
  * [1 Concurrent connection](#1-concurrent-connection)
  * [50 Concurrent connections](#50-concurrent-connections)
  * [200 Concurrent connections](#200-concurrent-connections)
  * [500 Concurrent connections](#500-concurrent-connections)
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
The test's duration: 30 seconds + 10 seconds warmup

## 1 Concurrent connection
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |       6990,1 |             0,14 |         243,1 |              4,03 |
| Node.js (Rest)            | 3002 |       8819,9 |             0,11 |         313,3 |              3,12 |
| Express.js (Rest)         | 3003 |         6060 |             0,16 |         301,6 |              3,25 |
| Node.js Cluster (Rest)    | 3009 |         8670 |             0,11 |         306,4 |               3,2 |
| Express.js Cluster (Rest) | 3010 |       6028,3 |             0,16 |         290,5 |              3,37 |
| Rust axum (Rest)          | 3005 |       5246,8 |             0,19 |         196,4 |              4,98 |
| Rust actix-web (Rest)     | 3006 |      10045,8 |              0,1 |         357,3 |              2,74 |
| Node.js (gRPC)            | 3004 |       6767,6 |             0,14 |         582,3 |              1,68 |
| Node.js Cluster (gRPC)    | 3008 |       6582,3 |             0,15 |         591,7 |              1,65 |
| .Net 8 (gRPC)             | 3001 |       9496,4 |              0,1 |          1211 |              0,81 |
| Rust (gRPC)               | 3007 |      11294,5 |             0,09 |         909,8 |              1,07 |

## 50 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |      45812,8 |             1,08 |        1226,7 |             40,25 |
| Node.js (Rest)            | 3002 |      21186,5 |             2,33 |           749 |             65,92 |
| Express.js (Rest)         | 3003 |       9820,9 |             5,02 |         677,9 |             72,89 |
| Node.js Cluster (Rest)    | 3009 |      40275,3 |             1,22 |        1492,8 |             33,07 |
| Express.js Cluster (Rest) | 3010 |      16977,7 |              2,9 |        1462,8 |             33,77 |
| Rust axum (Rest)          | 3005 |      34681,1 |             1,42 |         868,4 |                57 |
| Rust actix-web (Rest)     | 3006 |      49102,5 |             1,01 |        1483,3 |             33,37 |
| Node.js (gRPC)            | 3004 |      17315,6 |             2,85 |         667,5 |             74,11 |
| Node.js Cluster (gRPC)    | 3008 |      16212,4 |             3,04 |         676,5 |              73,1 |
| .Net 8 (gRPC)             | 3001 |      62360,4 |             0,79 |        2204,1 |             22,42 |
| Rust (gRPC)               | 3007 |      30066,5 |             1,64 |        2108,5 |              23,4 |

## 200 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |      43024,2 |             4,59 |         985,9 |            200,89 |
| Node.js (Rest)            | 3002 |      20224,7 |             9,75 |         777,5 |            254,23 |
| Express.js (Rest)         | 3003 |       9293,3 |            21,23 |         706,8 |            280,59 |
| Node.js Cluster (Rest)    | 3009 |      43665,8 |             4,52 |        1153,2 |            172,13 |
| Express.js Cluster (Rest) | 3010 |      33396,3 |             5,91 |          1097 |            314,59 |
| Rust axum (Rest)          | 3005 |      32014,3 |             6,16 |         820,5 |            257,67 |
| Rust actix-web (Rest)     | 3006 |        35848 |             5,52 |        1335,7 |            153,05 |
| Node.js (gRPC)            | 3004 |      16207,7 |            12,18 |         688,5 |            287,18 |
| Node.js Cluster (gRPC)    | 3008 |      15299,6 |            12,89 |         685,3 |            288,99 |
| .Net 8 (gRPC)             | 3001 |      64442,4 |             3,06 |          2060 |             103,6 |
| Rust (gRPC)               | 3007 |      26102,2 |             7,55 |        1787,1 |            110,71 |


## 500 Concurrent connections
| Framework                 | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|---------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)             | 3000 |      37857,4 |            13,03 |         794,8 |             655,4 |
| Node.js (Rest)            | 3002 |      18723,4 |            26,37 |         779,4 |            639,42 |
| Express.js (Rest)         | 3003 |       9038,1 |            54,58 |         717,4 |            695,54 |
| Node.js Cluster (Rest)    | 3009 |      32881,9 |            15,04 |        1005,8 |            511,55 |
| Express.js Cluster (Rest) | 3010 |      29272,2 |            16,91 |         941,2 |            547,44 |
| Rust axum (Rest)          | 3005 |        31876 |            15,49 |         758,4 |            723,64 |
| Rust actix-web (Rest)     | 3006 |      30378,4 |            16,04 |        1076,8 |            482,26 |
| Node.js (gRPC)            | 3004 |      13834,6 |            35,63 |         655,1 |            760,25 |
| Node.js Cluster (gRPC)    | 3008 |      12837,7 |            38,46 |         668,1 |            745,84 |
| .Net 8 (gRPC)             | 3001 |        57867 |             8,53 |        2154,3 |            255,33 |
| Rust (gRPC)               | 3007 |        27718 |            17,81 |        1722,5 |            288,22 |

## 1000 Concurrent connections
| Framework                    | Port | RPS<br/> 100 | Mean ms<br/> 100 | RPS<br/> 5000 | Mean ms<br/> 5000 |
|------------------------------|:----:|-------------:|-----------------:|--------------:|------------------:|
| .Net 8 (Rest)                | 3000 |      24264,4 |            40,68 |         690,7 |           1992,53 |
| Node.js (Rest)               | 3002 |      17586,2 |            56,23 |         790,2 |           1277,07 |
| Express.js (Rest)            | 3003 |       8767,8 |            112,8 |         718,5 |           1406,22 |
| Node.js Cluster (Rest) hf    | 3009 |      27610,5 |            39,57 |         696,7 |           1529,94 |
| Express.js Cluster (Rest) hf | 3010 |      20942,8 |             47,4 |         828,1 |           1502,43 |
| Rust axum (Rest)             | 3005 |      23424,8 |            43,58 |         818,2 |            1925,6 |
| Rust actix-web (Rest) hf     | 3006 |      28752,4 |            34,37 |           933 |           1545,25 | 
| Node.js (gRPC)               | 3004 |      13380,4 |            73,86 |         627,3 |           1584,38 |
| Node.js Cluster (gRPC)       | 3008 |      13567,3 |            72,77 |         638,1 |           1578,14 |
| .Net 8 (gRPC)                | 3001 |      42543,4 |            23,26 |        1736,4 |            621,42 |
| Rust (gRPC)                  | 3007 |      26242,3 |            37,68 |        1753,6 |            569,45 |

