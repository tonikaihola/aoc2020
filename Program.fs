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
                | _ -> "enter day number"
        printfn "%s" result
        0 // return an integer exit code