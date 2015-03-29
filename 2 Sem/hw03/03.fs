// Task 27 - 29
// by Tarasenko Nik, 171 group

type IList<'T> =   //Task 27
  interface
    abstract HeadValue   : unit -> Option<'T> 
    abstract Length      : unit -> int
    abstract Tail        : unit -> IList<'T>
    abstract AddHead     : 'T   -> unit
    abstract AddTail     : 'T   -> unit
    abstract AddAt       : 'T   -> int  -> bool
    abstract RemoveHead  : unit -> bool
    abstract RemoveTail  : unit -> bool
    abstract RemoveAt    : int  -> bool
    abstract Find        : ('T  -> bool) -> Option<'T>
    abstract Append      : IList<'T>    -> unit
  end

type Node<'T> = 
    | Nil
    | El of 'T * Node<'T> 

type ListADT<'T> (input : Node<'T>, size : int) =
    class
        let mutable l = input
        let mutable length = size

        let rec tail (t : Node<'T>) =
            match t with
            | Nil -> Nil
            | El( _ , Nil) -> t
            | El( _ , t) -> tail t

        interface IList<'T> with

            member this.HeadValue () =
                match l with
                | Nil -> None
                | El(a, _ ) -> Some a


            member this.Length () = length

            //member this.Tail () = tail l
    end


[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code

