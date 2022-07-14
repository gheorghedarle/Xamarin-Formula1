const port = 5000;
const securePort = 5001;
var fs = require("fs");
var http = require("http");
var https = require("https");
var express = require("express");
var cheerio = require("cheerio");
var request = require("request-promise");

// var privateKey = fs.readFileSync("sslcert/server.key", "utf8");
// var certificate = fs.readFileSync("sslcert/server.crt", "utf8");

// var credentials = { key: privateKey, cert: certificate };

var app = express();

app.get("/driver/info", async function (req, res) {
  if (req.query.driver != null) {
    var r = await scrapeDriverInfo(req.query.driver);
    res.end(JSON.stringify(r));
  }
});

async function scrapeDriverInfo(driver) {
  var res = {};
  const result = await request.get(
    encodeURI(`https://www.formula1.com/en/drivers/${driver}.html`)
  );
  const $ = cheerio.load(result);
  $(
    "body > div.site-wrapper > main > article > div > header > section.stats > div > table > tbody > tr"
  ).each((index, element) => {
    const label = $(element).find("th > span").text();
    const value = $(element).find("td").text();
    res[label] = value;
  });
  return res;
}

app.get("/team/info", async function (req, res) {
  if (req.query.team != null) {
    var r = await scrapeTeamInfo(req.query.team);
    res.end(JSON.stringify(r));
  }
});

async function scrapeTeamInfo(team) {
  var res = {};
  const result = await request.get(
    encodeURI(`https://www.formula1.com/en/teams/${team}.html`)
  );
  const $ = cheerio.load(result);
  $(
    "body > div.site-wrapper > main > article > div > header.team-details > section.stats > div > table > tbody > tr"
  ).each((index, element) => {
    const label = $(element).find("th > span").text();
    const value = $(element).find("td").text();
    res[label] = value;
  });
  return res;
}

var httpServer = http.createServer(app);
// var httpsServer = https.createServer(credentials, app);

httpServer.listen(port);
// httpsServer.listen(securePort);
