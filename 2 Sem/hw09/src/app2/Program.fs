module Thread02

open System
open System.Threading

let merge (arr1 : int array) (arr2 : int array) (res : int [] ref) =
    let mutable i = 0
    let mutable j = 0
    while (i + j) < res.Value.Length do
        if (i >= arr1.Length) then 
            res.Value.[i + j] <- arr2.[j]
            j <- j + 1
        elif (j >= arr2.Length) then
            res.Value.[i + j] <- arr1.[i]
            i <- i + 1
        else
            if (arr1.[i] < arr2.[j]) then 
                res.Value.[i + j] <- arr1.[i]
                i <- i + 1
            else
                res.Value.[i + j] <- arr2.[j]
                j <- j + 1

let rec mergeSort (arr : int []) threads =
    let res = ref (Array.zeroCreate(arr.Length))
    if arr.Length > 1 then
        let mid = arr.Length / 2
        if (threads > 1) then
            let arr1 = ref [||]
            let arr2 = ref [||]

            let ThreadMerge = new Thread(ThreadStart(fun _ ->
                arr1 := mergeSort arr.[0..mid - 1] (threads / 2)
               ))
            ThreadMerge.Start()
            arr2 := mergeSort arr.[mid..(arr.Length - 1)] (threads - threads / 2)
            ThreadMerge.Join()
            lock res (fun _ -> merge arr1.Value arr2.Value res)
        else
            merge (mergeSort arr.[0..mid - 1] 0) (mergeSort arr.[mid..(arr.Length - 1)] 0) res
        res.Value
    else
        arr


let measureTime f =
  let timer = new System.Diagnostics.Stopwatch()
  timer.Start()
  let returnValue = f()
  timer.Stop()
  printf "Time: %A" timer.Elapsed.TotalMilliseconds
  returnValue

[<EntryPoint>]
let main argv = 
    printfn "%A" (measureTime (fun _ -> mergeSort [|9;8;7;6;5;4;3;2;1;0|] 1))
    0 // возвращение целочисленного кода выхода
