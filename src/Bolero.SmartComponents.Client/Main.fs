module Bolero.SmartComponents.Client.Main

open System
open System.Linq.Expressions
open Elmish
open Bolero
open Bolero.Html
open Bolero.Templating.Client
open SmartComponents

type Model =
    {
        x: string
    }

let initModel =
    {
        x = ""
    }

type Message =
    | Ping
    |ChangeData of string

let update message model =
    match message with
    | Ping -> model
    | ChangeData v -> {model with x = v}

    
type Expr = 
  static member Quote(e:Expression<System.Func<string>>) = e 

let view model dispatch =
    let l= Expr.Quote (fun () -> model.x)
    div{
        comp<SmartTextArea>{
            "UserRole"=>"system"
            "Value" => model.x
            attr.callback<string> "ValueChanged" (ChangeData >> dispatch )
            "ValueExpression" => l
        }
    }

type MyApp() =
    inherit ProgramComponent<Model, Message>()

    override this.Program =
        Program.mkSimple (fun _ -> initModel) update view
#if DEBUG
        |> Program.withHotReload
        |> Program.withConsoleTrace
#endif
