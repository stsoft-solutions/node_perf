<!-- TOC -->
* [Node.js](#nodejs)
  * [1 Bar](#1-bar)
    * [gRPC](#grpc)
    * [HTTP](#http)
    * [HTTP Static](#http-static-)
  * [Bars - 10000](#bars---10000)
    * [gRPC](#grpc-1)
    * [HTTP](#http-1)
    * [HTTP static](#http-static)
* [.Net 8](#net-8)
<!-- TOC -->

# Node.js
## 1 Bar
### gRPC
> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.15.31_session_6cbf2124`

> scenario stats

scenario: `scenario_grpc`

- ok count: `2633025`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                                |
|--------------------|-------------------------------------------------------------------------|
| name               | `global information`                                                    |
| request count      | all = `2633025`, ok = `2633025`, RPS = `21941,9`                        |
| latency            | min = `0,08` ms, mean = `1,26` ms, max = `2042,65` ms, StdDev = `12,34` |
| latency percentile | p50 = `1,15` ms, p75 = `1,38` ms, p95 = `2,65` ms, p99 = `4,76` ms      |


### HTTP
> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.24.78_session_3392c375`

> scenario stats

scenario: `scenario_http`

- ok count: `1477440`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                             |
|--------------------|----------------------------------------------------------------------|
| name               | `global information`                                                 |
| request count      | all = `1477440`, ok = `1477440`, RPS = `12312`                       |
| latency            | min = `0,06` ms, mean = `2,25` ms, max = `17,37` ms, StdDev = `1,57` |
| latency percentile | p50 = `2,44` ms, p75 = `3,47` ms, p95 = `4,58` ms, p99 = `5,68` ms   |

### HTTP Static 
> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.29.95_session_a2e60a17`

> scenario stats

scenario: `scenario_http_static`

- ok count: `1478316`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                             |
|--------------------|----------------------------------------------------------------------|
| name               | `global information`                                                 |
| request count      | all = `1478316`, ok = `1478316`, RPS = `12319,3`                     |
| latency            | min = `0,05` ms, mean = `2,25` ms, max = `16,37` ms, StdDev = `1,65` |
| latency percentile | p50 = `2,37` ms, p75 = `3,57` ms, p95 = `4,75` ms, p99 = `5,9` ms    |


## Bars - 10000

### gRPC

> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.34.00_session_4bd4e2d5`

> scenario stats

scenario: `scenario_grpc`

- ok count: `35802`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                                  |
|--------------------|---------------------------------------------------------------------------|
| name               | `global information`                                                      |
| request count      | all = `35802`, ok = `35802`, RPS = `298,4`                                |
| latency            | min = `2,34` ms, mean = `92,95` ms, max = `2246,27` ms, StdDev = `103,71` |
| latency percentile | p50 = `99,46` ms, p75 = `146,18` ms, p95 = `187,9` ms, p99 = `216,19` ms  |


### HTTP
> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.38.61_session_86e1326e`

> scenario stats

scenario: `scenario_http`

- ok count: `26158`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                                   |
|--------------------|----------------------------------------------------------------------------|
| name               | `global information`                                                       |
| request count      | all = `26158`, ok = `26158`, RPS = `218`                                   |
| latency            | min = `6,26` ms, mean = `127,06` ms, max = `342,83` ms, StdDev = `54,72`   |
| latency percentile | p50 = `136,96` ms, p75 = `170,88` ms, p95 = `191,49` ms, p99 = `240,38` ms |


### HTTP static
> test info

test suite: `nbomber_default_test_suite_name`

test name: `nbomber_default_test_name`

session id: `2024-03-27_21.48.82_session_430a36e8`

> scenario stats

scenario: `scenario_http_static`

- ok count: `64919`

- fail count: `0`

- all data: `0` MB

- duration: `00:02:00`

load simulations:

- `keep_constant`, copies: `1`, during: `00:00:30`

- `ramping_constant`, copies: `50`, during: `00:00:30`

- `keep_constant`, copies: `50`, during: `00:00:30`

- `ramping_constant`, copies: `20`, during: `00:00:30`

| step               | ok stats                                                                 |
|--------------------|--------------------------------------------------------------------------|
| name               | `global information`                                                     |
| request count      | all = `64919`, ok = `64919`, RPS = `541`                                 |
| latency            | min = `3,87` ms, mean = `51,44` ms, max = `635,92` ms, StdDev = `52,15`  |
| latency percentile | p50 = `43,39` ms, p75 = `54,98` ms, p95 = `101,25` ms, p99 = `341,76` ms |

# .Net 8
