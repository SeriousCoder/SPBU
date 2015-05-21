module test

open NUnit.Framework
open CalcLib


[<TestCase ("3+7*(1-5)^2^3/1024", Result = 451)>]
[<TestCase ("sin(pi)", Result = 0)>]
[<TestCase ("cos(pi)", Result = -1)>]
[<TestCase ("tg(pi)", Result = 0)>]
[<TestCase ("4!", Result = 24)>]
[<TestCase ("log(e)", Result = 1)>]
[<TestCase ("sqrt(121)", Result = 11)>]
let Test (str : string) =
    let c = new CalcClass ()
    c.ParsToPolish (str)
    c.Calc ()
    
    