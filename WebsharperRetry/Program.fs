open WebSharper
open WebSharper.Html.Server

type Endpoints =
    | [<EndPoint "GET /">] Home
    | [<EndPoint "GET /about">] About
    | [<EndPoint "GET /contact">] ContactUs

let MySite =
    Warp.CreateApplication (fun ctx endpoint ->
        let (=>) label endpoint = A [HRef (ctx.Link endpoint)] -< [Text label]
        match endpoint with
        | Endpoints.Home ->
            Warp.Page(
                Body =
                    [
                        H1 [Text "Hello world!"]
                        "About" => Endpoints.About
                        H2 [Text "Welcome to the F# Web App Demo"]
                        "Contact Us" => Endpoints.ContactUs
                    ]
            )
        | Endpoints.About ->
            Warp.Page(
                Body = 
                    [
                        "Home"=> Endpoints.Home
                        P [Text ""]
                        "Contact" => Endpoints.ContactUs
                        P [Text "A simple multi-page F# web app :)"]
                        P [Text "F# is a strongly typed, type-driven language."]
                        P [Text "As you can see, the type 'Endpoints' act like Rails' routes."]
                    ]
            )
        | Endpoints.ContactUs -> 
            Warp.Page(
               
                Body =
                    [
                        H1 [Text "Contact Us"]
                        "Home" => Endpoints.Home


                    ]
            )
    )

[<EntryPoint>]
do Warp.RunAndWaitForInput(MySite) |> ignore