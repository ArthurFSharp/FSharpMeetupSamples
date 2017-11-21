namespace App.Web

open WebSharper
open WebSharper.JavaScript
open WebSharper.JQuery
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Templating

[<JavaScript>]
module Code =

    // The templates are loaded from the DOM, so you just can edit index.html
    // and refresh your browser, no need to recompile unless you add or remove holes.
    type IndexTemplate = Template<"index.html">

    [<NoComparison>]
    type Task = { Name: string; Done: Var<bool> }

    let Tasks =
        ListModel.Create (fun task -> task.Name)
            [ { Name = "Apprendre le F#"; Done = Var.Create true }
              { Name = "Finir le bot en Xamarin Forms"; Done = Var.Create false } ]

    let NewTaskName = Var.Create ""

    [<SPAEntryPoint>]
    let Main =
        IndexTemplate.Main()
            .ListContainer(
                ListModel.View Tasks |> Doc.BindSeqCached (fun task ->
                    IndexTemplate.ListItem()
                        .Task(task.Name)
                        .Clear(fun () -> Tasks.RemoveByKey task.Name)
                        .Done(task.Done)
                        .ShowDone(Attr.DynamicClass "checked" task.Done.View id)
                        .Doc()
                )
            )
            .NewTaskName(NewTaskName)
            .Add(fun () ->
                Tasks.Add { Name = NewTaskName.Value; Done = Var.Create false }
                Var.Set NewTaskName ""
            )
            .ClearCompleted(fun () -> Tasks.RemoveBy (fun task -> task.Done.Value))
            .Doc()
        |> Doc.RunById "tasks"
