// En savoir plus sur F# sur le site http://fsharp.org
// Voir le projet 'Didacticiel F#' pour obtenir de l'aide.

open System
open System.Windows
open System.Windows.Controls
open System.Windows.Markup
open App.Wpf
open System.Collections.ObjectModel
open System.Linq
open System.Collections.Generic

type Task() =
    inherit ViewModelBase()

    let mutable name = ""
    let mutable isDone = false

    member this.Name
        with get() = name
        and set(value) =
            name <- value
            base.NotifyPropertyChanged(<@ this.Name @>)

    member this.Done
        with get() = isDone
        and set(value) =
            isDone <- value
            base.NotifyPropertyChanged(<@ this.Done @>)

type TodoViewModel() =
    inherit ViewModelBase()
    
    let mutable newTask = ""

    let mutable tasks = ObservableCollection<Task>()

    do
        tasks.Add(new Task (Name = "Apprendre le F#", Done = true))
        tasks.Add(new Task (Name = "Finir le bot en Xamarin Forms", Done = false))

    member this.Tasks
        with get() = tasks
        and set(value) =
            tasks <- value
            base.NotifyPropertyChanged(<@ this.Tasks @>)

    member this.NewTask
        with get() = newTask
        and set(value) =
            newTask <- value
            base.NotifyPropertyChanged(<@ this.NewTask @>)
    
    member this.AddTaskCommand = Command(
                                        fun _ -> 
                                            let duplicate = tasks.ToList().Any(fun l -> l.Name = this.NewTask)
                                            match duplicate with
                                            | true -> ()
                                            | false -> let elem = new Task(Name = this.NewTask, Done = false )
                                                       tasks.Add(elem)
                                            this.NewTask <- "")

    member this.ClearCompletedCommand = Command(
                                            fun _ ->
                                                let taskList = tasks.ToList()
                                                let removeElem = taskList.RemoveAll((fun task -> task.Done = true))
                                                this.Tasks <- new ObservableCollection<Task>(taskList))
    
    member this.RemoveElementCommand = Command(
                                            fun args ->
                                                let element = args :?> Task
                                                let isRemove = this.Tasks.Remove(element)
                                                ())
    

[<STAThread>]
[<EntryPoint>]
let main argv = 
    let mainWindow =
        Application.LoadComponent(new System.Uri("/App.Wpf;component/MainWindow.xaml", UriKind.Relative)) :?> Window

    mainWindow.DataContext <- new TodoViewModel()
    
    let application = new Application()
    application.Run(mainWindow)
