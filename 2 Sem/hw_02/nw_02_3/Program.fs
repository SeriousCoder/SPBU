// hw_02, part 3
// by Tarasenko Nik, 171 group
//
//

type BinTree<'T> = Nil | Info of BinTree<'T> * 'T * BinTree<'T>

type Option<'T> = None | Some of 'T

let rec mapList f acc tree =   //Map для дерева
    match tree with
    |Nil -> Nil
    |Info(left, value, right) -> Info(mapList f acc left, f value, mapList f acc right)

let rec foldTree f acc tree =   //Fold для дерева
    match tree with
    |Nil -> acc
    |Info(left, value, right) -> f (foldTree f acc left) value (foldTree f acc right)

let sumElements tree = foldTree (fun lVal mVal rVal -> lVal + mVal + rVal) 0 tree   //Сумма всех элементов дерева через Fold

let findMin (tree : BinTree<'T>) : Option<'T> =    //Нахождение минимального элемента в дереве через Fold
    match tree with
    |Nil -> None
    |Info( _ , value, _ ) -> Some (foldTree (fun lVal mVal rVal -> if lVal < mVal then lVal else mVal) value tree)

let copyTree tree = foldTree (fun lVal mVal rVal -> Info(lVal, mVal, rVal)) Nil tree


[<EntryPoint>]
let main argv = 
   let tree = Info (Info(Info(Nil,1,Nil),3,Info(Info(Nil,4,Nil),6,Info(Nil,7,Nil))),8,Info(Nil,10,(Info(Info(Nil,13,Nil),14,Nil))))

   printf "%A\n" tree

   sumElements tree |> printf "%A\n"

   match findMin tree with
   |None -> printf "None\n"
   |Some a -> printf "%d\n" a

   copyTree tree |> printf "%A\n"

   0 // возвращение целочисленного кода выхода