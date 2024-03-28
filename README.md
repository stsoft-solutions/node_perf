<!-- TOC -->
* [Preface](#preface)
* [Performance summary](#performance-summary)
<!-- TOC -->

# Preface
This is a simple example to check Node.js performance with different transports and handlers.

There is the simplest Express.js (Node v21.7.1, "express": "^4.19.2") server to maintain REST endpoints and gRPC server ("@grpc/grpc-js": "^1.10.4").

REST API provides the following endpoints:
- GET /bars - returns a JSON object with a serialization.
- GET /bars-static - returns a JSON object without serialization (prepared json string).

Both services are using the same data to prepare the response.

NBomber is used to test the performance of these servers.

For comparison, the .Net server (.Net 8) is added as well.

Json object
```json
{ "bars": [] }
```
With set bars: 
```json
{"open": 132.23, "high": 4324, "low": "433", "close": 432}
```

Computer:
CPU
```
	12th Gen Intel(R) Core(TM) i7-12700
	Base speed:	2,10 GHz
	Sockets:	1
	Cores:	12
	Logical processors:	20
	Virtualization:	Enabled
	L1 cache:	1,0 MB
	L2 cache:	12,0 MB
	L3 cache:	25,0 MB
```

Memory
```
	32,0 GB
	Speed:	2133 MHz
	Slots used:	2 of 4
	Form factor:	DIMM
	Hardware reserved:	244 MB
```

# Performance summary
The test's duration: 2 minutes

| Platform   | Data points | Threads | Endpoint     | RPS    | Data total (MB) |
|------------|-------------|---------|--------------|--------|-----------------|
| Express.js | 100         | 1       | gRPC         | 6628   | 2882            |
|            |             |         | /bars        | 6056   | 3472            |
|            |             |         | /bars-static | 7026   | 4028            |
|            |             | 50      | gRPC         | 14667  | 6378            |
|            |             |         | /bars        | 9186   | 5266            |
|            |             |         | /bars-static | 11634  | 6670            |
|            |             | 200     | gRPC         | 13639  | 5931            |
|            |             |         | /bars        | 8824   | 5059            |
|            |             |         | /bars-static | 11188  | 6415            |
|            | 5,000       | 1       | gRPC         | 454    | 9886            |
|            |             |         | /bars        | 299    | 8578            |
|            |             |         | /bars-static | 430    | 12319           |
|            |             | 50      | gRPC         | 477    | 10375           |
|            |             |         | /bars        | 691    | 19789           |
|            |             |         | /bars-static | 1468   | 42021           |
|            |             | 200     | gRPC         | 481    | 10472           |
|            |             |         | /bars        | 709    | 20286           |
|            |             |         | /bars-static | 1252   | 35841           |
| .Net       | 100         | 1       | gRPC         | 9353   | 4067            |
|            |             |         | /bars        | 7330   | 4203            |
|            |             |         | /bars-static | 12905  | 7399            |
|            |             | 50      | gRPC         | 59616  | 25925           |
|            |             |         | /bars        | 42887  | 24589           |
|            |             |         | /bars-static | 112125 | 64287           |
|            |             | 200     | gRPC         | 61444  | 26720           |
|            |             |         | /bars        | 37345  | 21411           |
|            |             |         | /bars-static | 94909  | 54416           |
|            | 5,000       | 1       | gRPC         | 1133   | 24643           |
|            |             |         | /bars        | 277    | 7929            |
|            |             |         | /bars-static | 1188   | 34003           |
|            |             | 50      | gRPC         | 1888   | 41059           |
|            |             |         | /bars        | 1335   | 38216           |
|            |             |         | /bars-static | 4879   | 139610          |
|            |             | 200     | gRPC         | 2094   | 45535           |
|            |             |         | /bars        | 1071   | 30648           |
|            |             |         | /bars-static | 4593   | 131413          |
