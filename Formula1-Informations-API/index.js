const port = 5000;
const securePort = 5001;
var http = require("http");
var https = require("https");
var express = require("express");
var cheerio = require("cheerio");
var axios = require("axios");

const helpers = require("./helpers/helpers");

var baseUrl = "https://www.formula1.com/en";
var app = express();

app.get("/driver/info", async function (req, res) {
  if (req.query.driver != null) {
    console.log(`/driver/info - ${req.query.driver}`);
    axios
      .get(`${baseUrl}/drivers/${req.query.driver}.html`)
      .then((result) => {
        var r = {};
        const $ = cheerio.load(result.data);
        $(
          "body > div.site-wrapper > main > article > div > header > section.stats > div > table > tbody > tr"
        ).each((index, element) => {
          const label = $(element).find("th > span").text();
          const value = $(element).find("td").text();
          r[helpers.camelize(label)] = value;
        });
        var finalResult = { result: r };
        res.status(200).end(JSON.stringify(finalResult));
      })
      .catch((err) => {
        console.log(err);
        res.status(400).end();
      });
  }
});

app.get("/team/info", async function (req, res) {
  if (req.query.team != null) {
    console.log(`/team/info - ${req.query.team}`);
    axios
      .get(`${baseUrl}/teams/${helpers.convertToTeamName(req.query.team)}.html`)
      .then((result) => {
        var r = {};
        const $ = cheerio.load(result.data);
        $(
          "body > div.site-wrapper > main > article > div > header.team-details > section.stats > div > table > tbody > tr"
        ).each((index, element) => {
          const label = $(element).find("th > span").text();
          const value = $(element).find("td").text();
          r[helpers.camelize(label)] = value;
        });
        var finalResult = { result: r };
        res.status(200).end(JSON.stringify(finalResult));
      })
      .catch((err) => {
        console.log(err);
        res.status(400).end();
      });
  }
});

app.get("/circuit/info", async function (req, res) {
  if (req.query.country != null) {
    console.log(`/circuit/info - ${req.query.country}`);
    axios
      .get(`${baseUrl}/racing/2022/${req.query.country}/Circuit.html`)
      .then((result) => {
        var r = {};
        const $ = cheerio.load(result.data);
        $(
          "body > div.site-wrapper > main > div > div.racehub-page > div.racehub-page-section > div > div > div.f1-race-hub--map > fieldset > div > div.col-xl-5 > div > div.col-lg-8.col-xl-12 > div > div"
        ).each((index, element) => {
          const label = $(element).find("div > p.misc--label").text();
          const value = $(element).find("div > p.f1-bold--stat").text();
          r[helpers.camelize(label)] = value;
        });
        var finalResult = { result: r };
        res.status(200).end(JSON.stringify(finalResult));
      })
      .catch((err) => {
        console.log(err);
        res.status(400).end();
      });
  }
});

var httpServer = http.createServer(app);
httpServer.listen(port);

// var httpsServer = https.createServer(credentials, app);
// httpsServer.listen(securePort);
