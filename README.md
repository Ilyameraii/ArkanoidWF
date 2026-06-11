# 🧱 ArkanoidWF

A classic **Arkanoid / Breakout** game built with **C# .NET 8 and Windows Forms**, featuring angle-based ball physics, multi-hit bricks, and a Dota 2-themed visual style.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D6?logo=windows)
![Language](https://img.shields.io/badge/Language-C%23-239120?logo=csharp)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📸 Screenshots

> _Add gameplay screenshots here — Start screen, game in progress, victory/defeat screens._

---

## ✨ Features

- **Smooth 120 Hz game loop** — timer interval is synced to monitor refresh rate via `MonitorParameters`
- **Realistic ball physics** — angle-based movement using trigonometry (`Math.Cos` / `Math.Sin`), with proper angle normalization
- **Precise collision detection** — AABB circle-rectangle test using the nearest-point algorithm; hit side (Top / Bottom / Left / Right) is resolved by minimum overlap to prevent tunneling
- **Dynamic platform bounce** — ball's exit angle depends on where it hits the platform (edge → steep angle, center → shallow angle), up to ±60°
- **Multi-hit bricks** — each brick has 1–3 HP; color transitions from **green → yellow → red** as it takes damage
- **Clean separation of concerns** — game logic lives entirely in `GameCore` and models; the form only renders and forwards input
- **Screen management** — Start, Game, Win, and Lose screens implemented as `UserControl` overlays with event-driven transitions
- **Double-buffered rendering** — flicker-free GDI+ drawing via `DoubleBuffered = true`
- **Responsive exit button** — three-state image button (Default / Hover / Click) with a confirmation dialog

---

## 🏗️ Project Structure

```
ArkanoidWF/
├── Constants/               # Centralized game parameters (sizes, speeds, colors, images)
│   ├── BallParameters.cs
│   ├── BrickColors.cs
│   ├── BrickParameters.cs
│   ├── MonitorParameters.cs
│   └── PlayerPlatformParameters.cs
│
├── Enums/
│   └── HitSide.cs           # Top / Bottom / Left / Right — used in collision resolution
│
├── Interfaces/
│   └── IRectangle.cs        # Abstraction for collidable rectangular objects
│
├── Models/                  # Pure game logic — no UI dependencies
│   ├── Ball.cs              # Movement, wall bounce, rectangle collision
│   ├── Brick.cs             # HP system, color by HP, IRectangle implementation
│   ├── FloatPoint.cs        # Lightweight 2D point (float precision)
│   ├── GameCore.cs          # Orchestrates the game loop: Tick(), state flags
│   └── PlayerPlatform.cs    # Platform movement, IRectangle implementation
│
├── Rendering/               # WinForms UI layer
│   ├── MainForm.cs          # Game loop timer, GDI+ rendering, keyboard input
│   ├── ResultUC.cs          # Win / Lose screen UserControl
│   └── StartUC.cs           # Start screen UserControl
│
└── Resources/               # Embedded images (Dota 2 themed assets)
```

---

## ⚙️ Technical Highlights

### Collision System
The `IRectangle` interface decouples the collision logic from concrete types. Both `Brick` and `PlayerPlatform` implement it, allowing `Ball` to use a single generic `BounceOffRectangle` method with an `Action<HitSide>` callback that lets each object type customize the horizontal bounce response:

```csharp
// Ball.cs
private void BounceOffRectangle(IRectangle rect, Action<HitSide> onHorizontalBounce)
{
    if (!IsBallCollidesWith(rect)) return;
    var side = GetHitSide(rect);
    // push-out correction + direction change
    onHorizontalBounce?.Invoke(side);
}
```

### Platform Angle Control
When the ball hits the platform, the exit angle is not a simple reflection — it's calculated from the relative hit position, giving the player control over direction:

```csharp
var relativeX = (Center.X - platform.X) / platform.Width;
var normalized = Math.Clamp(2 * relativeX - 1, -1f, 1f);
Angle = -MathF.PI / 2 + normalized * maxBounceAngle; // ±60°
```

### Proportional Constants
All sizes and speeds are derived from a single `MonitorParameters.MaximizeWidth` constant, making the layout scale predictably:

```csharp
public const int Width  = MonitorParameters.MaximizeWidth / 16;   // brick width
public const int Size   = MonitorParameters.MaximizeWidth / 40;   // ball size
public const float Speed = Width / 10;                             // platform speed
```

---

## 🚀 Getting Started

### Requirements

- Windows OS
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (or Rider) with the **Windows Forms** workload

### Build & Run

```bash
git clone https://github.com/your-username/ArkanoidWF.git
cd ArkanoidWF/ArkanoidWF
dotnet run
```

Or open `ArkanoidWF.sln` in Visual Studio and press **F5**.

---

## 🎮 Controls

| Key | Action |
|-----|--------|
| `←` / `A` | Move platform left |
| `→` / `D` | Move platform right |

---

## 🧩 How It Works

1. On launch the **Start screen** is shown as a `UserControl` overlay.
2. Clicking **Start** creates a new `GameCore` instance and starts the `System.Windows.Forms.Timer`.
3. Each timer tick calls `GameCore.Tick()` — which moves the ball, handles collisions, removes destroyed bricks, and checks win/lose conditions — then calls `Invalidate()` to trigger a repaint.
4. `OnPaint` reads only the public surface of `GameCore` (coordinates and brick list) and draws everything with GDI+.
5. When the game ends, the timer stops and the **Win** or **Lose** `UserControl` is shown.
6. Pressing **Restart** clears all overlays and returns to the Start screen.

---

## 📄 License

This project is licensed under the **MIT License** — see [LICENSE](LICENSE) for details.
