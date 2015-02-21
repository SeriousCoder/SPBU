// Дополнительные сведения о F# см. на http://fsharp.net
// Дополнительную справку см. в проекте "Учебник по F#".

type elem = Nil | Info of elem * int * elem

let rec  findMin t =
   match t with
   |Info(left, _ , _ ) when left = Nil -> t
   |Info(left, _ , _ ) -> findMin left

let rec add t a=
   match t with
   |Nil -> Info(Nil, a, Nil)
   |Info(left, value, right) -> 
      if a < value then Info(add left a, value, right) else Info(left, value, add right a)

let rec del t a =
   match t with
   |Nil -> Nil
   |Info(left, value, right) when a < value -> Info(del left a, value, right)
   |Info(left, value, right) when value > a -> Info(left, value, del right a)
   |Info(left, value, right) when value = a -> 
      match left, right with
      |Nil, Nil -> Nil
      |Nil, _ -> right
      | _ , Nil -> left
      | _ , _ -> 
         match findMin right with
         |Nil -> Nil
         |Info( _ , mValue, _ ) -> 
            let right = del right mValue
            Info(left, mValue, right)

let rec clr t =
   match t with
   |Nil -> 0
   |Info(left, _ , right) -> 
      printf "%A\n" t
      clr left |> ignore
      clr right |> ignore
      0

let rec lrc t =
   match t with
   |Nil -> 0
   |Info(left, _ , right) ->
      lrc left |> ignore
      lrc right |> ignore
      printf "%A\n" t
      0

let rec lcr t =
   match t with
   |Nil -> 0
   |Info(left, _ , right) ->
      lcr left |> ignore
      printf "%A\n" t
      lcr right |> ignore
      0

[<EntryPoint>]
let main argv = 
   let mutable tree = Nil
   tree <- add tree 3
   tree <- add tree 1
   tree <- add tree 5
   tree <- add tree 4
   tree <- del tree 3
   lrc tree
   0 // возвращение целочисленного кода выхода
