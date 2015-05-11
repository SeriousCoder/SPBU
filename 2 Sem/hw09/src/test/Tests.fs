module Test

open System
open NUnit.Framework
open Thread01
open Thread02

[<Test>]
let ``Max of a few elems`` () =
  let arr = [| -5; 6; 0; 8; -10; 7; 5 |]
  for n in [1; 2; 4] do
    Assert.AreEqual(findMax n arr, 8)

[<Test>]
let ``Max of 10^4 elems`` () =
  let arr = seq { 1 .. 10000 } |> Seq.toArray
  for n in [1; 2; 4; 8] do
    Assert.AreEqual(findMax n arr, 10000)

[<Test>]
let ``Max of 10^7 elems`` () =
  let arr = seq { 1 .. 10000000 } |> Seq.toArray
  for n in [1; 2; 4; 8] do
    Assert.AreEqual(findMax n arr, 10000000)

[<Test>]
let ``Int y=x (0 .. 10)`` () =
  let f x = x
  for n in [1; 2; 4; 8] do
    Assert.Less(Math.Abs((calcIntegral n f 0.0 10.0 1e-5) - 50.0), 1e-4)

[<Test>]
let ``Int y=cos(x)+2 (0 .. 10)`` () =
  let f x = Math.Cos(x) + 2.0
  for n in [1; 2; 4; 8] do
    Assert.Less(Math.Abs((calcIntegral n f 0.0 10.0 1e-5) - 19.456), 1e-4)

[<Test>]
let ``Int y=sin(x)+2 (0 .. 56)`` () =
  let f x = Math.Sin(x) + 2.0
  for n in [1; 2; 4; 8] do
    Assert.Less(Math.Abs((calcIntegral n f 0.0 56.0 1e-5) - 112.1468), 1e-4)


let check (arr: 'a []) = 
  if arr = [||] then true
  else 
    let length = arr.Length - 1
    let mutable flag = true
    let mutable i = 0
    while i < (length - 1)  && flag do
      if arr.[i] <= arr.[i+1] then 
        i<- i+1
        flag <- true
      else flag <- false
    flag
    
[<TestCase (1, [|9;8;7;6;5;4;3;2;1;0|], Result = true)>]
[<TestCase (4, [|0;1;2;3;4;5|], Result = true)>]
[<TestCase (2, [|0;1;0;1;0|], Result = true)>]
[<TestCase (3, ([||] :int []), Result = true)>]
let test2 n arr = 
  mergeSort arr n |> check

[<Test>]
let Test_1Thread () =
  let n = 1000000 
  let rnd = new System.Random(0)
  let g = Array.init n (fun i -> rnd.Next(0, n))
  Assert.AreEqual(true, mergeSort g 1 |> check)


[<Test>]
let Test_3Thread () =
  let n = 1000000 
  let rnd = new System.Random(0)
  let g = Array.init n (fun i -> rnd.Next(0, n))
  Assert.AreEqual(true, mergeSort g 3 |> check)