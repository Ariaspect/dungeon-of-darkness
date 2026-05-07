module DungeonOfDarkness.Renderer

open Raylib_cs
open DungeonOfDarkness.Domain

let draw state =
    Raylib.BeginDrawing()
    Raylib.ClearBackground Color.Black

    let roomColor = if state.Room.IsCleared then Color.DarkGreen else Color.DarkBlue

    Raylib.DrawRectangle(World.roomX, World.roomY, World.roomWidth, World.roomHeight, roomColor)
    Raylib.DrawRectangleLines(World.roomX, World.roomY, World.roomWidth, World.roomHeight, Color.RayWhite)

    Raylib.DrawCircle(state.Player.Position.X, state.Player.Position.Y, float32 World.playerRadius, Color.SkyBlue)

    for enemy in state.Enemies do
        Raylib.DrawCircle(enemy.Position.X, enemy.Position.Y, float32 World.enemyRadius, Color.Maroon)

    Raylib.DrawText("Dungeon of Darkness", 24, 20, 28, Color.RayWhite)
    Raylib.DrawText("WASD: move | ESC: quit", 24, 490, 20, Color.LightGray)

    let hud =
        $"HP: {state.Player.HP} | Gold: {state.Player.Gold} | Room: {state.Room.Number} | Depth: {state.Room.Depth}"

    Raylib.DrawText(hud, 560, 28, 20, Color.Gold)

    Raylib.EndDrawing()
