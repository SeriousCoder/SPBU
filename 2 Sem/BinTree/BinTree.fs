// Дополнительные сведения о F# см. на http://fsharp.net
// Дополнительную справку см. в проекте "Учебник по F#".

type elem = Nil | Info of elem * int * elem

let rec  findMin t =
   match t with
   |Info(left, value ,right) when left = Nil -> 
      if right = Nil then value else findMin right
   |Info(left, _ , _ ) -> findMin left

let rec add t a=
   match t with
   |Nil -> Info(Nil, a, Nil)
   |Info(left, value, right) -> 
      if a < value then Info(add left a, value, right) else Info(left, value, add right a)

let rec CLRprint t =
   match t with
   |Nil -> 0
   |Info(left, value , right) -> 
      printf "%A " value
      CLRprint left |> ignore
      CLRprint right |> ignore
      0

let rec del t a =
   match t with
   |Nil -> Nil
   |Info(left, value, right) when a < value -> Info(del left a, value, right)
   |Info(left, value, right) when value < a -> Info(left, value, del right a)
   |Info(left, value, right) when value = a -> 
      match left, right with
      |Nil, Nil -> Nil
      |Nil, _ -> right
      | _ , Nil -> left
      | mLeft , Info(xLeft, xValue , xRight ) -> if xLeft = Nil then Info(mLeft, xValue, xRight) 
                                                 else Info(mLeft, findMin right, del right (findMin right))

//let rec del t a =
//   match t with
//   |Nil -> Nil
//   |Info(left, value, right) -> if a < value then Info(del left a, value, right)
//                                elif value < a then Info(left, value, del right a)
//                                else match left, right with
//                                     |Nil, Nil -> Nil
//                                     |Nil, _ -> right
//                                     | _ , Nil -> left
//                                     | mLeft , Info(xLeft, xValue , xRight ) -> if xLeft = Nil then Info(mLeft, xValue, xRight) 
//                                                                                else Info(mLeft, findMin right, del right (findMin right))


let rec LRCprint t =
   match t with
   |Nil -> 0
   |Info(left, value , right) ->
      LRCprint left |> ignore
      LRCprint right |> ignore
      printf "%A " value
      0

let rec LCRprint t =
   match t with
   |Nil -> 0
   |Info(left, value , right) ->
      LRCprint left |> ignore
      printf "%A " value
      LRCprint right |> ignore
      0

[<EntryPoint>]
let main argv = 
   let mutable tree = Info (Info(Info(Nil,1,Nil),3,Info(Info(Nil,4,Nil),6,Info(Nil,7,Nil))),8,Info(Nil,10,(Info(Info(Nil,13,Nil),14,Nil))))
   printf "%A\n" tree
   printf "LCR: "
   LCRprint tree |> ignore
   printf "\nLRC: "
   LRCprint tree |> ignore
   printf "\nCLR: "
   CLRprint tree |> ignore
   printf "\n"

   tree <- del tree 8
   tree <- del tree 7
   printf "After delete 8 and 7: %A\n" tree
   tree <- add tree 9
   tree <- add tree 11
   printf "After add 9 and 11: %A\n" tree
   tree <- del tree 70

   printf "%A\n" tree
   printf "LCR: "
   LCRprint tree |> ignore
   printf "\nLRC: "
   LRCprint tree |> ignore
   printf "\nCLR: "
   CLRprint tree |> ignore
   printf "\n"
   0 // возвращение целочисленного кода выхода
