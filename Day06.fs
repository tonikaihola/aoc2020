namespace Aoc2020

open System
open Utils

module Day06 =

    let countGroupYezzez (grp:string) = 
        grp.ToCharArray() 
        |> Array.filter Char.IsLetter
        |> Array.distinct
        |> Array.length

    let countYezzez text =
        let groups = Utils.splitOnEmptyLine text
        let part1 = groups |> Array.map countGroupYezzez |> Array.sum
        part1

    let countGroupYezzezAll (grp:string) = 
        let allAnswers = grp.ToCharArray() |> Array.filter Char.IsLetter |> Array.distinct
        let peopleAnswers = Utils.splitOnLine grp
        allAnswers |> Array.sumBy (fun a -> if peopleAnswers |> Array.forall (fun x -> x.Contains a) then 1 else 0)

    let countYezzezAll text =
        let groups = Utils.splitOnEmptyLine text
        let part2 = groups |> Array.map countGroupYezzezAll |> Array.sum
        part2

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

