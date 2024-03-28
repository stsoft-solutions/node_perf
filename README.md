<!-- TOC -->
* [Preface](#preface)
* [Performance summary](#performance-summary)
* [NBomber results](#nbomber-results)
  * [.Net server - 10,000 data points](#net-server---10000-data-points)
    * [1 thread](#1-thread)
      * [gRPC](#grpc)
      * [/bars](#bars)
      * [/bars-static](#bars-static)
    * [50 threads](#50-threads)
      * [gRPC](#grpc-1)
      * [/bars](#bars-1)
      * [/bars-static](#bars-static-1)
    * [200 threads](#200-threads)
      * [gRPC](#grpc-2)
      * [/bars](#bars-2)
      * [/bars-static](#bars-static-2)
  * [.Net server - 100 data points](#net-server---100-data-points)
    * [1 thread](#1-thread-1)
      * [gRPC](#grpc-3)
      * [/bars](#bars-3)
      * [/bars-static](#bars-static-3)
    * [50 threads](#50-threads-1)
      * [gRPC](#grpc-4)
      * [/bars](#bars-4)
      * [/bars-static](#bars-static-4)
    * [200 threads](#200-threads-1)
      * [gRPC](#grpc-5)
      * [/bars](#bars-5)
      * [/bars-static](#bars-static-5)
  * [Node.js server - 10,000 data points](#nodejs-server---10000-data-points)
    * [1 thread](#1-thread-2)
      * [gRPC](#grpc-6)
      * [/bars](#bars-6)
      * [/bars-static](#bars-static-6)
    * [50 threads](#50-threads-2)
      * [gRPC](#grpc-7)
      * [/bars](#bars-7)
      * [/bars-static](#bars-static-7)
    * [200 threads](#200-threads-2)
      * [gRPC](#grpc-8)
      * [/bars](#bars-8)
      * [/bars-static](#bars-static-8)
  * [Node.js server - 100 data points](#nodejs-server---100-data-points)
    * [1 thread](#1-thread-3)
      * [gRPC](#grpc-9)
      * [/bars](#bars-9)
      * [/bars-static](#bars-static-9)
    * [50 threads](#50-threads-3)
      * [gRPC](#grpc-10)
      * [/bars](#bars-10)
      * [/bars-static](#bars-static-10)
    * [200 threads](#200-threads-3)
      * [gRPC](#grpc-11)
      * [/bars](#bars-11)
      * [/bars-static](#bars-static-11)
<!-- TOC -->

# Preface
This is a simple example to check Node.js performance with different transports and handlers.

There is the simplest Express.js server that listens to on port 3000 and returns a simple JSON object.
And gRPC server that listens to on port 50051 and returns the same JSON object.
REST API provides the following endpoint:
- GET /bars - returns a JSON object with a serialization.
- GET /bars-static - returns a JSON object without serialization (prepared json string).

NBomber is used to test the performance of these servers.

For comparing the .Net server (.Net 8) is added as well.

# Performance summary

| Platform   | Data points | Threads | Endpoint     | RPS   | Data total (MB) |
|------------|-------------|---------|--------------|-------|-----------------|
| Express.js | 100         | 1       | gRPC         | 2772  | 1205            |
|            |             |         | /bars        | 396   | 227             |
|            |             |         | /bars-static | 413   | 237             |
|            |             | 50      | gRPC         | 10036 | 4364            |
|            |             |         | /bars        | 1612  | 1612            |
|            |             |         | /bars-static | 1558  | 1558            |
|            |             | 200     | gRPC         | 10060 | 4375            |
|            |             |         | /bars        | 1662  | 953             |
|            |             |         | /bars-static | 1645  | 943             |
|            | 10,000      | 1       | gRPC         | 182   | 7928            |
|            |             |         | /bars        | 61    | 3495            |
|            |             |         | /bars-static | 91    | 5248            |
|            |             | 50      | gRPC         | 296   | 12883           |
|            |             |         | /bars        | 219   | 12585           |
|            |             |         | /bars-static | 323   | 18516           |
|            |             | 200     | gRPC         | 202   | 11560           |
|            |             |         | /bars        | 296   | 16934           |
|            |             |         | /bars-static |       |                 |
| .Net       | 100         | 1       | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |
|            |             | 50      | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |
|            |             | 200     | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |
|            | 10,000      | 1       | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |
|            |             | 50      | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |
|            |             | 200     | gRPC         |       |                 |
|            |             |         | /bars        |       |                 |
|            |             |         | /bars-static |       |                 |



# NBomber results

## .Net server - 10,000 data points
### 1 thread
#### gRPC
scenario: `scenario_grpc`

- ok count: `16793`

- fail count: `0`

- all data: `6085,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `16793`, ok = `16793`, RPS = `139,9`                                     |
| latency            | min = `4,88` ms, mean = `7,12` ms, max = `34,22` ms, StdDev = `2,74`           |
| latency percentile | p50 = `6,24` ms, p75 = `6,77` ms, p95 = `13,1` ms, p99 = `18,67` ms            |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `6085,7` MB |

#### /bars
scenario: `scenario_http`

- ok count: `10220`

- fail count: `0`

- all data: `4873,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `10220`, ok = `10220`, RPS = `85,2`                                      |
| latency            | min = `9,36` ms, mean = `11,69` ms, max = `51,44` ms, StdDev = `3,72`          |
| latency percentile | p50 = `10,62` ms, p75 = `11,39` ms, p95 = `18,05` ms, p99 = `30,69` ms         |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `4873,4` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `17348`

- fail count: `0`

- all data: `8272,3` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `17348`, ok = `17348`, RPS = `144,6`                                     |
| latency            | min = `5,64` ms, mean = `6,89` ms, max = `51,92` ms, StdDev = `1,26`           |
| latency percentile | p50 = `6,68` ms, p75 = `7,3` ms, p95 = `8,09` ms, p99 = `9,12` ms              |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `8272,3` MB |

### 50 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `30536`

- fail count: `0`

- all data: `11066,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `30536`, ok = `30536`, RPS = `254,5`                                      |
| latency            | min = `6,81` ms, mean = `196,51` ms, max = `391,03` ms, StdDev = `35,3`         |
| latency percentile | p50 = `196,74` ms, p75 = `216,7` ms, p95 = `251,78` ms, p99 = `289,28` ms       |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `11066,1` MB |

#### /bars
scenario: `scenario_http`

- ok count: `32175`

- fail count: `0`

- all data: `15342,5` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `32175`, ok = `32175`, RPS = `268,1`                                      |
| latency            | min = `11,18` ms, mean = `187,45` ms, max = `574,98` ms, StdDev = `65,42`       |
| latency percentile | p50 = `164,22` ms, p75 = `194,05` ms, p95 = `336,9` ms, p99 = `387,58` ms       |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `15342,5` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `75293`

- fail count: `0`

- all data: `35903,2` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `75293`, ok = `75293`, RPS = `627,4`                                      |
| latency            | min = `6,79` ms, mean = `79,65` ms, max = `2123,48` ms, StdDev = `37,07`        |
| latency percentile | p50 = `68,74` ms, p75 = `87,81` ms, p95 = `143,49` ms, p99 = `189,7` ms         |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `35903,2` MB |

### 200 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `40140`

- fail count: `0`

- all data: `14546,6` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `40140`, ok = `40140`, RPS = `334,5`                                      |
| latency            | min = `9,29` ms, mean = `600,25` ms, max = `1752,15` ms, StdDev = `185,03`      |
| latency percentile | p50 = `550,91` ms, p75 = `653,31` ms, p95 = `973,82` ms, p99 = `1267,71` ms     |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `14546,6` MB |

#### /bars
scenario: `scenario_http`

- ok count: `27626`

- fail count: `0`

- all data: `13173,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `27626`, ok = `27626`, RPS = `230,2`                                      |
| latency            | min = `14,2` ms, mean = `883,98` ms, max = `1843,12` ms, StdDev = `267,43`      |
| latency percentile | p50 = `827,39` ms, p75 = `1136,64` ms, p95 = `1308,67` ms, p99 = `1366,02` ms   |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `13173,4` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `66473`

- fail count: `0`

- all data: `31697,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `66473`, ok = `66473`, RPS = `553,9`                                      |
| latency            | min = `7,13` ms, mean = `361,11` ms, max = `1321,76` ms, StdDev = `127,41`      |
| latency percentile | p50 = `293,63` ms, p75 = `484,61` ms, p95 = `546,3` ms, p99 = `629,25` ms       |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `31697,4` MB |

## .Net server - 100 data points
### 1 thread
#### gRPC
scenario: `scenario_grpc`

- ok count: `52909`

- fail count: `0`

- all data: `191,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `52909`, ok = `52909`, RPS = `440,9`                              |
| latency            | min = `1,76` ms, mean = `2,26` ms, max = `45,88` ms, StdDev = `0,6`     |
| latency percentile | p50 = `2,09` ms, p75 = `2,38` ms, p95 = `2,97` ms, p99 = `4,15` ms      |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `191,7` MB |

#### /bars
scenario: `scenario_http`

- ok count: `45402`

- fail count: `0`

- all data: `216,9` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `45402`, ok = `45402`, RPS = `378,4`                              |
| latency            | min = `1,9` ms, mean = `2,63` ms, max = `23,26` ms, StdDev = `0,58`     |
| latency percentile | p50 = `2,56` ms, p75 = `2,81` ms, p95 = `3,38` ms, p99 = `4,36` ms      |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `216,9` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `46614`

- fail count: `0`

- all data: `222,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `46614`, ok = `46614`, RPS = `388,4`                              |
| latency            | min = `1,79` ms, mean = `2,56` ms, max = `24,44` ms, StdDev = `0,62`    |
| latency percentile | p50 = `2,48` ms, p75 = `2,74` ms, p95 = `3,42` ms, p99 = `4,74` ms      |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `222,7` MB |

### 50 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `76450`

- fail count: `0`

- all data: `277,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `76450`, ok = `76450`, RPS = `637,1`                               |
| latency            | min = `1,94` ms, mean = `78,46` ms, max = `199,3` ms, StdDev = `19,82`   |
| latency percentile | p50 = `78,85` ms, p75 = `87,55` ms, p95 = `110,14` ms, p99 = `134,91` ms |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `277,1` MB  |

#### /bars
scenario: `scenario_http`

- ok count: `262228`

- fail count: `0`

- all data: `1252,9` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `262228`, ok = `262228`, RPS = `2185,2`                            |
| latency            | min = `2,06` ms, mean = `22,85` ms, max = `1108,34` ms, StdDev = `15,1`  |
| latency percentile | p50 = `20,32` ms, p75 = `23,28` ms, p95 = `48,19` ms, p99 = `63,2` ms    |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1252,9` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `225506`

- fail count: `0`

- all data: `1077,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `225506`, ok = `225506`, RPS = `1879,2`                            |
| latency            | min = `2,18` ms, mean = `26,57` ms, max = `4534` ms, StdDev = `59,97`    |
| latency percentile | p50 = `21,17` ms, p75 = `25,18` ms, p95 = `56,99` ms, p99 = `71,74` ms   |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1077,4` MB |

### 200 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `113295`

- fail count: `0`

- all data: `410,6` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                   |
|--------------------|----------------------------------------------------------------------------|
| name               | `global information`                                                       |
| request count      | all = `113295`, ok = `113295`, RPS = `944,1`                               |
| latency            | min = `2,55` ms, mean = `211,65` ms, max = `429,16` ms, StdDev = `40,59`   |
| latency percentile | p50 = `207,74` ms, p75 = `225,92` ms, p95 = `295,42` ms, p99 = `334,08` ms |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `410,6` MB    |

#### /bars
scenario: `scenario_http`

- ok count: `216223`

- fail count: `0`

- all data: `1033,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                  |
|--------------------|---------------------------------------------------------------------------|
| name               | `global information`                                                      |
| request count      | all = `216223`, ok = `216223`, RPS = `1801,9`                             |
| latency            | min = `3,35` ms, mean = `110,95` ms, max = `1484,83` ms, StdDev = `64,83` |
| latency percentile | p50 = `83,33` ms, p75 = `111,04` ms, p95 = `226,18` ms, p99 = `291,58` ms |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1033,1` MB  |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `210375`

- fail count: `0`

- all data: `1005,2` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                  |
|--------------------|---------------------------------------------------------------------------|
| name               | `global information`                                                      |
| request count      | all = `210375`, ok = `210375`, RPS = `1753,1`                             |
| latency            | min = `3,39` ms, mean = `114,02` ms, max = `3449,48` ms, StdDev = `67,65` |
| latency percentile | p50 = `83,39` ms, p75 = `125,57` ms, p95 = `225,54` ms, p99 = `254,21` ms |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1005,2` MB  |

## Node.js server - 10,000 data points
### 1 thread
#### gRPC
scenario: `scenario_grpc`

- ok count: `21878`

- fail count: `0`

- all data: `7928,5` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `21878`, ok = `21878`, RPS = `182,3`                                     |
| latency            | min = `2,89` ms, mean = `5,49` ms, max = `2057,6` ms, StdDev = `19,86`         |
| latency percentile | p50 = `4,29` ms, p75 = `5,03` ms, p95 = `13,23` ms, p99 = `17,87` ms           |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `7928,5` MB |

#### /bars
scenario: `scenario_http`

- ok count: `7331`

- fail count: `0`

- all data: `3495,8` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `7331`, ok = `7331`, RPS = `61,1`                                        |
| latency            | min = `10,86` ms, mean = `16,32` ms, max = `62,09` ms, StdDev = `7,05`         |
| latency percentile | p50 = `13,02` ms, p75 = `16,58` ms, p95 = `33,18` ms, p99 = `40,48` ms         |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `3495,8` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `11006`

- fail count: `0`

- all data: `5248,2` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                       |
|--------------------|--------------------------------------------------------------------------------|
| name               | `global information`                                                           |
| request count      | all = `11006`, ok = `11006`, RPS = `91,7`                                      |
| latency            | min = `7,87` ms, mean = `10,87` ms, max = `59,92` ms, StdDev = `4,67`          |
| latency percentile | p50 = `9,47` ms, p75 = `10,41` ms, p95 = `18,69` ms, p99 = `35,94` ms          |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `5248,2` MB |

### 50 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `35551`

- fail count: `0`

- all data: `12883,5` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `35551`, ok = `35551`, RPS = `296,3`                                      |
| latency            | min = `4,72` ms, mean = `168,75` ms, max = `2250,43` ms, StdDev = `88,55`       |
| latency percentile | p50 = `166,78` ms, p75 = `192,51` ms, p95 = `237,82` ms, p99 = `285,95` ms      |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `12883,5` MB |

#### /bars
scenario: `scenario_http`

- ok count: `26393`

- fail count: `0`

- all data: `12585,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `26393`, ok = `26393`, RPS = `219,9`                                      |
| latency            | min = `12,3` ms, mean = `227,38` ms, max = `546,07` ms, StdDev = `32,41`        |
| latency percentile | p50 = `226,69` ms, p75 = `236,03` ms, p95 = `273,15` ms, p99 = `313,86` ms      |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `12585,4` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `38830`

- fail count: `0`

- all data: `18516,0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `38830`, ok = `38830`, RPS = `323,6`                                      |
| latency            | min = `10,15` ms, mean = `154,98` ms, max = `365,88` ms, StdDev = `54,2`        |
| latency percentile | p50 = `131,84` ms, p75 = `185,09` ms, p95 = `265,21` ms, p99 = `296,19` ms      |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `18516,0` MB |

### 200 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `33785`

- fail count: `0`

- all data: `12243,6` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `33785`, ok = `33785`, RPS = `281,5`                                      |
| latency            | min = `14,17` ms, mean = `710,07` ms, max = `2864,54` ms, StdDev = `226,93`     |
| latency percentile | p50 = `736,77` ms, p75 = `818,69` ms, p95 = `918,53` ms, p99 = `1039,36` ms     |
| data transfer      | min = `371,094` KB, mean = `371,125` KB, max = `371,094` KB, all = `12243,6` MB |

#### /bars
scenario: `scenario_http`

- ok count: `24243`

- fail count: `0`

- all data: `11560,2` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `24243`, ok = `24243`, RPS = `202`                                        |
| latency            | min = `13,9` ms, mean = `992,94` ms, max = `4481,36` ms, StdDev = `284,94`      |
| latency percentile | p50 = `925,7` ms, p75 = `944,64` ms, p95 = `1586,18` ms, p99 = `1899,52` ms     |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `11560,2` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `35514`

- fail count: `0`

- all data: `16934,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                        |
|--------------------|---------------------------------------------------------------------------------|
| name               | `global information`                                                            |
| request count      | all = `35514`, ok = `35514`, RPS = `296`                                        |
| latency            | min = `10,31` ms, mean = `688,95` ms, max = `6298,66` ms, StdDev = `227,83`     |
| latency percentile | p50 = `564,74` ms, p75 = `894,98` ms, p95 = `1055,74` ms, p99 = `1120,26` ms    |
| data transfer      | min = `488,291` KB, mean = `488,375` KB, max = `488,291` KB, all = `16934,7` MB |

## Node.js server - 100 data points
### 1 thread
#### gRPC
scenario: `scenario_grpc`

- ok count: `332686`

- fail count: `0`

- all data: `1205,6` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `332686`, ok = `332686`, RPS = `2772,4`                            |
| latency            | min = `0,22` ms, mean = `0,36` ms, max = `2053,69` ms, StdDev = `5,03`   |
| latency percentile | p50 = `0,31` ms, p75 = `0,34` ms, p95 = `0,46` ms, p99 = `0,69` ms       |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `1205,6` MB |

#### /bars
scenario: `scenario_http`

- ok count: `47521`

- fail count: `0`

- all data: `227,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `47521`, ok = `47521`, RPS = `396`                                |
| latency            | min = `2,08` ms, mean = `2,51` ms, max = `49,97` ms, StdDev = `0,62`    |
| latency percentile | p50 = `2,4` ms, p75 = `2,56` ms, p95 = `3,18` ms, p99 = `4` ms          |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `227,1` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `49620`

- fail count: `0`

- all data: `237,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `49620`, ok = `49620`, RPS = `413,5`                              |
| latency            | min = `2,02` ms, mean = `2,41` ms, max = `28,77` ms, StdDev = `0,55`    |
| latency percentile | p50 = `2,31` ms, p75 = `2,44` ms, p95 = `2,93` ms, p99 = `3,88` ms      |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `237,1` MB |

### 50 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `1204408`

- fail count: `0`

- all data: `4364,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `1204408`, ok = `1204408`, RPS = `10036,7`                         |
| latency            | min = `0,23` ms, mean = `4,98` ms, max = `2121,04` ms, StdDev = `14,01`  |
| latency percentile | p50 = `4,14` ms, p75 = `4,94` ms, p95 = `9,33` ms, p99 = `15,18` ms      |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `4364,7` MB |

#### /bars
scenario: `scenario_http`

- ok count: `193460`

- fail count: `0`

- all data: `924,3` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `193460`, ok = `193460`, RPS = `1612,2`                           |
| latency            | min = `2,72` ms, mean = `30,97` ms, max = `198,55` ms, StdDev = `13,66` |
| latency percentile | p50 = `23,6` ms, p75 = `46,24` ms, p95 = `54,66` ms, p99 = `62,85` ms   |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1612,3` MB |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `187038`

- fail count: `0`

- all data: `893,7` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `50`, during: `00:02:00`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `187038`, ok = `187038`, RPS = `1558,6`                           |
| latency            | min = `2,49` ms, mean = `32,05` ms, max = `568,4` ms, StdDev = `17,21`  |
| latency percentile | p50 = `24,54` ms, p75 = `47,42` ms, p95 = `57,57` ms, p99 = `70,08` ms  |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `1558,7` MB |

### 200 threads
#### gRPC
scenario: `scenario_grpc`

- ok count: `1207284`

- fail count: `0`

- all data: `4375,2` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `1207284`, ok = `1207284`, RPS = `10060,7`                         |
| latency            | min = `0,48` ms, mean = `19,87` ms, max = `2251,78` ms, StdDev = `30,93` |
| latency percentile | p50 = `15,79` ms, p75 = `20,67` ms, p95 = `37,02` ms, p99 = `50,37` ms   |
| data transfer      | min = `3,711` KB, mean = `3,712` KB, max = `3,711` KB, all = `4375,2` MB |

#### /bars
scenario: `scenario_http`

- ok count: `199489`

- fail count: `0`

- all data: `953,1` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                  |
|--------------------|---------------------------------------------------------------------------|
| name               | `global information`                                                      |
| request count      | all = `199489`, ok = `199489`, RPS = `1662,4`                             |
| latency            | min = `3,46` ms, mean = `120,18` ms, max = `1667,91` ms, StdDev = `80,16` |
| latency percentile | p50 = `85,63` ms, p75 = `178,43` ms, p95 = `216,96` ms, p99 = `249,47` ms |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `953,1` MB   |

#### /bars-static
scenario: `scenario_http_static`

- ok count: `197452`

- fail count: `0`

- all data: `943,4` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `200`, during: `00:02:00`

| step               | ok stats                                                                  |
|--------------------|---------------------------------------------------------------------------|
| name               | `global information`                                                      |
| request count      | all = `197452`, ok = `197452`, RPS = `1645,4`                             |
| latency            | min = `2,7` ms, mean = `121,45` ms, max = `776,74` ms, StdDev = `63,12`   |
| latency percentile | p50 = `84,99` ms, p75 = `194,05` ms, p95 = `217,73` ms, p99 = `243,46` ms |
| data transfer      | min = `4,893` KB, mean = `4,893` KB, max = `4,893` KB, all = `943,4` MB   |
