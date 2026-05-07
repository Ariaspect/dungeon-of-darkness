module DungeonOfDarkness.Domain

type Position =
    { X: int
      Y: int }

type FacingDirection =
    | Up
    | Down
    | Left
    | Right

type IWeapon =
    abstract Name: string
    abstract Damage: int with get, set
    abstract Cooldown: float32 with get, set
    abstract Range: float32 with get, set

type SwordWeapon(?damage: int, ?cooldown: float32, ?range: float32) =
    let mutable damage = defaultArg damage 5
    let mutable cooldown = defaultArg cooldown 0.45f
    let mutable range = defaultArg range 90.0f

    interface IWeapon with
        member _.Name = "Sword"

        member _.Damage
            with get () = damage
            and set value = damage <- value

        member _.Cooldown
            with get () = cooldown
            and set value = cooldown <- value

        member _.Range
            with get () = range
            and set value = range <- value

type Player =
    { Position: Position
      Facing: FacingDirection
      EquippedWeapon: IWeapon
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
      MoveDown: bool
      IsAttacking: bool }

module World =
    let screenWidth = 960
    let screenHeight = 540

    let roomX = 80
    let roomY = 80
    let roomWidth = 800
    let roomHeight = 380

    let playerRadius = 18
    let enemyRadius = 18
