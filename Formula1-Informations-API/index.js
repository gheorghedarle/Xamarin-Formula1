const port = 5000;
const securePort = 5001;
var fs = require("fs");
var http = require("http");
var https = require("https");
var express = require("express");
var cheerio = require("cheerio");
var axios = require("axios");

// var privateKey = fs.readFileSync("sslcert/server.key", "utf8");
// var certificate = fs.readFileSync("sslcert/server.crt", "utf8");

// var credentials = { key: privateKey, cert: certificate };

var app = express();

app.get("/driver/info", async function (req, res) {
  if (req.query.driver != null) {
    console.log(req.query.driver);
    axios
      .get(`https://www.formula1.com/en/drivers/${req.query.driver}.html`)
      .then((result) => {
        var r = {};
        const $ = cheerio.load(result.data);
        $(
          "body > div.site-wrapper > main > article > div > header > section.stats > div > table > tbody > tr"
        ).each((index, element) => {
          const label = $(element).find("th > span").text();
          const value = $(element).find("td").text();
          r[camelize(label)] = value;
        });
        res.end(JSON.stringify(r));
      })
      .catch((err) => console.error(err));
  }
});

app.get("/team/info", async function (req, res) {
  if (req.query.team != null) {
    console.log(req.query.team);
    axios
      .get(`https://www.formula1.com/en/teams/${req.query.team}.html`)
      .then((result) => {
        var r = {};
        const $ = cheerio.load(result.data);
        $(
          "body > div.site-wrapper > main > article > div > header.team-details > section.stats > div > table > tbody > tr"
        ).each((index, element) => {
          const label = $(element).find("th > span").text();
          const value = $(element).find("td").text();
          r[camelize(label)] = value;
        });
        res.end(JSON.stringify(r));
      })
      .catch((err) => console.error(err));
  }
});

function camelize(str) {
  return str.replace(/(?:^\w|[A-Z]|\b\w|\s+)/g, function (match, index) {
    if (+match === 0) return ""; // or if (/\s+/.test(match)) for white spaces
    return index === 0 ? match.toLowerCase() : match.toUpperCase();
  });
}

var httpServer = http.createServer(app);
// var httpsServer = https.createServer(credentials, app);

httpServer.listen(port);
// httpsServer.listen(securePort);
