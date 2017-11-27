namespace DotNetList.Core

module ListManipulation =
    let AddElement elem list =
        elem :: list

    let rec RemoveElement index list =
        match index, list with
        | 0, h::t       -> t
        | index, h::t   -> h::RemoveElement (index - 1) t
        | index, []     -> failwith "index out of range"

    let FindElement elem list =
        list |> List.tryFind (fun e -> e = elem)
