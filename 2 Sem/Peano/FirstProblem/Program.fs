// Дополнительные сведения о F# см. на http://fsharp.net
// Дополнительную справку см. в проекте "Учебник по F#".

type Peano = Zero | LoL of Peano

let rec plus a b =    //Сложение
   match a with
   |Zero -> b
   |LoL a -> LoL (plus a b)

let rec minus a b =   //Вычитание
   match a, b  with
   |Zero, _ -> Zero
   |LoL a, LoL b -> minus a b
   |LoL a, Zero -> LoL a

let rec mult a b =   //Умножение
   match a, b with
   |Zero, _ -> Zero
   | _ , Zero -> Zero
   | LoL a, _ -> 
      if a = Zero then b else plus b (mult a b)

let rec pow a b =   //Возведение в степень
   match b with
   |Zero -> LoL Zero
   |LoL b -> mult a (pow a b)  

let rec toInt a =   //Перевод в Int
   match a with
   |Zero -> 0
   |LoL a -> 1 + toInt a


[<EntryPoint>]
let main argv = 
    printf "%A\n" (mult (LoL Zero) (LoL (LoL Zero)))
    0 // возвращение целочисленного кода выхода
