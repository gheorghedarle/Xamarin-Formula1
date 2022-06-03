const port = 4000;
const securePort = 4001;
var fs = require("fs");
var http = require("http");
var https = require("https");
var express = require("express");

// var privateKey = fs.readFileSync("sslcert/server.key", "utf8");
// var certificate = fs.readFileSync("sslcert/server.crt", "utf8");

// var credentials = { key: privateKey, cert: certificate };

var app = express();
app.use(express.static("public"));

var httpServer = http.createServer(app);
// var httpsServer = https.createServer(credentials, app);

httpServer.listen(port);
// httpsServer.listen(securePort);
