namespace Aoc2020

open System
open System.Text
open System.Text.RegularExpressions
open Utils

module Day01 =

    let find2020 (numbers:int array) (number:int)  =
        let inverse = 2020 - number
        if numbers |> Array.contains inverse
        then Some (number, inverse)
        else None

    let find2020_3 (numbers:int array) =
        numbers 
        |> Array.allPairs numbers 
        |> Array.allPairs numbers 
        |> Array.tryFind (fun (x, (y, z)) -> x + y + z = 2020)

    let run () =
        let lines = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day01.txt"
        let numbers = lines |> Array.map Int32.Parse 
        let result = numbers |> Array.tryPick (find2020 numbers)
        let result2 = find2020_3 numbers
        let s1 = match result with
                 | Some (n,m) -> n * m
                 | None -> 0
                 |> string
        let s2 = match result2 with
                 | Some (x, (y, z)) -> x * y * z
                 | None -> 0
                 |> string
        string (s1,s2)