open Suave                 // always open suave
open Suave.Successful      // for OK-result
open Suave.Web             // for config
open Suave.Filters
open Suave.Operators
open Suave.RequestErrors

let browse =
    request (fun r ->
        match r.queryParam "genre" with
        | Choice1Of2 genre -> OK (sprintf "Genre: %s" genre)
        | Choice2Of2 msg   -> BAD_REQUEST msg)

let webPart =
    choose [
        path "/" >=> (OK "Home")
        path "/store" >=> (OK "Store")
        path "/store/browse" >=> browse
        pathScan "/store/details/%d"
            (fun id -> OK (sprintf "Details: %d" id))
        pathScan "/store/details/%s/%d"
            (fun (a, id) -> OK (sprintf "Artists: %s; Id: %d" a id))
    ]

startWebServer defaultConfig webPart
