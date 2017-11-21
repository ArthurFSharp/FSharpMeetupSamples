namespace App.Wpf

open System.ComponentModel
open Microsoft.FSharp.Quotations.Patterns

type ViewModelBase () =
    let propertyChanged =
        Event<PropertyChangedEventHandler, PropertyChangedEventArgs>()

    let getPropertyName quotation =
        match quotation with
        | PropertyGet(_, p, _) -> p.Name
        | _                    -> invalidOp "property get accessor needed"

    interface INotifyPropertyChanged with
        [<CLIEvent>]
        member this.PropertyChanged = propertyChanged.Publish

    member private this.NotifyPropertyChanged propertyName =
        propertyChanged.Trigger(this, PropertyChangedEventArgs(propertyName))

    member this.NotifyPropertyChanged quotation =
        quotation |> getPropertyName |> this.NotifyPropertyChanged

open Microsoft.FSharp.Control
open System.Windows.Input
open System

type Command(execute, canExecute) =
    let canExecuteChanged = Event<_, _>()
    interface ICommand with
        [<CLIEvent>]
        member this.CanExecuteChanged = canExecuteChanged.Publish
        member this.CanExecute param = canExecute param
        member this.Execute param = execute param
    
    new (execute) =
        Command(execute, (fun p -> true))

    member this.RaiseCanExecuteChanged p = canExecuteChanged.Trigger(this, EventArgs.Empty)