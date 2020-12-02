namespace Aoc2020

open System
open System.Text
open System.Text.RegularExpressions
open Utils

module Day02 =

    type Password = Password of string
    type PolicyParams = { Min:int; Max:int; Character: char }
    type PasswordPolicy =
        | MinMax of PolicyParams
        | Positions of PolicyParams
    
    let isValidMinMax (mmc:PolicyParams) (Password pwd) =
        let chars = pwd.Trim().ToCharArray()
        let occurs = chars |> Array.sumBy (fun c -> if c = mmc.Character then 1 else 0)
        let minValid = occurs >= mmc.Min
        let maxValid = occurs <= mmc.Max
        minValid && maxValid

    let isValidPositions (mmc:PolicyParams) (Password pwd) =
        let isHit position = pwd.[position - 1] = mmc.Character
        match isHit mmc.Min, isHit mmc.Max with
        | true, false -> true
        | false, true -> true
        | _,_ -> false

    let parsePolicyParams (s:string) : PolicyParams * Password =
        let parts = s.Split ([|"-";" ";":"|], System.StringSplitOptions.RemoveEmptyEntries)
        let params' = 
            { Min = int parts.[0]
              Max = int parts.[1]
              Character = parts.[2].[0] }
        params', Password parts.[3]

    let validatePassword policy password = 
        match policy with
        | MinMax p -> isValidMinMax p password
        | Positions p -> isValidPositions p password
    
    let test () =
        let policy, pwd = parsePolicyParams "13-16 j: jjjjjjjjjjjjjjjj" 
        validatePassword (MinMax policy) pwd

    let run () =
        let lines = System.IO.File.ReadAllLines $"{__SOURCE_DIRECTORY__}/input/day02.txt"
        let inputs = lines |> Array.map parsePolicyParams
        let countValid policy (params',pwd) = if validatePassword (policy params') pwd then 1 else 0
        let minmaxResult = inputs |> Array.sumBy (countValid MinMax)
        let positionsResult = inputs |> Array.sumBy (countValid Positions)
        minmaxResult,positionsResult