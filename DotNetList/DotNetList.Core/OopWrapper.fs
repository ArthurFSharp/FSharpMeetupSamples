namespace DotNetList.Core

open ListManipulation
open System.Collections.Generic

type IMyList =
    abstract member AddElementToList: string -> System.Collections.Generic.List<string> -> System.Collections.Generic.List<string>
    abstract member RemoveElementToList: int -> System.Collections.Generic.List<string> -> System.Collections.Generic.List<string>
    abstract member FindElementInList: string -> System.Collections.Generic.List<string> -> string option

type MyList() =
    member this.AddElementToList elem list =
        let l = List.ofSeq list
        ResizeArray<string> (AddElement elem l)
    member this.RemoveElementToList index list =
            let l = List.ofSeq list
            ResizeArray<string> (RemoveElement index l)
    member this.FindElementInList elem list =
            let l = List.ofSeq list
            FindElement elem l

    interface IMyList with
        member this.AddElementToList elem list =
            this.AddElementToList elem list
        member this.RemoveElementToList index list =
            this.RemoveElementToList index list
        member this.FindElementInList elem list =
            this.FindElementInList elem list
