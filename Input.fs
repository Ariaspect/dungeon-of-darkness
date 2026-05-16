module DungeonOfDarkness.Input

open Raylib_cs
open DungeonOfDarkness.Domain

let private cbool (value: CBool) : bool = CBool.op_Implicit value

let readInput deltaTime =
    { MoveLeft = cbool (Raylib.IsKeyDown KeyboardKey.A)
      MoveRight = cbool (Raylib.IsKeyDown KeyboardKey.D)
      MoveUp = cbool (Raylib.IsKeyDown KeyboardKey.W)
      MoveDown = cbool (Raylib.IsKeyDown KeyboardKey.S)
      IsAttacking = cbool (Raylib.IsKeyDown KeyboardKey.Space)
      DeltaTime = deltaTime }

let shouldCloseWindow () =
    cbool (Raylib.WindowShouldClose())
