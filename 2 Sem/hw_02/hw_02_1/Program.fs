// hw_02, part 1
// by Tarasenko Nik, 171 group

//List.iter : ('T -> unit) -> 'T list -> unit 


let rec fold f acc list =
    match list with
    | [] -> acc
    | elem :: list -> fold f (f acc elem) list

let reverseList list = fold (fun acc elem -> elem :: acc) [] list   //List.rev через fold

let filterList f list = fold (fun acc elem -> if f(elem) = true then acc @ [elem] else acc) [] list   //List.filter через fold

let mapList f list = fold (fun acc elem -> acc @ [f elem]) [] list   //List.map через fold

[<EntryPoint>]
let main args =
    let mas = [1; 2; 3; 4; 5; 6]

    reverseList mas                                               |> List.iter(fun x -> printf "%A " x)

    printfn ""

    filterList (fun elem -> if elem < 4 then true else false) mas |> List.iter(fun x -> printf "%A " x)

    printfn ""

    mapList (fun x -> x + 1) mas                                  |> List.iter(fun x -> printf "%A " x)
    0
