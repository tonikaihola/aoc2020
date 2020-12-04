namespace Aoc2020

open Argu

module CommandLine =

    [<CliPrefix(CliPrefix.DoubleDash)>]
    type AocArgs =
        | [<AltCommandLine("-d")>] Day of int

        interface IArgParserTemplate with
            member this.Usage =
                match this with
                | Day _ -> "specify day number"

    let parse argv = 
        let parser = ArgumentParser.Create<AocArgs>(programName = "aoc2020.exe")
        parser.ParseCommandLine(inputs = argv, raiseOnUsage = true)
