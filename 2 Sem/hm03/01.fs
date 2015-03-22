// Tasks 
// by Tarasenko Nik, 171 group


type IGraph<'A> =   //Task 20
    interface                          
        abstract IsEmpty    : unit -> bool
        abstract GetValue   : int -> 'A
        abstract GetSize    : unit -> int
        abstract FromThis   : int -> int list
        abstract Print      : unit -> unit
    end

type MatrixGraph<'T> (nodes : 'T [], edges : list<int * int>) =  //Task 21
    class
        let values = nodes
        let size = nodes.Length
        let matrix = Array2D.create size size false
        do
            for (a, b) in edges do 
            Array2D.set matrix a b true

        interface IGraph<'T> with
            member this.IsEmpty () = 
                if size = 0 then true 
                else false
            member this.GetSize () = size
            member this.GetValue i = 
                Array.get values i
            member this.FromThis i = 
                let rec out j m =
                    if j < 0 then m
                    elif Array2D.get matrix i j then out (j - 1) (j :: m)
                    else out (j - 1) m
                out (size - 1) []
            member this.Print () =
                printfn "Nodes: %A" values
    end

type ListGraph<'T> (nodes : 'T [], edges : list<int * int>) =   //Task 22
    class
        let values = nodes
        let size = nodes.Length
        let gList = Array.create size List.empty

        do 
            for (a, b) in edges do
                Array.set gList a (b :: (Array.get gList a))

        interface IGraph<'T> with
            member this.IsEmpty () =
                if size = 0 then true else false
            member this.GetSize () = size
            member this.GetValue i =
                Array.get values i
            member this.FromThis i =
                Array.get gList i
            member this.Print () =
                printfn "Nodes: %A" values
    end

let accessOut (graph : IGraph<'T>) node =  //Task 23
    let size = graph.GetSize ()
    let visit = Array.create (size) false
    let rec checking i =
        Array.set visit i true
        for j in (graph.FromThis i) do
            if (Array.get visit j) = false then checking j else ()
    checking node
    Array.set visit node false
    List.map graph.GetValue (List.filter (Array.get visit) [0..(size - 1)]) 
   
//let accessIn (graph : IGraph<'T>) node =  //Task 24
//    let size = graph.GetSize ()
//    let visit = Array.create size false
//    let way = Array.create size false
//    Array.set way node true
//    let access value =
//        let rec checking i =
//            if Array.get visit i = false then
//                Array.set visit i true
//                List.fold (fun acc elem -> acc || (checking elem)) false (graph.FromThis i)
//            else
//                Array.get way i
//        Array.set way value (checking value)
//    access node
//    Array.set way node false
//    List.map graph.GetValue (List.filter (Array.get way) [0..(size - 1)]) 

let accessIn (graph : IGraph<'T>) node =  //Task 24
  let size = graph.GetSize()
  let access i =
    let visit = Array.create size false
    let rec checking i =
      if i = node then
        true
      elif not (Array.get visit i) then
        Array.set visit i true
        List.fold (fun acc elem -> acc || (checking elem)) false (graph.FromThis i)
      else false
    checking i
  List.map graph.GetValue (List.filter access (List.filter (fun x -> x <> node) [0 .. (size - 1)]))

type IPoliGraph<'T, 'A> = //Task 25
    interface
        inherit IGraph<'T>

        abstract GetMark : 'T -> 'T -> 'A
    end

[<EntryPoint>]
let main argv = 
    let g1 =
        new MatrixGraph<int>([|1; 2; 3; 4|], [(0, 1); (0, 3); (1, 0); (3, 0)])
    let g2 =
        new ListGraph<char>([|'A'; 'B'; 'C'; 'D'; 'E'; 'F' |], [(0, 1); (1, 2); (2, 3); (4, 0); (1, 5); (4, 5); (5, 0)])
    printfn "21. Граф с матрицей смежности:"
    (g1 :> IGraph<int>).Print()
    printfn "\n22. Граф со списками смежности:"
    (g2 :> IGraph<char>).Print()
    printfn "\n23. Cписок вершин, доступных из вершины А второго графа:\n%A" (accessIn g2 0)
    //printfn "%A" argv
    0 // return an integer exit code

