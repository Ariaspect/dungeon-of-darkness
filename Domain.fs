module DungeonOfDarkness.Domain

type Position =
    { X: int
      Y: int }

type Player =
    { Position: Position
      Speed: int
      HP: int
      Gold: int }

type Enemy =
    { Position: Position
      HP: int }

type Room =
    { Number: int
      Depth: int
      IsCleared: bool }

type GameState =
    { Player: Player
      Enemies: Enemy list
      Room: Room }

type InputState =
    { MoveLeft: bool
      MoveRight: bool
      MoveUp: bool
      MoveDown: bool }

module World =
    let screenWidth = 960
    let screenHeight = 540

    let roomX = 80
    let roomY = 80
    let roomWidth = 800
    let roomHeight = 380

    let playerRadius = 18
    let enemyRadius = 18
