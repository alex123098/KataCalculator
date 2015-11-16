namespace KataFSharp

open System

type Calculator() =
    member x.Add (numbers: string) = 
        numbers.Split([| ','; '\n' |], StringSplitOptions.RemoveEmptyEntries)
        |> Seq.map (fun s -> Int32.Parse(s))
        |> Seq.sum
