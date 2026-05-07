module DungeonOfDarkness.Logic

open DungeonOfDarkness.Domain

let initialState =
    { Player =
        { Position = { X = 480; Y = 270 }
          Facing = Right
          EquippedWeapon = SwordWeapon() :> IWeapon
          Speed = 4
          HP = 20
          Gold = 0 }
      Enemies =
        [ { Position = { X = 640; Y = 270 }
            HP = 5 } ]
      Room =
        { Number = 1
          Depth = 0
          IsCleared = false } }

let private clamp minValue maxValue value =
    value |> max minValue |> min maxValue

let private updatePlayerPosition input player =
    let dx =
        match input.MoveLeft, input.MoveRight with
        | true, false -> -player.Speed
        | false, true -> player.Speed
        | _ -> 0

    let dy =
        match input.MoveUp, input.MoveDown with
        | true, false -> -player.Speed
        | false, true -> player.Speed
        | _ -> 0

    let minX = World.roomX + World.playerRadius
    let maxX = World.roomX + World.roomWidth - World.playerRadius
    let minY = World.roomY + World.playerRadius
    let maxY = World.roomY + World.roomHeight - World.playerRadius

    let facing =
        match dx, dy with
        | value, 0 when value < 0 -> Left
        | value, 0 when value > 0 -> Right
        | 0, value when value < 0 -> Up
        | 0, value when value > 0 -> Down
        | _ -> player.Facing

    { player with
        Facing = facing
        Position =
            { X = player.Position.X + dx |> clamp minX maxX
              Y = player.Position.Y + dy |> clamp minY maxY } }

let update input state =
    { state with Player = updatePlayerPosition input state.Player }
