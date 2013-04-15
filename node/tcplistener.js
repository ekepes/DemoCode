var net = require('net');

var client_ctr = 0;

var server = net.createServer(function(socket){
	client_ctr++;
	console.log("Client " + client_ctr + " connected");
	socket.write("Connected to server.\r\n");

	socket.on('data', function(data) {
		possibleCommand = data.toString('utf8').replace(/\r\n/, '');
		switch (possibleCommand){
			case "disconnect":
				console.log(socket.remoteAddress + ":" +
					socket.remotePort +
					" ended their session.");
				socket.end();
				break;
			case "server address":
				address = server.address();
				socket.write("address: " + address.address 
					+ "  port: " + address.port + "\r\n"); 
				break;
			default:
				socket.write(data);
		}
 	});
});

server.listen(8080, "127.0.0.1");

// to use - telnet 127.0.0.1 8080