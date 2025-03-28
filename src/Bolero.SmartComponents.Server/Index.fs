module Bolero.SmartComponents.Server.Index

open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web
open Bolero
open Bolero.Html
open Bolero.Server.Html
open Bolero.SmartComponents

let page = doctypeHtml {
    head {
        meta { attr.charset "UTF-8" }
        meta { attr.name "viewport"; attr.content "width=device-width, initial-scale=1.0" }
        title { "Bolero Application" }
        ``base`` { attr.href "/" }
        link { attr.rel "stylesheet"; attr.href "Bolero.SmartComponents.Client.styles.css" }
    }
    body {
        div {
            attr.id "main"
            comp<Client.Main.MyApp> { attr.renderMode (InteractiveWebAssemblyRenderMode(false)) }
        }
        boleroScript
    }
}

[<Route "/{*path}">]
type Page() =
    inherit Bolero.Component()
    override _.Render() = page
