open Raylib_cs
open DungeonOfDarkness.Domain
open DungeonOfDarkness.Input
open DungeonOfDarkness.Logic
open DungeonOfDarkness.Renderer

[<System.STAThread>]
[<EntryPoint>]
let main _ =
    Raylib.InitWindow(World.screenWidth, World.screenHeight, "Dungeon of Darkness")
    Raylib.SetTargetFPS(60)

    let mutable gameState = initialState

    while not (shouldCloseWindow ()) do
        let input = readInput ()
        gameState <- update input gameState
        draw gameState

    Raylib.CloseWindow()
    0
