var http = require('http');
var url = require('url');
var fs = require('fs');

var server = http.createServer(function(request, response){
	var path = url.parse(request.url).pathname;

	switch (path){
		case '/test':
			response.writeHead(200, {'Content-Type':'text/plain'});
			response.write('It works\n');
			response.end();

			break;

		case '/':
			fs.readFile(__dirname + '/index.html', function(err, data) {
				if (err) return (send404(response));

				response.writeHead(200, {'Content-Type':'text/html'});
				response.write(data, 'utf8');
				response.end();
			});

			break;

		default: 
			send404(response);
	}
});

send404 = function(response){
	response.writeHead(404);
	response.write('404');
	response.end();
};

server.listen(8080);
