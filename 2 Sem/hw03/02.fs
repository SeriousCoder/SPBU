// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
//open System

let chanсeInfect = System.Random()

let Protection osName =
    match osName with
    | "Windows" -> 0.6
    | "Linux  "   -> 0.2
    | "OS X   "    -> 0.15
    | "Android" -> 0.3
    | _         -> failwith "Oops, i don't know this OS."

type Computer(os : string, infect : bool) =
    class
        let mutable OS = os
        let mutable Infected = infect

        member this.IsInfected () =
            Infected
         
        member this.IsOS () =
            OS

        member this.TryInfect () =
            if (Protection OS < chanсeInfect.NextDouble()) then
                Infected <- true
    end

let InitComps (comps : list<string>, infect : list<bool>) =
    let rec Comp (comps, infect) : list<Computer> =
        match comps, infect with
        | a :: comps, b :: infect -> new Computer(a, b) :: (Comp (comps, infect))
        | _ , _ -> []
    Comp (comps, infect)


type Network (comps : list<Computer>, edges : list<int * int>) =
    class
        let mutable time = 0
        let commun = Array.create comps.Length List.empty

        do
            for (a, b) in edges do
                Array.set commun a (b :: (Array.get commun a))
                Array.set commun b (a :: (Array.get commun b))
        
        member this.Print () =
            printfn "Step %d" time
            let mutable i = 0
            for comp in comps do
                printfn "%d | %A %A" i (comp.IsOS ()) (comp.IsInfected ())
                i <- i + 1

        member this.NextStep () =
            time <- time + 1
            let infected = List.filter (fun a -> comps.[a].IsInfected ()) [1 .. (comps.Length - 1)]
            for comp in infected do
                for victim in commun.[comp] do
                    if (comps.[victim].IsInfected () = false) then comps.[victim].TryInfect ()

        member this.CheckStatus () : bool =
            if ((List.fold (fun acc a -> acc + (if comps.[a].IsInfected () = true then 1 else 0)) 0 [0 .. (comps.Length - 1)]) <> comps.Length) then 
                true
            else
                false

    end

let Net = new Network(InitComps(["Windows"; "Linux  "; "OS X   "; "Windows"; "Linux  "; "Windows"; "Linux  "; "OS X   "; "Windows"], 
                                [false; false; true; false; false; true; true; false; false]), 
                                [(0, 1); (1, 2); (0, 2); (8, 5); (4, 3); (4, 7); (3, 7); (6, 7)]);

(*
    L ---- !iOS   !M        L
    |      /      |        |  \
    |     /       |        |    \
    |    /        |        |      \
    |   /         |        iOS ---- M
    |  /          |        |
    | /           |        |
    M             M       !L
*)

let Life =
    printfn "Первоначальное состояние сети:     "    //В этом месте у меня почему-то не получилось все сделать через один printf
    printfn "(1) ----(2)   (5)       (4)         " 
    printfn " |      /      |        |  \        " 
    printfn " |     /       |        |    \      " 
    printfn " |    /        |        |      \    " 
    printfn " |   /         |       (7) ----(3)  " 
    printfn " |  /          |        |           " 
    printfn " | /           |        |           " 
    printfn "(0)           (8)      (6)          \n"   
            
    Net.Print ()
    printfn ""
    while (Net.CheckStatus ()) do
        Net.NextStep ()
        Net.Print ()
        printfn ""
        async {
            do! Async.Sleep(3000)
        } |>ignore

[<EntryPoint>]
let main argv = 
    Life 
    //printfn "%A" argv
    0 // return an integer exit code

