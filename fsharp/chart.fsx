// Note, on Mac OSX use FSharp.Charting.Gtk.dll.
#r "FSharp.Charting.dll" 

open System
open FSharp.Charting

Chart.Combine(
    [
    Chart.Line([ for x in 0 .. 10 -> x, x ])
    Chart.Line([ for x in 0 .. 10 -> x, x*x ])
    Chart.Line([ for x in 0 .. 10 -> x, x*x*x ]) 
    ]).ShowChart()

printfn "Done. Press Enter..."

Console.ReadLine();

