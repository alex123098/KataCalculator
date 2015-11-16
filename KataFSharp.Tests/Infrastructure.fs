namespace KataFSharp.Tests

open System

module Infrastructure =
    
    let toString v = v.ToString()

    let combineString (delimiter: string) (items: 'a seq) =
        String.Join(delimiter, items)