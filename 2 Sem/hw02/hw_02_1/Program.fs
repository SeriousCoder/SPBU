// hw_02, part 1
// by Tarasenko Nik, 171 group

//List.iter : ('T -> unit) -> 'T list -> unit 



let reverseList list = List.fold (fun acc elem -> elem :: acc) [] list   //List.rev через fold

let filterList f list = List.foldBack (fun elem acc -> if f(elem) = true then elem :: acc else acc) list []   //List.filter через fold

let mapList f list = List.foldBack (fun elem acc -> (f elem) :: acc) list []   //List.map через fold

let printList mas = 
    mas |> List.iter(fun x -> printf "%A " x)
    printfn ""

[<EntryPoint>]
let main args =
    let mas = [1; 2; 3; 4; 5; 6]

    printList (reverseList mas)

    printList (filterList (fun elem -> elem < 4) mas)

    printList (mapList (fun x -> x + 1) mas)
    0
