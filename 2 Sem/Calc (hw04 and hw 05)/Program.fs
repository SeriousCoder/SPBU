//by Nikita Tarasenko, 171 group
//Tasks 35-38

open NUnit.Framework
open FsUnit

let ToPolish (tern : string) : list<string> =  //Task 35, 37
    let mutable stackOut : list<string> = []
    let mutable stackOperands : list<char> = []
    let mutable foo : string = ""

    let getPriority ch =
        match ch with
        | '(' -> 0
        | ')' -> 1
        | '+' | '-' -> 2
        | '*' | '/' | '%' -> 3
        | '^' -> 4
        | _ -> -1 //Expression

    for ch in tern do
        match ch with
        |ch when ((ch >= '0') && (ch <= '9')) || (int ch > 96 && int ch < 123) -> foo <- foo + ch.ToString()
        |'(' ->
            if (foo <> "") then
                stackOut <- foo :: stackOut
                foo <- ""
            stackOperands <- ch :: stackOperands
        |')' -> 
            if (foo <> "") then
                stackOut <- foo :: stackOut
                foo <- ""
            while (stackOperands.Length > 0 && stackOperands.Head <> '(') do
                stackOut <- stackOperands.Head.ToString() :: stackOut
                stackOperands <- stackOperands.Tail
            if (stackOperands.Length > 0 && stackOperands.Head = '(') then
                stackOperands <- stackOperands.Tail  // + need Expression
        | '+' | '-' | '*' | '/' | '^' | '%' -> 
            if (ch = '-') && (foo = "") then foo <- "-"
            else
                if (foo <> "") then
                    stackOut <- foo :: stackOut
                    foo <- ""
                if stackOperands.Length = 0 then
                    stackOperands <- ch :: stackOperands
                elif (getPriority ch) > (getPriority stackOperands.Head) || ((getPriority ch) >= (getPriority stackOperands.Head) && (ch = '^'))then
                    stackOperands <- ch :: stackOperands
                else
                    while (stackOperands.Length > 0 && ((getPriority ch) <= (getPriority stackOperands.Head))) do
                        stackOut <- stackOperands.Head.ToString () :: stackOut
                        stackOperands <- stackOperands.Tail
                    stackOperands <- ch :: stackOperands
        | _ -> () //Expression
    if (foo <> "") then
        stackOut <- foo :: stackOut
        foo <- ""
    if (stackOperands.Length > 0) then
        for ch in stackOperands do
            stackOut <- ch.ToString () :: stackOut
    List.rev stackOut


let rec Solution (tern : list<string>) : int =  //Tasks 35, 37
    let mutable stack = []

    let action (a : string, b : string, sign : string) : int =
        match sign with
        | "+" -> int (float (a) + float (b))
        | "-" -> int (float (b) - float (a))
        | "*" -> int (float (a) * float (b))
        | "/" -> int (float (b) / float (a))
        | "%" -> int (float (b) % float (a)) 
        | "^" -> int (float (b) ** float (a))
        | _ -> 0 

    for elem in tern do
        match elem with
        | "+" | "-" | "*" | "/" | "^" | "%"-> 
            let a = stack.Head
            stack <- stack.Tail
            let b = stack.Head
            stack <- (action (a, b, elem)).ToString () :: stack.Tail
        | _ -> stack <- elem :: stack

    int (int stack.Head)

let SolutionWithVar (tern : string, vars : list<string * int>) =
    let count = vars.Length
    let mutable nTern = ToPolish tern

    //printf "Для ("
    for (v, value) in vars do
        //printf " {%s = %d} " v value
        nTern <- List.map (fun x -> if x = v then value.ToString() else x) nTern
    //printf ") = %d" (Solution nTern)
    Solution nTern
            
let readTern str =
    let mutable tern = []
    let mutable foo = ""

    for ch in str do
        match ch with
        | '\n' -> 
            tern <- foo :: tern
            foo <- ""
        | _ -> foo <- foo + ch.ToString ()
    List.rev tern


let printPolish tern =  //Task 37
    let mutable out = ""
    for elem in tern do
       out <- out + elem + "\n"
    out
    //printfn ""

[<TestCase ("1+(2/3)^2", Result = 1)>]
[<TestCase ("(4^(5%3)+(-68))/((-6)*(-2))", Result = -4)>]
[<TestCase ("3+7*(1-5)^2^3/1024", Result = 451)>]
let Test35 (str : string) =
    Solution (ToPolish str)


[<Test>] 
let test36_1 () = 
  SolutionWithVar("1+(2/a)^2", [("a", 3)])  
  |>should equal 1
[<Test>]
let test36_2 () = 
  SolutionWithVar("(4^(5%3)+foo))/(bar*(-2))", [ ("foo", -68); ("bar", -6) ]) 
  |> should equal -4
[<Test>]
let test36_3 () = 
  SolutionWithVar("a+b*(c-d)^e^a/f", [("a", 3); ("b", 7); ("c", 1); ("d", 5); ("e", 2); ("f", 1024)])  
  |>should equal 451

[<TestCase ("1+(2/3)^2", Result = "1\n2\n3\n/\n2\n^\n+\n")>]
[<TestCase ("2^3^2", Result = "2\n3\n2\n^\n^\n")>]
[<TestCase ("1-2-3", Result = "1\n2\n-\n3\n-\n")>]
[<TestCase ("(4^(5%3)+(-68))/((-6)*(-2))", Result = "4\n5\n3\n%\n^\n-68\n+\n-6\n-2\n*\n/\n")>]
[<TestCase ("3+7*(1-5)^2^3/1024", Result = "3\n7\n1\n5\n-\n2\n3\n^\n^\n*\n1024\n/\n+\n")>]
let Test37 (str : string) =
    printPolish (ToPolish str)


[<TestCase ("2\n3\n2\n^\n^\n", Result = 512)>]
[<TestCase ("1\n2\n-\n3\n-\n", Result = -4)>]
[<TestCase ("1\n2\n3\n/\n2\n^\n+\n", Result = 1)>]
[<TestCase ("4\n5\n3\n%\n^\n-68\n+\n-6\n-2\n*\n/\n", Result = -4)>]
[<TestCase ("3\n7\n1\n5\n-\n2\n3\n^\n^\n1024\n/\n*\n+\n", Result = 451)>]
let Test38 (str : string) =
    Solution (readTern str)


[<EntryPoint>]
let main argv = 
    //let tern = ToPolish "1+(2/3)^2"
    //printfn "%A\n" tern
    //printPolish tern
    //printfn "%A" (Solution tern)
    //SolutionWithVar("1+(2/x)^2", [("x", 3)])

    0 // return an integer exit code

