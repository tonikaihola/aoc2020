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

    let run () =
        let lines = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day01.txt"
        let numbers = lines |> Array.map Int32.Parse 
        let result = numbers |> Array.tryPick (find2020 numbers)
        match result with
        | Some (n,m) -> n * m
        | None -> 0
        |> string