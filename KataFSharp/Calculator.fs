namespace KataFSharp

open System

type Calculator() =
    member x.Add numbers = 
        if numbers <> "" then
            numbers.Split(',')
            |> Seq.map (fun s -> Int32.Parse(s))
            |> Seq.sum
        else
            0
