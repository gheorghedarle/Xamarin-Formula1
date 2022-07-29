const port = 4000;
var http = require("http");
var express = require("express");

var app = express();
app.use(express.static("public"));

var httpServer = http.createServer(app);
httpServer.listen(port);
