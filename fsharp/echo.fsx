// Source: https://twitter.com/jonharrop/status/553335574556184576/photo/1

let echo (client: System.Net.Sockets.TcpClient) = async {
    use client = client
    use stream = client.GetStream()
    while true do
        let! b = stream.AsyncRead 1
        do! stream.AsyncWrite b
}

do
    let server = System.Net.Sockets.TcpListener(System.Net.IPAddress.Any, 12321)
    server.Start()
    while true do
        Async.Start(echo(server.AcceptTcpClient()))