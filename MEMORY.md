# Project Memory

## Current Repository State

- Workspace root: `/home/ariaspect/dev/class/cs20200/dungeon-of-darkness`
- Git repository: yes
- Current source files inspected:
  - `Program.fs`
  - `DungeonOfDarkness.fsproj`
  - `Dungeon_of_Darkness.pdf`
  - `.gitignore`
- Current project is a minimal F# Raylib-cs app targeting `net10.0`.
- `DungeonOfDarkness.fsproj` references `Raylib-cs` version `7.0.2`.
- `Program.fs` currently opens a 960x540 window, draws a room, player circle, enemy placeholder, and simple HUD.
- Current controls in prototype: `WASD` movement, `ESC` to quit.

## Specification Summary

`Dungeon of Darkness` must become a 2D top-down hack-and-slash roguelike. The player clears sequential combat rooms, earns/selects rewards, shops at room 4 of each depth, descends after every 8 rooms, chooses debuffs on depth changes, and dies when HP reaches 0. The game must display player state during play and final stats/score on game over.

## Key Required Counts

- At least 1 playable character.
- At least 3 enemy types.
- At least 3 weapon types.
- At least 3 reward options after each cleared combat room.
- At least 3 purchasable shop options in shop events.
- Depth increases after every 8 cleared rooms.
- Shop appears in room 4 of the depth.

## Open Implementation Work

- Replace placeholder state with structured player, enemy, room, weapon, relic, reward, shop, debuff, and score models.
- Implement combat, attack cooldowns, hit areas/ranges, dash cooldown, collision damage, enemy death, and gold awards.
- Implement room clear detection, lit-room transition, reward selection, next-room progression, depth progression, and debuff selection.
- Implement shop UI and purchase/leave behavior.
- Implement relic effects and weapon replacement.
- Implement game-over screen, restart, exit, and score formula display.
- Add `README.md` with exact build and run commands.

## Development Preferences

- Use F# and Raylib-cs only unless there is a concrete need for another dependency.
- Prefer simple shapes/text until real assets are available.
- Keep gameplay values easy to tune: HP, speeds, damage, cooldowns, gold rewards, shop prices, and depth scaling.
- Keep all user-visible reward, relic, weapon, shop, and debuff effects written clearly on screen.
- Do not make git commits unless explicitly prompted.
- Avoid excessive testing; the user will test gameplay manually.
- Focus verification on F# syntax correctness, compilation viability, and semantic correctness of the game logic.
