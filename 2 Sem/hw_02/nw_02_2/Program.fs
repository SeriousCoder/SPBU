// hw_02, part 2
// by Tarasenko Nik, 171 group

let rec fold f acc list =
    match list with
    | [] -> acc
    | elem :: list -> fold f (f acc elem) list

let horner a list =   //Реализация схемы Горнера
    match list with
    | [] -> 0
    | elem :: list -> fold (fun acc elem ->  a * acc + elem) elem list

[<EntryPoint>]
let main argv = 

    let mas = [2; -3; 7]   //Возьмем многочлен: 2 * x^2 - 3 * x + 7

    printf "%A " (horner 3 mas)   //Переменная равна 3. Вывод результата.

    0 // return an integer exit code

