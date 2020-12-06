namespace Aoc2020

open System
open Utils

module Day06 =

    let distinctChars (grp:string) = grp.ToCharArray() |> Array.filter Char.IsLetter |> Array.distinct

    let countYezzez text =
        Utils.splitOnEmptyLine text
        |> Array.map (distinctChars >> Array.length)
        |> Array.sum

    let countGroupYezzezAll (grp:string) = 
        let countAnswers (a:char) = if Utils.splitOnLine grp |> Array.forall (fun x -> x.Contains a) then 1 else 0
        distinctChars grp |> Array.sumBy countAnswers

    let countYezzezAll text =
        Utils.splitOnEmptyLine text
        |> Array.map countGroupYezzezAll
        |> Array.sum

    let run () =
        let text = System.IO.File.ReadAllText $"{__SOURCE_DIRECTORY__}/input/day06.txt" 
        let count = countYezzez text, countYezzezAll text
        $"YES count: {count}"

module Day06Tests = 

    open Expecto
    open Day06
    (* 
    Test data

    Correct answers: 
        1) 6686
        2) 3476
    *)

    let testText = """abc

a
b
c

ab
ac

a
a
a
a

b"""

    let testsPartOne =
      testList "A test group" [
        test "abc" {
          Expect.equal (countYezzez "abc") 3 "example"
        }
        test "a b c" {
          let sample = $"a{Environment.NewLine}b{Environment.NewLine}c"
          Expect.equal (countYezzez sample) 3 "example"
        }
        test "ab ac" {
          let sample = $"ab{Environment.NewLine}ac"
          Expect.equal (countYezzez sample) 3 "example"
        }
        test "a a a a" {
          let sample = $"a{Environment.NewLine}a{Environment.NewLine}a{Environment.NewLine}a"
          Expect.equal (countYezzez sample) 1 "example"
        }
        test "b" {
          let sample = $"b"
          Expect.equal (countYezzez sample) 1 "example"
        }
        test "example" {
          Expect.equal (countYezzez testText) 11 "example"
        }
      ]
      |> testLabel "samples"
    
    let tests =
      testList "A test group" [
        test "abc" {
          Expect.equal (countYezzezAll "abc") 3 "example"
        }
        test "a b c" {
          let sample = $"a{Environment.NewLine}b{Environment.NewLine}c"
          Expect.equal (countYezzezAll sample) 0 "example"
        }
        test "ab ac" {
          let sample = $"ab{Environment.NewLine}ac"
          Expect.equal (countYezzezAll sample) 1 "example"
        }
        test "a a a a" {
          let sample = $"a{Environment.NewLine}a{Environment.NewLine}a{Environment.NewLine}a"
          Expect.equal (countYezzezAll sample) 1 "example"
        }
        test "b" {
          let sample = $"b"
          Expect.equal (countYezzezAll sample) 1 "example"
        }
        test "example" {
          Expect.equal (countYezzezAll testText) 6 "example"
        }
      ]
      |> testLabel "samples"

    let run () = Tests.runTests defaultConfig tests

