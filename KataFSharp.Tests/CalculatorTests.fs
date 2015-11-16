namespace KataFSharp.Tests

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

    [<Theory>]
    [<InlineData(18, 6, 32)>]
    [<InlineData(8, 16, 23)>]
    let ``Given three items separated with comma and line break should return sum of them``(x: int, y: int, z: int) =
        let calc = Calculator()
        let numbers = sprintf "%d\n%d,%d" x y z
        calc.Add numbers |> should equal (x + y + z)

    [<Theory>]
    [<InlineData(65, 2, ']')>]
    [<InlineData(35, 21, '\\')>]
    let ``Calculator should support different delimiters``(x: int, y: int, d: char) =
        let calc = Calculator()
        let numbersOnlyString = [| x; y|] |> combineString (d |> toString)
        let numbers = sprintf "//%c\n%s" d numbersOnlyString
        calc.Add numbers |> should equal (x + y)

    [<Theory>]
    [<InlineData(-1, 2)>]
    [<InlineData(1, -2)>]
    [<InlineData(-1, -2)>]
    let ``Given negative number should throw exception``(x: int, y: int) =
        let calc = Calculator()
        let numbers = [| x; y |] |> combineString ","
        (fun () -> calc.Add numbers |> ignore) |> shouldThrow typeof<ArgumentException>

    [<Theory>]
    [<InlineData(-1, 2, "-1")>]
    [<InlineData(1, -2, "-2")>]
    [<InlineData(-1, -2, "-1,-2")>]
    let ``Negative number provided should be mentioned in exception message``(x: int, y: int, msg: string) =
        let calc = Calculator()
        let numbers = [| x; y |] |> combineString ","
        (fun () -> calc.Add numbers |> ignore) 
        |> shouldThrowWithMessage typeof<ArgumentException> (sprintf "Negatives not allowed. Found %s." msg)

    [<Theory>]
    [<InlineData(10, 1020, 10)>]
    [<InlineData(1040, 120, 120)>]
    let ``Numbers greater than 1000 should be ignored``(x: int, y: int, expected: int) =
        let calc = Calculator()
        let numbers = [| x; y |] |> combineString ","
        calc.Add numbers |> should equal expected
