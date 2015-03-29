// Tasks 31 - ...
// by Tarasenko Nik, 171 group

open NUnit.Framework
open FsUnit

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

type IPoliGraph<'T, 'A> =  //Task 25
    interface
        inherit IGraph<'T>

        abstract GetMark : 'T -> 'T -> 'A
    end

[<TestFixture>]
type ``Тест графа с матрицей смежности`` () =  
  let g =
    new MatrixGraph<int>([|1; 2; 3; 4; 5; 6; 7; 8; 9|],
      [(0, 1); (1, 2); (1, 3); (4, 3); (4, 5); (5, 3); (2, 5); (6, 7); (7, 8)])
        :> IGraph<int>
  
  [<Test>] member this.
    ``Из вершины 1 можно попасть в 1, 2, 3, 4, 6`` () =
      (printf "%A" (accessOut g 0))
        |> should equal "[2; 3; 4; 6]"
  
  [<Test>] member this.
    ``Из вершины 4 можно попасть в 4`` () =
      (sprintf "%A" (accessOut g 3))
        |> should equal "[]"
  
  [<Test>] member this.
    ``Из вершины 8 можно попасть в 8, 9`` () =
      (sprintf "%A" (accessOut g 7))
        |> should equal "[9]"  
  
  [<Test>] member this.
    ``В вершину 4 можно попасть из 1, 2, 3, 4, 5, 6`` () =
      (sprintf "%A" (accessIn g 3))
        |> should equal "[1; 2; 3; 5; 6]"  
  
  [<Test>] member this.
    ``В вершину 7 можно попасть из 7`` () =
      (sprintf "%A" (accessIn g 6))
        |> should equal "[]"
  
  [<Test>] member this.
    ``В вершину 8 можно попасть из 7, 8`` () =
      (sprintf "%A" (accessIn g 7))
        |> should equal "[7]"

[<TestFixture>]
type ``Тест графа со списком смежности`` () =  
  let g =
    new ListGraph<int>([|1; 2; 3; 4; 5; 6; 7; 8; 9|],
      [(0, 1); (1, 2); (1, 3); (4, 3); (4, 5); (5, 3); (2, 5); (6, 7); (7, 8)])
        :> IGraph<int>
  
  [<Test>] member this.
    ``Из вершины 1 можно попасть в 1, 2, 3, 4, 6`` () =
      (sprintf "%A" (accessOut g 0))
        |> should equal "[2; 3; 4; 6]"
  
  [<Test>] member this.
    ``Из вершины 4 можно попасть в 4`` () =
      (sprintf "%A" (accessOut g 3))
        |> should equal "[]"
  
  [<Test>] member this.
    ``Из вершины 8 можно попасть в 8, 9`` () =
      (sprintf "%A" (accessOut g 7))
        |> should equal "[9]"  
  
  [<Test>] member this.
    ``В вершину 4 можно попасть из 1, 2, 3, 4, 5, 6`` () =
      (sprintf "%A" (accessIn g 3))
        |> should equal "[1; 2; 3; 5; 6]"  
  
  [<Test>] member this.
    ``В вершину 7 можно попасть из 7`` () =
      (sprintf "%A" (accessIn g 6))
        |> should equal "[]"
  
  [<Test>] member this.
    ``В вершину 8 можно попасть из 7, 8`` () =
      (sprintf "%A" (accessIn g 7))
        |> should equal "[7]"

[<EntryPoint>]
let main argv = 
    let g1 =
        new MatrixGraph<int>([|1; 2; 3; 4; 5; 6; 7; 8; 9|],
                             [(0, 1); (1, 2); (1, 3); (4, 3); (4, 5); (5, 3); (2, 5); (6, 7); (7, 8)]) :> IGraph<int>
    let g2 =
        new ListGraph<char>([|'A'; 'B'; 'C'; 'D'; 'E'; 'F' |], [(0, 1); (1, 2); (2, 3); (4, 0); (1, 5); (4, 5); (5, 0)])
    printfn "\nГраф с матрицей смежности:"
    g1.Print()
    printfn "\nГраф со списками смежности:"
    (g2 :> IGraph<char>).Print()
    printfn "\nCписок вершин, доступных из вершины 1 второго графа:\n%A" (accessOut g1 0)
    printfn "\nCписок вершин, из которых доступна из вершина А второго графа:\n%A" (accessIn g1 0)
    0 // return an integer exit code

