namespace Aoc2020

open System
open System.Text
open System.Text.RegularExpressions
open Utils

module Day03 =

    let rec ride slopeRows slopeCols (lines:string array) (row:int) (col:int) (count:int) =
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
            ride slopeRows slopeCols lines row col count

    let run () =
        let lines = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day03.txt"
        let rows, cols = 1, 3
        ride rows cols lines 0 0 0 |> string