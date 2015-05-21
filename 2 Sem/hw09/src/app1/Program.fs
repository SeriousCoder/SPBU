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

{*
10^4 elems:
Time: 0.4274    No threading:   10000
Time: 4.0954    1 threads:      10000
Time: 2.5308    2 threads:      10000
Time: 4.587     4 threads:      10000
Time: 47.1479   8 threads:      10000
Time: 124.2279  10 threads:     10000

10^7 elems:
Time: 34.0019   No threading:   10000000
Time: 35.9786   1 threads:      10000000
Time: 18.514    2 threads:      10000000
Time: 36.8879   4 threads:      10000000
Time: 59.4414   8 threads:      10000000
Time: 43.1299   10 threads:     10000000

10^7 elems backwards:
Time: 28.6744   No threading:   10000000
Time: 29.518    1 threads:      10000000
Time: 16.4639   2 threads:      10000000
Time: 12.0087   4 threads:      10000000
Time: 43.7354   8 threads:      10000000
Time: 150.6696  10 threads:     10000000
*)
	
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
	
{*
Int y=x (0 .. 10), eps=1e-5             Ans = 50.0
Time: 135.0821  1 threads:      50.0
Time: 111.6567  2 threads:      50.0
Time: 99.7054   4 threads:      49.999975
Time: 60.9116   8 threads:      49.9999375
Time: 92.4361   10 threads:     49.99991

Int y=cos(x)+2 (0 .. 10), eps=1e-5       Ans = 19.456
Time: 231.7564  1 threads:      19.45597889
Time: 146.3427  2 threads:      19.45597889
Time: 106.0582  4 threads:      19.4559669
Time: 87.302    8 threads:      19.45595511
Time: 74.3429   10 threads:     19.45593949

Int y=sin(x)+2 (0 .. 56), eps=1e-5       Ans = 112.1468
Time: 1062.7415 1 threads:      112.1467651
Time: 683.2907  2 threads:      112.1467651
Time: 397.3233  4 threads:      112.1467543
Time: 307.8035  8 threads:      112.1467281
Time: 338.3822  10 threads:     112.1466583
*)

[<EntryPoint>]
let main argv = 
    //printfn "%A" (findMax 3 [|2; 3; 7; 5; 1; 8; 6; 10; 7|])
    //printfn "%A" (calcIntegral 8 (fun x -> Math.Sin x) -10000000.0 0.0 0.5)
    0 // возвращение целочисленного кода выхода