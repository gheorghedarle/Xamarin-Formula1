const port = 4000;
const securePort = 4001;
var http = require("http");
var https = require("https");
var express = require("express");

var app = express();
app.use(express.static("public"));

var httpServer = http.createServer(app);
httpServer.listen(port);

// var httpsServer = https.createServer(credentials, app);
// httpsServer.listen(securePort);
