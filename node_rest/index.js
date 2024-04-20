const http = require('node:http');

const bar100Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 100; i++) {
    bar100Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}

const bar5000Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 5000; i++) {
    bar5000Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}

http
    .createServer((request, response) => {
        const { headers, method, url } = request;
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
                response.write('Hello World');
        }
    })
    .listen(3002);