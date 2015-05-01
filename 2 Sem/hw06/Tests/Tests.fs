module InterprTests

open NUnit.Framework
open System.IO
open Main

[<TestCase("01.in","11.in",Result="1")>]
[<TestCase("02.in","12.in",Result="0")>]
[<TestCase("03.in","13.in",Result="1\n2")>]
[<TestCase("04.in","14.in",Result="5")>]
[<TestCase("05.in","15.in", Result="5\n4\n3\n2\n1")>]
  let test (code : string) (input : string) =
    let inStream = new StreamReader(code)
    let str = inStream.ReadToEnd ()
    inStream.Dispose ()
    let foo = Main.Interpreter(Main.ListStmt(str).ParsStmt())
    let inStream = new StreamReader(input)
    foo.readStr <- inStream.ReadLine
    inStream.Dispose ()