const express = require('express')
const cluster = require('node:cluster');
const os = require('node:os');
const process = require('node:process');

const bar = {open: 132.23, high: 4324, low: 433, close: 432}
const symbol = 'AAPL';

const bar100Response = { bars: Array(100).fill(bar), symbol };
const bar5000Response = { bars: Array(5000).fill(bar), symbol };
const port = 3010

if (cluster.isPrimary) {
  console.log(`Primary ${process.pid} is running`);
  let numCPUs = os.availableParallelism();
  console.log(`numCPUs: ${numCPUs}`);
  numCPUs = 17;
  for (let i = 0; i < numCPUs; i++) {
    cluster.fork();
  }
  cluster.on('exit', (worker, code, signal) => {
    console.log(`worker ${worker.process.pid} died`);
  });
} else {
  const app = express();
  app.get('/bar/100', (req, res) => {
    res.json(bar100Response)
  });
  app.get('/bar/5000', (req, res) => {
    res.json(bar5000Response)
  });
  app.get('*', (req, res) => {
    res.send('Hello World');
  })
  app.listen(port, () => {
    console.log(`Express server stsrted in Worker ${process.pid} and listenes port ${port}`);
  })
}