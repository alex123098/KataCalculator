namespace KataFSharp.Tests

open System
open Xunit

module Infrastructure =
    
    let toString v = v.ToString()

    let combineString (delimiter: string) (items: 'a seq) =
        String.Join(delimiter, items)

    let shouldThrowException (exceptionType: Type) (func: unit -> unit) =
        try
            func()
            Assert.True(false, sprintf "Expected exception of type %A. But no exception was thrown." exceptionType)
        with
            | e when e.GetType() <> exceptionType ->
                Assert.True(false, sprintf "Expected exception of type %A but %A was thrown." exceptionType (e.GetType()))
            | _ -> ()
