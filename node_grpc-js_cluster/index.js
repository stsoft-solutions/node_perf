const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const cluster = require('node:cluster');
const http = require('node:http');
const os = require('node:os');
const process = require('node:process');

const bar = {open: 132.23, high: 4324, low: 433, close: 432}
const symbol = 'AAPL';

const bar100Response = { bars: Array(100).fill(bar), symbol };
const bar5000Response = { bars: Array(5000).fill(bar), symbol };

function getBar100(call, callback) {
    callback(null, bar100Response);
}
function getBar5000(call, callback) {
    callback(null, bar5000Response);
}

const port = 3008;

// Prepare gRPC
const PROTO_PATH = __dirname + '/../proto/simple.proto';
const packageDefinition = protoLoader.loadSync(
    PROTO_PATH,
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    });
const simple_proto = grpc.loadPackageDefinition(packageDefinition).simple;

if (cluster.isPrimary) {
    console.log(`Primary ${process.pid} is running`);
    let numCPUs = os.availableParallelism();;
    console.log(`numCPUs: ${numCPUs}. os.cpus: ${os.cpus().length}`);
    numCPUs = 17;
    for (let i = 0; i < numCPUs; i++) {
      cluster.fork();
    }
    cluster.on('exit', (worker, code, signal) => {
      console.log(`worker ${worker.process.pid} died`);
    });
  } else {
    const server = new grpc.Server();
    server.addService(simple_proto.BarService.service, {
        GetBar100: getBar100,
        GetBar5000: getBar5000
    });
    server.bindAsync(`0.0.0.0:${port}`, grpc.ServerCredentials.createInsecure(), (err, port) => {
        if (err != null) {
            return console.error(err);
        }
        console.log(`gRPC server started on Worker ${process.pid} and listening on port ${port}`)
    });
}
