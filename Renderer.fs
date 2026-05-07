module DungeonOfDarkness.Renderer

open System.Numerics
open Raylib_cs
open DungeonOfDarkness.Domain

let private drawSwordCone (player: Player) =
    let origin = Vector2(float32 player.Position.X, float32 player.Position.Y)
    let range = player.EquippedWeapon.Range
    let halfWidth = range * 0.55f

    let leftPoint, rightPoint =
        match player.Facing with
        | Up ->
            Vector2(origin.X - halfWidth, origin.Y - range),
            Vector2(origin.X + halfWidth, origin.Y - range)
        | Down ->
            Vector2(origin.X + halfWidth, origin.Y + range),
            Vector2(origin.X - halfWidth, origin.Y + range)
        | Left ->
            Vector2(origin.X - range, origin.Y + halfWidth),
            Vector2(origin.X - range, origin.Y - halfWidth)
        | Right ->
            Vector2(origin.X + range, origin.Y - halfWidth),
            Vector2(origin.X + range, origin.Y + halfWidth)

    Raylib.DrawTriangle(origin, rightPoint, leftPoint, Color.Gold)
    Raylib.DrawTriangleLines(origin, rightPoint, leftPoint, Color.Yellow)

let draw (input: InputState) (state: GameState) =
    Raylib.BeginDrawing()
    Raylib.ClearBackground Color.Black

    let roomColor = if state.Room.IsCleared then Color.DarkGreen else Color.DarkBlue

    Raylib.DrawRectangle(World.roomX, World.roomY, World.roomWidth, World.roomHeight, roomColor)
    Raylib.DrawRectangleLines(World.roomX, World.roomY, World.roomWidth, World.roomHeight, Color.RayWhite)

    for enemy in state.Enemies do
        Raylib.DrawCircle(enemy.Position.X, enemy.Position.Y, float32 World.enemyRadius, Color.Maroon)

    if input.IsAttacking then
        drawSwordCone state.Player

    Raylib.DrawCircle(state.Player.Position.X, state.Player.Position.Y, float32 World.playerRadius, Color.SkyBlue)

    Raylib.DrawText("Dungeon of Darkness", 24, 20, 28, Color.RayWhite)
    Raylib.DrawText("WASD: move | SPACE: sword | ESC: quit", 24, 490, 20, Color.LightGray)

    let hud =
        $"HP: {state.Player.HP} | Gold: {state.Player.Gold} | Room: {state.Room.Number} | Depth: {state.Room.Depth}"

    Raylib.DrawText(hud, 560, 28, 20, Color.Gold)

    Raylib.EndDrawing()
