// Дополнительные сведения о F# см. на http://fsharp.net
// Дополнительную справку см. в проекте "Учебник по F#".
module Interpreter

open System
open System.IO
open System.Collections.Generic

type Expr = 
    | Num of int
    | Var of string
    | BinOp of char * Expr * Expr

type Stmt = 
       | Read   of string
       | Write  of Expr
       | Assign of string * Expr
       | Seq    of Stmt * Stmt
       | If     of Expr * Stmt * Stmt
       | While  of Expr * Stmt

let operands = 
    ['+', (+); '-', (-); '*', (*);
     '/', (/); '%', (%); '^', (pown);] |> Map.ofList

let ParsInput (inputStr : string) =
    let mutable queue = []
    let mutable foo = ""
   
    for ch in inputStr do
        match ch with
        | '\n' -> 
            queue <- foo :: queue
            foo <- ""
        | '\r' -> ()
        | _ -> foo <- foo + ch.ToString ()

    if foo <> "" then queue <- foo :: queue
 
    List.rev queue

type ListStmt (l : string) =
    let mutable inp = ParsInput l

    member this.Pop () =
        let elem = inp.Head
        inp <- inp.Tail
        elem

    member this.ParsExpr () =
        let elem = this.Pop ()
        match elem with
        | _ when operands.ContainsKey(elem.[0]) && elem.Length = 1 -> 
            BinOp (elem.[0], this.ParsExpr (), this.ParsExpr ())
        | _ when Char.IsDigit(elem.[0]) || elem.[0] = '-' ->
            Num (int elem)
        | _ -> 
            Var (elem)

    member this.ParsStmt () =
        let elem = this.Pop ()
        match elem with
        | "read"    -> Read (this.Pop ())
        | "write"   -> Write (this.ParsExpr ())
        | ":="      -> Assign (this.Pop (), this.ParsExpr ())
        | ";"       -> Seq (this.ParsStmt (), this.ParsStmt ())
        | "if"      -> If (this.ParsExpr (), this.ParsStmt (), this.ParsStmt ())
        | "while"   -> While (this.ParsExpr (), this.ParsStmt ())
        | _         -> failwith "Fail in ParsStmt"

type Interpreter (tree) =
    let dict = Dictionary ()

    member this.runExpr foo =
        match foo with
        | BinOp (op, l, r)  -> operands.[op] (this.runExpr l) (this.runExpr r)
        | Num bar           -> bar
        | Var bar           -> if dict.ContainsKey(bar) then dict.[bar] else 0
        
    member this.runStmt foo =
        match foo with 
        | Read (var)            -> dict.[var] <- int (Console.ReadLine ())
        | Write (expr)          -> Console.WriteLine(int (this.runExpr expr))
        | Assign (var, bar)     -> dict.[var] <- this.runExpr bar
        | Seq (l, r)            -> 
                    this.runStmt l 
                    this.runStmt r
        | If (cond, tr, fal)    -> this.runStmt (if this.runExpr cond <> 0 then tr else fal)
        | While (cond, stmt)    -> while (this.runExpr cond <> 0) do this.runStmt stmt

    member this.LetsGo () =
        this.runStmt tree

[<EntryPoint>]
let main argv = 
    Interpreter(ListStmt(";\nread\nx\n;\nwrite\n+\nx\n1\n;\nread\nx\nwrite\n+\nx\n1").ParsStmt()).LetsGo()
    0 // возвращение целочисленного кода выхода
