// hw_02, part 2
// by Tarasenko Nik, 171 group

//let rec fold f acc list =
//    match list with
//    | [] -> acc
//    | elem :: list -> fold f (f acc elem) list

let horner a list =   //Реализация схемы Горнера
    match list with
    | [] -> 0
    | elem :: list -> List.fold (fun acc elem ->  a * acc + elem) elem list

[<EntryPoint>]
let main argv = 

    horner 3 [2; -3; 7; 12] |> printfn "2x^3 - 3x^2 + 7x + 12 = %A"

    0 // return an integer exit code

