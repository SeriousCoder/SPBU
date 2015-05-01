namespace CalcLib

open System

type CalcClass () = 
    
    let mutable _tern = []

    member this.ParsToPolish tern =
        let mutable stackOut : list<string> = []
        let mutable stackOperands : list<string> = []
        let mutable foo : string = ""

        let getPriority ch =
            match ch with
            | "(" -> 0
            | ")" -> 1
            | "+" | "-" -> 2
            | "*" | "/" | "%" -> 3
            | "^" | "q" | "s" | "c" | "t" | "!" | "l" -> 4
            | _ -> -1 //Expression

        for ch in tern do
            match ch with
            |ch when ((ch >= '0') && (ch <= '9')) || (int ch > 96 && int ch < 123) || (ch = ',')-> 
                match ch with
                |ch when (ch >= '0') && (ch <= '9') || (ch = ',') ->
                    if (foo <> "") then
                        if (int foo.[0] > 96) then
                            if (this.Check foo) then
                                stackOut <- foo :: stackOut
                            else
                                stackOperands <- foo :: stackOperands
                            foo <- ""
                        else
                            foo <- foo + ch.ToString()
                    else
                        foo <- foo + ch.ToString()
                |ch when (ch >= 'a') && (ch <= 'z') ->
                    if (foo <> "") then
                        if (int foo.[0] < 97) then
                            stackOut <- foo :: stackOut
                            foo <- ""
                        else
                            foo <- foo + ch.ToString()
                    else
                        foo <- foo + ch.ToString()
            |'(' ->
                if (foo <> "") then
                    if (this.Check foo) then
                                stackOut <- foo :: stackOut
                            else
                                stackOperands <- foo :: stackOperands
                    foo <- ""
                stackOperands <- ch.ToString() :: stackOperands
            |')' -> 
                if (foo <> "") then
                    if (this.Check foo) then
                                stackOut <- foo :: stackOut
                            else
                                stackOperands <- foo :: stackOperands
                    foo <- ""
                while (stackOperands.Length > 0 && stackOperands.Head <> "(") do
                    stackOut <- stackOperands.Head.ToString() :: stackOut
                    stackOperands <- stackOperands.Tail
                if (stackOperands.Length > 0 && stackOperands.Head = "(") then
                    stackOperands <- stackOperands.Tail  // + need Expression
            | '+' | '-' | '*' | '/' | '^' | '%' | '!' -> 
                if (ch = '-') && (foo = "") then foo <- "-"
                else
                    if (foo <> "") then
                        if (this.Check foo) then
                                stackOut <- foo :: stackOut
                            else
                                stackOperands <- foo :: stackOperands
                        foo <- ""
                    if stackOperands.Length = 0 then
                        stackOperands <- ch.ToString() :: stackOperands
                    elif (getPriority (ch.ToString())) > (getPriority stackOperands.Head) || ((getPriority (ch.ToString())) >= (getPriority stackOperands.Head) && (ch = '^'))then
                        stackOperands <- ch.ToString() :: stackOperands
                    else
                        while (stackOperands.Length > 0 && ((getPriority (ch.ToString())) <= (getPriority stackOperands.Head))) do
                            stackOut <- stackOperands.Head.ToString () :: stackOut
                            stackOperands <- stackOperands.Tail
                        stackOperands <- ch.ToString() :: stackOperands
            | _ -> () //Expression
        if (foo <> "") then
            if (this.Check foo) then
                stackOut <- foo :: stackOut
            else
                stackOperands <- foo :: stackOperands
            foo <- ""
        if (stackOperands.Length > 0) then
            for ch in stackOperands do
                stackOut <- ch.ToString () :: stackOut
        _tern <- (List.rev stackOut)

    member this.Check str =
        match str with
        | "sin" | "cos" | "tg" | "log" | "sqrt" -> false
        | _ -> true

    member this.Fact a =
        let rec fact foo =
            match foo with
            | 0 -> 1
            | _ -> foo * fact (foo - 1)

        fact a
        
 
    member this.Calc () =
        let rec Solution (tern : list<string>) = 
            let mutable stack = []

            let action (a : string, b : string, sign : string)  =
                match sign with
                | "+" -> Double.Parse(a) + Double.Parse (b)
                | "-" -> Double.Parse (b) - Double.Parse (a)
                | "*" -> Double.Parse (a) * Double.Parse (b)
                | "/" -> Double.Parse (b) / Double.Parse (a)
                | "%" -> Double.Parse (b) % Double.Parse (a)
                | "^" -> Double.Parse (b) ** Double.Parse (a)
                | _ -> 0.0 

            for elem in _tern do
                match elem with
                | "+" | "-" | "*" | "/" | "^" | "%"-> 
                    let a = stack.Head
                    stack <- stack.Tail
                    let b = stack.Head
                    stack <- (action (a, b, elem)).ToString () :: stack.Tail
                | "sin" -> 
                    let a = stack.Head
                    stack <- (Math.Sin (Double.Parse a)).ToString() :: stack.Tail
                | "cos" -> 
                    let a = stack.Head
                    stack <- (Math.Cos (Double.Parse a)).ToString() :: stack.Tail
                | "tg" -> 
                    let a = stack.Head
                    stack <- (Math.Tan (Double.Parse a)).ToString() :: stack.Tail
                | "log" -> 
                    let a = stack.Head
                    stack <- (Math.Log (Double.Parse a)).ToString() :: stack.Tail
                | "sqrt" ->
                    let a = stack.Head
                    stack <- (Math.Sqrt (Double.Parse a)).ToString() :: stack.Tail
                | "!" -> 
                    let a = Double.Parse stack.Head
                    let _a = int (Math.Truncate(a))
                    let _b = a - double (_a)
                    stack <- (Math.E ** (Math.Log(double (this.Fact(_a))) + _b * Math.Log(double (_a + 1)))).ToString() :: stack.Tail
                | "e" -> stack <- Math.E.ToString() :: stack
                | "pi" -> stack <- Math.PI.ToString() :: stack
                | _ -> stack <- elem :: stack

            _tern <- []
            Double.Parse (stack.Head)  


        Solution _tern;


    member this.GetResult var =
        let foo = _tern
        _tern <- List.map (fun x -> if x = "x" then var.ToString() else x) _tern
        let res = this.Calc ();
        _tern <- foo
        res
