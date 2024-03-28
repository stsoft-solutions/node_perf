const express = require('express')
const grpc = require('@grpc/grpc-js');
const protoLoader = require('@grpc/proto-loader');
const nocache = require("nocache");

const app = express()
const port = 3000

// Prepare bars
const barsResponse = { bars: [] };
for (let i = 0; i < 10000; i++) {
    barsResponse.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}
const barsResponseStatic = JSON.stringify(barsResponse);

app.use(nocache());

// Add REST endpoints
app.get('/', (req, res) => {
    res.send('Hello World!')
})
app.get('/bars', (req, res) => {
    res.json(barsResponse);
})

app.get('/bars-static', (req, res) => {
    res.send(barsResponseStatic);
})


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
server.addService(simple_proto.BarService.service, {getBars: getBars});
server.bindAsync('0.0.0.0:50051', grpc.ServerCredentials.createInsecure(), (err, port) => {
    if (err != null) {
        return console.error(err);
    }
    console.log(`gRPC listening on port ${port}`)
});

function getBars(call, callback) {
    callback(null, barsResponse);
}

app.listen(port, () => {
    console.log(`REST listening on port ${port}`)
})

