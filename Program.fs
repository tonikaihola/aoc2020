namespace Aoc2020

open System

module Main =

    [<EntryPoint>]
    let main argv =
        let result = 
            if argv.Length = 0 
            then 
                "enter day number"
            else
                match argv.[0] with
                | "1" -> Day01.run () |> sprintf "Product of 2020: %s"
                | "2" -> Day02.run () |> sprintf "Valid pwds: %A"
                | "3" -> Day03.run () |> sprintf "Tree count: %s"
                | "4" -> 
                    let passports,validCount,invalidCount = Day04.run ()
                    let testResult = Day04Tests.test ()
                    let newline = Environment.NewLine
                    sprintf "Passports: %A%s// Valid: %i , Invalid: %i %s// Test valid/invalid OK: %A" 
                            passports newline validCount invalidCount newline testResult
                | _ -> "enter day number"
        printfn "%s" result
        0 // return an integer exit code