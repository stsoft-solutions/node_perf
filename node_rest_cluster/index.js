const cluster = require('node:cluster');
const http = require('node:http');
const os = require('node:os');
const process = require('node:process');

const bar = {open: 132.23, high: 4324, low: 433, close: 432}
const symbol = 'AAPL';

const bar100Response = { bars: Array(100).fill(bar), symbol };
const bar5000Response = { bars: Array(5000).fill(bar), symbol };

if (cluster.isPrimary) {
  console.log(`Primary ${process.pid} is running`);
  const numCPUs = os.cpus().length;
  for (let i = 0; i < numCPUs; i++) {
    cluster.fork();
  }
  cluster.on('exit', (worker, code, signal) => {
    console.log(`worker ${worker.process.pid} died`);
  });
} else {
  http
  .createServer((request, response) => {
      const { url } = request;
      response.setHeader('Content-Type', 'application/json');
      response.statusCode = 200;
      switch (url) {
          case '/bar/100':
              response.end(JSON.stringify(bar100Response));
              break;
          case '/bar/5000':
              response.end(JSON.stringify(bar5000Response));
              break;
          default:
              response.end('Hello World');
      }
  })
  .listen(3002, () => {
    console.log(`Worker ${process.pid} started and listenes port 3002`);
  });
}
