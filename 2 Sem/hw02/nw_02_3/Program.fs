// hw_02, part 3
// by Tarasenko Nik, 171 group
//
//

type BinTree<'T> = Nil | Node of BinTree<'T> * 'T * BinTree<'T>

let rec mapTree f acc tree =   //Map для дерева
    match tree with
    |Nil -> Nil
    |Node(left, value, right) -> Node(mapTree f acc left, f value, mapTree f acc right)

let rec foldTree f acc tree =   //Fold для дерева
    match tree with
    |Nil -> acc
    |Node(left, value, right) -> f (foldTree f acc left) value (foldTree f acc right)

let sumElements tree = foldTree (fun lVal mVal rVal -> lVal + mVal + rVal) 0 tree   //Сумма всех элементов дерева через Fold

let fMin a b _ =
    match a with
    |None -> Some b
    |Some a -> Some (min a b)

let findMin tree = foldTree fMin None tree  //Нахождение минимального элемента в дереве через Fold

let copyTree tree = foldTree (fun lVal mVal rVal -> Node(lVal, mVal, rVal)) Nil tree

let rec add t a=
   match t with
   |Nil -> Node(Nil, a, Nil)
   |Node(left, value, right) -> 
      if a < value then Node(add left a, value, right) else Node(left, value, add right a)

let rec CLRprint t =
   match t with
   |Nil -> 0
   |Node(left, value , right) -> 
      printf "%A " value
      CLRprint left |> ignore
      CLRprint right |> ignore
      0

let rec del t a =
   match t with
   |Nil -> Nil
   |Node(left, value, right) when a < value -> Node(del left a, value, right)
   |Node(left, value, right) when value < a -> Node(left, value, del right a)
   |Node(left, value, right) when value = a -> 
      match left, right with
      |Nil, Nil -> Nil
      |Nil, _ -> right
      | _ , Nil -> left
      | mLeft , Node(xLeft, xValue , xRight ) -> if xLeft = Nil then Node(mLeft, xValue, xRight) 
                                                 else Node(mLeft, findMin right, del right (findMin right))

//let rec del t a =
//   match t with
//   |Nil -> Nil
//   |Node(left, value, right) -> if a < value then Node(del left a, value, right)
//                                elif value < a then Node(left, value, del right a)
//                                else match left, right with
//                                     |Nil, Nil -> Nil
//                                     |Nil, _ -> right
//                                     | _ , Nil -> left
//                                     | mLeft , Node(xLeft, xValue , xRight ) -> if xLeft = Nil then Node(mLeft, xValue, xRight) 
//                                                                                else Node(mLeft, findMin right, del right (findMin right))


let rec LRCprint t =
   match t with
   |Nil -> 0
   |Node(left, value , right) ->
      LRCprint left |> ignore
      LRCprint right |> ignore
      printf "%A " value
      0

let rec LCRprint t =
   match t with
   |Nil -> 0
   |Node(left, value , right) ->
      LRCprint left |> ignore
      printf "%A " value
      LRCprint right |> ignore
      0

let rec print t =
    match t with 
    |Nil -> ()
    |Node(left, value, right) -> 
        printf "Node %A {" value
        match left with
        |Nil -> printf "Leaf"
        | _ -> print left
        match right with
        |Nil -> printf "Leaf"
        | _ -> print right
        printf "}"



[<EntryPoint>]
let main argv = 
   let tree = Node (Node(Node(Nil,1,Nil),3,Node(Node(Nil,4,Nil),6,Node(Nil,7,Nil))),8,Node(Nil,10,(Node(Node(Nil,13,Nil),14,Nil))))

   tree |> print  //Изначальное дерево

   printfn "\n Use mapTree for increment by 1: "
   mapTree (fun a -> a + 1) Nil tree |> print

   sumElements tree |> printfn "%d"

   findMin tree |> printfn "%A"

   copyTree tree |> print

   0 // возвращение целочисленного кода выхода