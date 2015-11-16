namespace KataFSharp

open System

type Calculator() =
    
    let charsToString (chars: char seq) =
        String(chars |> Seq.toArray)

    let split (separators: char array) (str: string) =
        str.Split(separators, StringSplitOptions.RemoveEmptyEntries)

    let getSeparators (numbers: string) =
        if numbers.StartsWith("//") then
            numbers
            |> Seq.skip 2
            |> Seq.take 1
            |> Seq.append ('\n' |> Seq.singleton)
            |> Seq.toArray
        else
            [| ','; '\n' |]

    let skipPrologue (numbers: string) =
        if numbers.StartsWith("//") then
            numbers
            |> Seq.skipWhile (fun c -> c <> '\n')
            |> Seq.skip 1
            |> charsToString
        else
            numbers

    let separate (numbers: string) =
        numbers
        |> skipPrologue
        |> split (numbers |> getSeparators)

    member x.Add (numbers: string) = 
        numbers
        |> separate
        |> Seq.map (fun s -> Int32.Parse(s))
        |> Seq.sum
