namespace Aoc2020

open System
open System.Text
open System.Text.RegularExpressions
open Utils

module Day03 =

    let rec ride (lines:string array) (row:int) (col:int) (count:int) slopeRows slopeCols =
        if row >= lines.Length
        then count
        else 
            let line = lines.[row]
            printfn $"Index row: {row} / col: {col}"
            let count = 
                if line.[col] = '#' 
                then count + 1
                else count
            let row = row + slopeRows
            let col = 
                if col + slopeCols >= line.Length
                then col + slopeCols - line.Length
                else col + slopeCols
            ride lines row col count slopeRows slopeCols 

    let run () =
        let lines = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day03.txt"
        let slopes =
            [ 
                1, 1
                1, 3
                1, 5
                1, 7
                2, 1
            ]
        let ride = ride lines 0 0 0
        let slopeTrees = slopes |> List.map (fun (rows,cols) -> ride rows cols)
        let total = slopeTrees |> List.reduce (*)
        string total