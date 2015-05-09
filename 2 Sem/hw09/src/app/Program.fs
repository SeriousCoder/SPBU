module Thread01

open System
open System.Threading

let findMaxInRange (arr : int []) b e : int =
    let mutable max = Int32.MinValue
    for i in b .. e do
        if max < arr.[i] then max <- arr.[i]
    max

let findMax threadNum (arr : int []) : int =
    let max = ref -1
    let step = arr.Length / threadNum
    let threadArr = Array.init threadNum (fun i ->
        new Thread(ThreadStart (fun _ ->
            let threadMax = findMaxInRange arr (i * step) ((i + 1) * step - 1)
            lock max (fun _ -> if max.Value < threadMax then max := threadMax)
        ))
    )
    for th in threadArr do
        th.Start()
    for th in threadArr do
        th.Join()
    max.Value

let integralInRange (f : double -> double) b e step : double =
    let mutable res = 0.0
    for i in b .. step .. (e - step) do
        res <- res + ((f i) + (f (i + step))) * step * 0.5
    res

let calcIntegral (threadNum : int) f (b : float) (e : float) step =
    let res = ref 0.0
    let interval = double ((e - b) / float threadNum)
    let threadArr = Array.init threadNum (fun i ->
        new Thread(ThreadStart (fun _ ->
            let threadRes = integralInRange (f) (b + double i * interval) (b + (double i + 1.0) * interval) step
            lock res (fun _ -> res := res.Value + threadRes)
        ))
    )
    for th in threadArr do
        th.Start()
    for th in threadArr do
        th.Join()
    res.Value

[<EntryPoint>]
let main argv = 
    //printfn "%A" (findMax 3 [|2; 3; 7; 5; 1; 8; 6; 10; 7|])
    //printfn "%A" (calcIntegral 8 (fun x -> Math.Sin x) -10000000.0 0.0 0.5)
    0 // возвращение целочисленного кода выхода