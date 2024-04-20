const port = 3003
const express = require('express')
const app = express();


const bar100Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 100; i++) {
    bar100Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}

const bar5000Response = { bars: [], symbol: 'AAPL'};
for (let i = 0; i < 5000; i++) {
    bar5000Response.bars.push({open: 132.23, high: 4324, low: 433, close: 432});
}

app.get('/bar/100', (req, res) => {
    res.json(bar100Response)
});

app.get('/bar/5000', (req, res) => {
    res.json(bar5000Response)
});

app.listen(port, () => {
    console.log(`REST listening on port ${port}`)
})
