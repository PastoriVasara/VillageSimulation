var express = require('express');
var http = require('http');
var path = require('path');
var bodyParser = require('body-parser');
var reload = require('reload');
var fs = require('fs');


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
app.post("/building",function(req,res){
    console.log(req.body);
    fs.writeFile(path.join(publicDir, 'building.json'),JSON.stringify(req.body, null, 4),function(err){
        if(err){
        res.statusCode = 404;
        res.end();
        }
        else{
            res.statusCode = 200;
            res.end();
        }
    });

});
app.get("/house",function(req,res){
    res.sendFile(path.join(publicDir,'house.html'));
})
app.post("/house",function(req,res){
    console.log(req.body);
    fs.writeFile(path.join(publicDir, 'house.json'),JSON.stringify(req.body, null, 4),function(err){
        if(err){
        res.statusCode = 404;
        res.end();
        }
        else{
            res.statusCode = 200;
            res.end();
        }
    });

});
app.get("/person",function(req,res){
    res.sendFile(path.join(publicDir,'person.html'));
})
app.post("/person",function(req,res){
    console.log(req.body);
    fs.writeFile(path.join(publicDir, 'person.json'),JSON.stringify(req.body, null, 4),function(err){
        if(err){
        res.statusCode = 404;
        res.end();
        }
        else{
            res.statusCode = 200;
            res.end();
        }
    });

});
var server = http.createServer(app);

reload(app).then(function (reloadReturned) {
    server.listen(app.get('port'), function () {
      console.log('Web server listening on port ' + app.get('port'))
    })
  }).catch(function (err) {
    console.error('Reload could not start, could not start server/sample app', err)
  })