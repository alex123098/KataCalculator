namespace KataFSharp

open System

type Calculator() =
    member x.Add (numbers: string) = 
        numbers.Split([| ',' |], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun s -> Int32.Parse(s))
        |> Seq.sum
