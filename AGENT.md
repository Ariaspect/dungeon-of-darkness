# AI Helper Instructions

You are AI Helper for the `Dungeon of Darkness` game development project.

## Project Identity

- Project: `Dungeon of Darkness`
- Language: F#
- Runtime: .NET 10
- Framework: Raylib-cs
- Genre: 2D top-down hack-and-slash roguelike
- Main spec: `Dungeon_of_Darkness.pdf`

## Primary Goal

Help implement a complete playable F# Raylib-cs game that satisfies the term project specification. Prioritize working game logic, clear state transitions, and reliable build/run behavior over elaborate architecture.

## Required Game Features

- Top-down rectangular dungeon rooms that start dark and become lit when cleared.
- Playable character with movement, HP, weapon attack, and dash cooldown.
- HUD displaying HP, gold, current room number, and current depth.
- Sequential room progression starting at room 1.
- Combat rooms with enemies; room clears when all enemies are defeated.
- At least 3 enemy types with HP, speed, and damage.
- At least 3 weapon types with different attack behavior, damage, cooldown, and hit area/range.
- Exactly one equipped weapon at a time.
- Multiple active relics, each with written gameplay effects.
- Gold starting at 0; defeated enemies award/drop gold.
- Reward choice after each cleared combat room: show 3 options, select exactly 1, discard the rest.
- Shop event/room at room 4 of each depth with at least 3 purchasable options and prices.
- Depth starts at 0 and increases after every 8 cleared rooms.
- Enemy HP and damage scale with depth.
- On depth increase, player selects one debuff from three options.
- Game ends when player HP reaches 0.
- Game-over screen shows final room count, depth reached, enemies slain, remaining gold, final weapon, relic count, and fixed-formula final score.
- Game-over screen allows restart or exit.
- Repository must include `README.md` with build and run instructions.

## Engineering Guidance

- Keep changes small, direct, and compatible with F# compilation order.
- Prefer simple immutable records plus explicit game-state transitions; use mutable fields only where Raylib frame-loop pragmatism benefits clarity.
- Keep core game logic testable when possible by separating calculations from drawing/input.
- Keep rendering code straightforward: draw room, entities, attacks/effects, UI, overlays, and menus in a predictable order.
- Avoid introducing large abstractions before the game loop and state machine require them.
- Use Raylib-cs APIs carefully from F#; convert `CBool` values to `bool` when needed.
- Avoid committing generated build artifacts from `bin/` and `obj/`.
- Assets and sprites may be generated later with other AI tools; until assets exist, use simple Raylib shapes, colors, and text labels.

## Suggested Implementation Shape

- `GameState`: current mode, player, room, enemies, rewards/shop/debuff choices, stats, random source.
- Modes: title or playing, reward selection, shop, depth debuff selection, game over.
- Data definitions: weapons, relics, enemies, rewards, shop items, debuffs.
- Update step: input, movement, dash, attacks, enemy AI, collisions, room clear, rewards, shops, depth transitions, death.
- Draw step: room darkness/light state, player, enemies, weapon attack visualization, HUD, menus, game-over summary.

## Verification

- Run `dotnet build` after code changes.
- For gameplay changes, run `dotnet run` when feasible and confirm the window opens and basic controls still work.
- If adding documentation only, no build is required unless project files changed.
