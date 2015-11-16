namespace KataFSharp

open System

type Calculator() =
    
    let combineString (delimiter: string) (items: 'a seq) =
        String.Join(delimiter, items)

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

    let checkForNonNegative (numbers: int seq) =
        let negativeNumbers =
            numbers
            |> Seq.filter (fun n -> n < 0)
        if negativeNumbers |> Seq.isEmpty then
            numbers
        else
            let message = sprintf "Negatives not allowed. Found %s." (negativeNumbers |> combineString ",")
            invalidArg "numbers" message

    member x.Add (numbers: string) = 
        numbers
        |> separate
        |> Seq.map (fun s -> Int32.Parse(s))
        |> checkForNonNegative
        |> Seq.filter (fun n -> n <= 1000)
        |> Seq.sum
