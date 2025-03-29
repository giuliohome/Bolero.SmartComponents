module Bolero.SmartComponents.Client.Main

open System
open System.Linq.Expressions
open Elmish
open Bolero
open Bolero.Html
open Bolero.Templating.Client
open SmartComponents

type Address ={
        name:string
        addr1:string
        city:string
        zip:string 
}

type Model =
    {
        x: string
        address:Address
    }

let initModel =
    {
        x = ""
        address = {
            addr1=""
            city=""
            name=""
            zip=""
        }
    }

type Message =
    | Ping
    |ChangeData of string
    |ChangeAddress of Address

let update message model =
    match message with
    | Ping -> model
    | ChangeData v -> {model with x = v}
    | ChangeAddress a -> {model with address = a}

    
type Expr = 
  static member Quote(e:Expression<System.Func<string>>) = e 

let view model dispatch =
    let l= Expr.Quote (fun () -> model.x)
    concat{
        div{
            comp<SmartTextArea>{
                "UserRole"=>"HR administrator replying to an employee enquiry"
                "Value" => model.x
                attr.callback<string> "ValueChanged" (ChangeData >> dispatch )
                "ValueExpression" => l
            }
        }
        form{
            p {
                "Name:" 
                input {
                    bind.input.string model.address.name (fun x-> dispatch (ChangeAddress {model.address with name = x}) )
                }
            }
            p {
                "Address line 1: " 
                input {
                    bind.input.string model.address.addr1 (fun x-> dispatch (ChangeAddress {model.address with addr1 = x}) )
                }
            }
            p {
                "City: " 
                input {
                    bind.input.string model.address.city (fun x-> dispatch (ChangeAddress {model.address with city = x}) ) 
                }
            }
            p {
                "Zip/postal code: " 
                input {
                    bind.input.string model.address.zip (fun x-> dispatch (ChangeAddress {model.address with zip = x}) ) 
                }
            }
            comp<SmartPasteButton>{
                "DefaultIcon"=> true
            }
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
