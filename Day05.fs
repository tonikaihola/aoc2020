namespace Aoc2020

open System
open System.Text.RegularExpressions
open System.Threading.Tasks

#if INTERACTIVE
#r "nuget:FParsec"
#endif
open FParsec

module Day05 =

    type Seat =
        { Row:int
          Col:int
          SID:int }

    let solveSeat (input:string) = 

        let upper (low,high) =
            (high + low) / 2 + (high % 2), high

        let lower (low,high) =
            low, (high + low) / 2

        let rec loop cs rowpart colpart =
            match cs with
            | [] -> 
                let row = fst rowpart
                let col = fst colpart
                let sid = (8 * row) + col
                { Row = row; Col = col; SID = sid } 
            | c :: cs -> 
                let row,col = 
                    match c with
                    | 'B' -> upper rowpart, colpart
                    | 'F' -> lower rowpart, colpart
                    | 'L' -> rowpart, lower colpart
                    | 'R' -> rowpart, upper colpart
                    |  c  -> raise (exn $"unknown character '{c}'")
                loop cs row col

        let cs = input.ToCharArray() |> List.ofArray
        let seat = loop cs (0,127) (0,7) 
        seat
        
    let run () =
        let seatCodes = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day05.txt"
        let seats = seatCodes |> Array.map solveSeat
        let maxId = seats |> Array.maxBy (fun s -> s.SID)
        maxId.SID

module Day05Tests = 

    open Expecto
    open Day05
    (* Test data
        BFFFBBFRRR: row 70, column 7, seat ID 567.
        FFFBBBFRRR: row 14, column 7, seat ID 119.
        BBFFBBFRLL: row 102, column 4, seat ID 820.
    *)
    let tests =
      testList "A test group" [
        test "BFFFBBFRRR" {
          let expected =  { Row = 70; Col = 7; SID = 567 }
          Expect.equal (solveSeat "BFFFBBFRRR") expected "xxx"
        }
        test "FFFBBBFRRR" {
          let expected =  { Row = 14; Col = 7; SID = 119 }
          Expect.equal (solveSeat "FFFBBBFRRR") expected "xxx"
        }
        test "BBFFBBFRLL" {
          let expected =  { Row = 102; Col = 4; SID = 820 }
          Expect.equal (solveSeat "BBFFBBFRLL") expected "xxx"
        }
      ]
      |> testLabel "samples"

    let run () = Tests.runTests defaultConfig tests

