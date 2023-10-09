// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"



let lst1 = [3;5;12]

let lst2 = [2;3;4;7]

let merge a b = List.append a b |> List.sort

    