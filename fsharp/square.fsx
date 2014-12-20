// define the square function
let square x = x * x

// define the sumOfSquares function
let sumOfSquares n = 
   [1..n] |> List.map square |> List.sum

// try it
sumOfSquares 100