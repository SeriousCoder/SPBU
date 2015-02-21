// Дополнительные сведения о F# см. на http://fsharp.net
// Дополнительную справку см. в проекте "Учебник по F#".

type Peano = Zero | S of Peano

let rec plus a b =    //Сложение
   match a with
   |Zero -> b
   |S a -> S (plus a b)

let rec minus a b =   //Вычитание
   match a, b  with
   |Zero, _ -> Zero
   |S a, S b -> minus a b
   |S a, Zero -> S a

let rec mult a b =   //Умножение
   match a, b with
   |Zero, _ -> Zero
   | _ , Zero -> Zero
   | S a, _ -> 
      if a = Zero then b else plus b (mult a b)

let rec pow a b =   //Возведение в степень
   match b with
   |Zero -> S Zero
   |S b -> mult a (pow a b)  

let rec toInt a =   //Перевод в Int
   match a with
   |Zero -> 0
   |S a -> 1 + toInt a


[<EntryPoint>]
let main argv = 
    printf "%A\n" (mult (S Zero) (S (S Zero)))
    0 // возвращение целочисленного кода выхода
