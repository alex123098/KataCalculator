﻿namespace KataFSharp.Tests

open System
open Xunit
open FsUnit.Xunit
open KataFSharp
open KataFSharp.Tests.Infrastructure

module Calculator =
    
    [<Fact>]
    let ``Given empty string should return 0``() =
        let calc = Calculator()
        calc.Add String.Empty |> should equal 0

    [<Theory>]
    [<InlineData(5)>]
    [<InlineData(6)>]
    [<InlineData(15)>]
    let ``Given one number in string should return this number``(x: int) =
        let calc = Calculator()
        let numbers = x |> toString
        calc.Add numbers |> should equal x

    [<Theory>]
    [<InlineData(15, 6)>]
    [<InlineData(18, 60)>]
    [<InlineData(4, 16)>]
    let ``Given two comma separated numbers should return sum of them``(x: int, y: int) =
        let calc = Calculator()
        let numbers = [| x; y|] |> combineString ","
        calc.Add numbers |> should equal (x + y)

    [<Theory>]
    [<InlineData(15, 2, 3)>]
    [<InlineData(8, 12, 30)>]
    let ``Given three comma separated numbers should return sum of them``(x: int, y: int, z: int) =
        let calc = Calculator()
        let numbers = [| x; y; z |] |> combineString ","
        calc.Add numbers |> should equal (x + y + z)