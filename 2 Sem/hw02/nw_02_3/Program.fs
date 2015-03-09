// hw_02, part 3
// by Tarasenko Nik, 171 group
//
//

type BinTree<'T> = Nil | Node of BinTree<'T> * 'T * BinTree<'T>

let rec mapTree f acc tree =   //Map для дерева
    match tree with
    | Nil -> Nil
    | Node(left, value, right) -> Node(mapTree f acc left, f value, mapTree f acc right)

let rec foldTree f acc tree =   //Fold для дерева
    match tree with
    | Nil -> acc
    | Node(left, value, right) -> f (foldTree f acc left) value (foldTree f acc right)

let sumElements tree = foldTree (fun lVal mVal rVal -> lVal + mVal + rVal) 0 tree   //Сумма всех элементов дерева через Fold

let fMin a b foo =
    match a with
    | None -> Some b
    | _ -> a

let rec findMin tree = foldTree fMin None tree  //Нахождение минимального элемента в дереве через Fold

let rec copyTree tree = foldTree (fun lVal mVal rVal -> Node(lVal, mVal, rVal)) Nil tree

let rec add t a=
   match t with
   | Nil -> Node(Nil, a, Nil)
   | Node(left, value, right) -> 
      if a < value then Node(add left a, value, right) else Node(left, value, add right a)

let rec del binTree x =
  let rec takeSmallestLeaf binTree =
    match binTree with
    | Nil -> Nil
    | Node(left, value, right) ->
      if left = Nil then binTree 
      else takeSmallestLeaf left  
  match binTree with
  | Nil -> Nil
  | Node(left, value, right) when x < value -> Node(del left x, value, right)
  | Node(left, value, right) when x > value -> Node(left, value, del right x)
  | Node(left, value, right) ->
      if left = Nil then right
      elif right = Nil then left
      else 
        let smallestLeaf = takeSmallestLeaf right
        match smallestLeaf with
        | Nil -> Nil
        | Node(mLeft, mValue, mRight) -> Node(left, mValue, del right mValue)

let rec CLRprint t =
   match t with
   | Nil -> 0
   | Node(left, value , right) -> 
      printf "%A " value
      CLRprint left |> ignore
      CLRprint right |> ignore
      0

let rec LRCprint t =
   match t with
   | Nil -> 0
   | Node(left, value , right) ->
      LRCprint left |> ignore
      LRCprint right |> ignore
      printf "%A " value
      0

let rec LCRprint t =
   match t with
   | Nil -> 0
   | Node(left, value , right) ->
      LRCprint left |> ignore
      printf "%A " value
      LRCprint right |> ignore
      0

let rec print t =
    match t with 
    | Nil -> ()
    | Node(left, value, right) -> 
        printf "Node %A {" value
        match left with
        | Nil -> printf "Nil"
        | _ -> print left
        printf "; "
        match right with
        | Nil -> printf "Nil"
        | _ -> print right
        printf "}"


[<EntryPoint>]
let main argv = 
   let tree = Node (Node(Node(Nil, 1, Nil), 3, Node(Node(Nil, 4, Nil), 6, Node(Nil, 7, Nil))), 8, Node(Nil, 10, (Node(Node(Nil, 13, Nil), 14, Nil))))

   printfn "Example tree:"
   tree |> print  //Изначальное дерево

   printfn "\n\nUse mapTree for increment by 1: "
   mapTree (fun a -> a + 1) Nil tree |> print

   sumElements tree |> printfn "\n\nSum of elements: %d"

   findMin tree |> printfn "\nMin element: %A/n"

   printfn "Copyed tree:"
   copyTree tree |> print

   0 // возвращение целочисленного кода выхода