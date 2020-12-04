namespace Aoc2020

open System
open System.Text.RegularExpressions

#if INTERACTIVE
#r "nuget:FParsec"
#endif
open FParsec

module Day04 =

    type HeightData =
        | Inch of int
        | Cm of int
        | Unknown of string

    let createHeightData (s:string) =
        let m = Regex.Match(s, "(?<h>[\d]+)(?<uom>in|cm)")
        let h, uom = m.Groups.["h"].Value, m.Groups.["uom"].Value
        match uom with
        | "in" -> Inch (int h)
        | "cm" -> Cm (int h)
        | _ -> Unknown s

    type PassportClaim =
        | BirthYear of int        // byr (Birth Year)
        | IssueYear of int        // iyr (Issue Year)
        | ExpirationYear of int     // eyr (Expiration Year)
        | Height of HeightData             // hgt (Height)
        | HairColor of string        // hcl (Hair Color)
        | EyeColor of string         // ecl (Eye Color)
        | PassportID of string       // pid (Passport ID)
        | CountryID of string        // cid (Country ID)
    
    type Passport = 
        | ValidPassport of PassportClaim list
        | InvalidPassport of PassportClaim list

    let passportChecks = 
        [ 
            function | BirthYear y when y >= 1920 && y <= 2002 -> true 
                     | _ -> false 
            function | IssueYear y when y >= 2010 && y <= 2020 -> true
                     | _ -> false 
            function | ExpirationYear y when y >= 2020 && y <= 2030 -> true 
                     | _ -> false 
            function | Height (Cm h) when h >= 150 && h <= 193 -> true
                     | Height (Inch h) when h >= 59 && h <= 76 -> true
                     | _ -> false 
            function | HairColor color when Regex.IsMatch(color, "^#[0-9a-f]{6}$") -> true | _ -> false 
            function | EyeColor color ->
                       ["amb"; "blu"; "brn"; "gry"; "grn"; "hzl"; "oth"]
                       |> List.contains color
                     | _ -> false 
            function | PassportID pid when Regex.IsMatch(pid, "^[\d]{9}$") -> true | _ -> false 
            // optional
            //function | CountryID _ -> true | _ -> false  
        ]

    let createPassport (claims:PassportClaim list) =
        
        let isValid =
            passportChecks 
            |> List.map (fun check -> claims |> List.exists check)
            |> List.forall ((=) true)
        
        if isValid
        then ValidPassport claims
        else InvalidPassport claims

    let claimValue = manyChars (letter <|> digit <|> pchar '#')
    let claimType =
        choice [
            stringReturn "byr" (int >> BirthYear)
            stringReturn "iyr" (int >> IssueYear)
            stringReturn "eyr" (int >> ExpirationYear)
            stringReturn "hgt" (createHeightData >> Height)
            stringReturn "hcl" HairColor
            stringReturn "ecl" EyeColor
            stringReturn "pid" PassportID
            stringReturn "cid" CountryID
        ]    
    
    let claim = 
        claimType .>> skipChar ':' .>>. claimValue
        |>> fun (ctor, value) -> ctor value
    
    let claimList = many (claim .>> spaces)

    let getResult presult = 
        match presult with
        | Success (result, state, pos) -> result
        | Failure (errorStr, error, state) -> raise (exn errorStr)

    let parsePassports (text:string) =
        let doubleNewLine = [| Environment.NewLine + Environment.NewLine |]
        let passportData = text.Split(doubleNewLine, StringSplitOptions.RemoveEmptyEntries)
        let parsed = passportData |> Array.map (run claimList)
        parsed |> Array.map getResult 
               |> Array.map createPassport
    
    let run () =
        let text = System.IO.File.ReadAllText $"{__SOURCE_DIRECTORY__}/input/day04.txt" 
        let passports = parsePassports text
        let validPassportCount = passports |> Array.sumBy (function ValidPassport _ -> 1 | _ -> 0)
        let invalidPassportCount = passports |> Array.sumBy (function InvalidPassport _ -> 1 | _ -> 0)
        passports, validPassportCount, invalidPassportCount 

module Day04Tests =
    
    open Day04 

    //let text = System.IO.File.ReadAllText $"{__SOURCE_DIRECTORY__}/input/day04karl.txt" // correct answers 256 and 198
    let invalidExample =
        """eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007"""

    let validExample =
        """pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719"""

    let test () =
        let invalidOk = 
            let passports = parsePassports invalidExample
            passports |> Array.forall (function InvalidPassport _ -> true | _ -> false)
        let validOk = 
            let passports = parsePassports validExample
            passports |> Array.forall (function ValidPassport _ -> true | _ -> false)
        invalidOk, validOk
