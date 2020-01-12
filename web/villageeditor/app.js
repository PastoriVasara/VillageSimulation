var express = require('express');
var http = require('http');
var path = require('path');
var bodyParser = require('body-parser');
var reload = require('reload');


var app = express();
var publicDir = path.join(__dirname, "public");
var fontDir = path.join(__dirname, "node_modules");
fontDir = path.join(fontDir,"@fortawesome");
app.use(express.static(publicDir));
app.use(express.static(fontDir));
app.set('port', process.env.PORT || 3001);
app.use(bodyParser.json());


app.get("/",function(req,res)
{
    res.sendFile(path.join(publicDir, 'index.html'));
})
app.get("/building",function(req,res){
    res.sendFile(path.join(publicDir,'building.html'));
})
app.get("/house",function(req,res){
    res.sendFile(path.join(publicDir,'house.html'));
})
app.get("/person",function(req,res){
    res.sendFile(path.join(publicDir,'person.html'));
})
var server = http.createServer(app);

reload(app).then(function (reloadReturned) {
    server.listen(app.get('port'), function () {
      console.log('Web server listening on port ' + app.get('port'))
    })
  }).catch(function (err) {
    console.error('Reload could not start, could not start server/sample app', err)
  })