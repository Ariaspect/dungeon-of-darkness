module DungeonOfDarkness.Logic

open DungeonOfDarkness.Domain

let initialState =
    { Player =
        { Position = { X = 480.0f; Y = 270.0f }
          Velocity = { X = 0.0f; Y = 0.0f }
          Facing = Right
          EquippedWeapon = SwordWeapon() :> IWeapon
          AttackCooldownRemaining = 0.0f
          AttackVisualTimer = 0.0f
          Speed = 260.0f
          HP = 20
          Gold = 0 }
      Enemies =
        [ { Position = { X = 640.0f; Y = 270.0f }
            Stats = SlimeEnemy() :> IEnemy } ]
      Room =
        { Number = 1
          Depth = 0
          IsCleared = false } }

let private clamp minValue maxValue value =
    value |> max minValue |> min maxValue

let private length x y =
    sqrt (x * x + y * y)

let private lerp current target amount =
    current + (target - current) * amount

let private updatePlayerPosition input player =
    let inputX =
        match input.MoveLeft, input.MoveRight with
        | true, false -> -1.0f
        | false, true -> 1.0f
        | _ -> 0.0f

    let inputY =
        match input.MoveUp, input.MoveDown with
        | true, false -> -1.0f
        | false, true -> 1.0f
        | _ -> 0.0f

    let inputLength = length inputX inputY

    let directionX, directionY =
        if inputLength > 0.0f then
            inputX / inputLength, inputY / inputLength
        else
            0.0f, 0.0f

    let targetVelocity =
        { X = directionX * player.Speed
          Y = directionY * player.Speed }

    let responseRate = if inputLength > 0.0f then 18.0f else 24.0f
    let lerpAmount = clamp 0.0f 1.0f (responseRate * input.DeltaTime)

    let velocity =
        { X = lerp player.Velocity.X targetVelocity.X lerpAmount
          Y = lerp player.Velocity.Y targetVelocity.Y lerpAmount }

    let minX = float32 (World.roomX + World.playerRadius)
    let maxX = float32 (World.roomX + World.roomWidth - World.playerRadius)
    let minY = float32 (World.roomY + World.playerRadius)
    let maxY = float32 (World.roomY + World.roomHeight - World.playerRadius)

    let nextPosition =
        { X = player.Position.X + velocity.X * input.DeltaTime |> clamp minX maxX
          Y = player.Position.Y + velocity.Y * input.DeltaTime |> clamp minY maxY }

    let velocity =
        { X = if nextPosition.X = minX || nextPosition.X = maxX then 0.0f else velocity.X
          Y = if nextPosition.Y = minY || nextPosition.Y = maxY then 0.0f else velocity.Y }

    let facing =
        match directionX, directionY with
        | value, 0.0f when value < 0.0f -> Left
        | value, 0.0f when value > 0.0f -> Right
        | 0.0f, value when value < 0.0f -> Up
        | 0.0f, value when value > 0.0f -> Down
        | _ -> player.Facing

    { player with
        Velocity = velocity
        Facing = facing
        Position = nextPosition }

let private createEnemyStats enemyType health damage gold =
    let mutable health = health
    let mutable damage = damage
    let mutable gold = gold

    { new IEnemy with
        member _.EnemyType = enemyType

        member _.Health
            with get () = health
            and set value = health <- value

        member _.Damage
            with get () = damage
            and set value = damage <- value

        member _.Gold
            with get () = gold
            and set value = gold <- value }

let private enemyWithHealth health enemy =
    { enemy with
        Stats = createEnemyStats enemy.Stats.EnemyType health enemy.Stats.Damage enemy.Stats.Gold }

let private tickAttackTimers deltaTime player =
    { player with
        AttackCooldownRemaining = player.AttackCooldownRemaining - deltaTime |> max 0.0f
        AttackVisualTimer = player.AttackVisualTimer - deltaTime |> max 0.0f }

let private isEnemyInSwordCone (player: Player) (enemy: Enemy) =
    let dx = enemy.Position.X - player.Position.X
    let dy = enemy.Position.Y - player.Position.Y
    let range = player.EquippedWeapon.Range
    let halfWidthAtMaxRange = range * 0.55f
    let enemyRadius = float32 World.enemyRadius

    let forward, perpendicular =
        match player.Facing with
        | Up -> -dy, abs dx
        | Down -> dy, abs dx
        | Left -> -dx, abs dy
        | Right -> dx, abs dy

    forward >= -enemyRadius
    && forward <= range + enemyRadius
    && perpendicular <= (forward |> max 0.0f) / range * halfWidthAtMaxRange + enemyRadius

let private applySwordAttack state =
    let damage = state.Player.EquippedWeapon.Damage

    let damagedEnemies =
        state.Enemies
        |> List.map (fun enemy ->
            if isEnemyInSwordCone state.Player enemy then
                enemyWithHealth (enemy.Stats.Health - damage) enemy
            else
                enemy)

    let defeatedGold =
        damagedEnemies
        |> List.filter (fun enemy -> enemy.Stats.Health <= 0)
        |> List.sumBy (fun enemy -> enemy.Stats.Gold)

    let remainingEnemies =
        damagedEnemies
        |> List.filter (fun enemy -> enemy.Stats.Health > 0)

    { state with
        Player =
            { state.Player with
                Gold = state.Player.Gold + defeatedGold
                AttackCooldownRemaining = state.Player.EquippedWeapon.Cooldown
                AttackVisualTimer = 0.12f }
        Enemies = remainingEnemies
        Room = { state.Room with IsCleared = remainingEnemies.IsEmpty } }

let update input state =
    let movedState =
        { state with
            Player =
                state.Player
                |> tickAttackTimers input.DeltaTime
                |> updatePlayerPosition input }

    if input.IsAttacking && movedState.Player.AttackCooldownRemaining <= 0.0f then
        applySwordAttack movedState
    else
        movedState
