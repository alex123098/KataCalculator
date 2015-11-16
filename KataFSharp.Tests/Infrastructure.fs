namespace KataFSharp.Tests

open System
open Xunit

module Infrastructure =
    
    let toString v = v.ToString()

    let combineString (delimiter: string) (items: 'a seq) =
        String.Join(delimiter, items)

    let shouldThrow (exceptionType: Type) (func: unit -> unit) =
        try
            func()
            Assert.True(false, sprintf "Expected exception of type %A. But no exception was thrown." exceptionType)
        with
            | e when e.GetType() <> exceptionType ->
                Assert.True(false, sprintf "Expected exception of type %A but %A was thrown." exceptionType (e.GetType()))
            | _ -> ()

    let shouldThrowWithMessage (exceptionType: Type) (message: string) (func: unit -> unit) =
        try
            func()
            Assert.True(false, sprintf "Expected exception of type %A. But no exception was thrown." exceptionType)
        with
            | e when e.GetType() <> exceptionType ->
                Assert.True(false, sprintf "Expected exception of type %A but %A was thrown." exceptionType (e.GetType()))
            | e when not (e.Message.StartsWith(message)) ->
                Assert.True(false, sprintf "Expected exception message to be \"%s\" but actual was \"%s\"." message e.Message)
            | _ -> ()
