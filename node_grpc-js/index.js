const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const port = 3004;

const bar100Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 100; i++) {
    bar100Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}

const bar5000Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 5000; i++) {
    bar5000Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}


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
const server = new grpc.Server();
server.addService(simple_proto.BarService.service, {
    GetBar100: getBar100,
    GetBar5000: getBar5000
});
server.bindAsync(`0.0.0.0:${port}`, grpc.ServerCredentials.createInsecure(), (err, port) => {
    if (err != null) {
        return console.error(err);
    }
    console.log(`gRPC listening on port ${port}`)
});

function getBar100(call, callback) {
    callback(null, bar100Response);
}

function getBar5000(call, callback) {
    callback(null, bar5000Response);
}
