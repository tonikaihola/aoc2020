namespace Aoc2020

open System
open System.IO

module Utils =

    module String =
        let chars (s:string) = s.ToCharArray()

    let readLines path = File.ReadAllLines path

    let splitOnLine (text:string) =
        text.Split([| Environment.NewLine |], StringSplitOptions.RemoveEmptyEntries)

    let splitOnEmptyLine (text:string) =
        let emptyLine = [| Environment.NewLine + Environment.NewLine |]
        text.Split(emptyLine, StringSplitOptions.RemoveEmptyEntries)
