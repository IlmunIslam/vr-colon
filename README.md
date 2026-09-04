# VR-Colon — Interactive Colon Endoscopy Visualization

An interactive 3D visualization of the human colon, built in Unity with the **High
Definition Render Pipeline (HDRP)**. A first-person camera is navigated through the
colon lumen in an endoscopy-style view, with collision feedback when the scope
contacts the bowel wall — intended as an educational / medical-visualization tool.

> **Disclaimer:** This project is for **educational and visualization purposes
> only**. It is **not a medical device** and must **not** be used for clinical,
> diagnostic, or treatment decisions.

## Tech stack

- **Engine:** Unity **2022.3.4f1** (LTS)
- **Render pipeline:** High Definition Render Pipeline (**HDRP 14.0.8**)
- **Language:** C# (MonoBehaviour scripts)
- **Large assets:** tracked via **Git LFS** (see below)

## Navigation and collision

Three camera controllers are included, representing different approaches to moving
a scope through a closed tubular mesh:

| Script | Approach |
|---|---|
| `CameraCollision.cs` | **Predictive** — tests the *target* position with `Physics.CheckSphere` against a colon layer mask *before* moving. If the move would intersect the wall, the camera reverts to its last known safe position. |
| `CameraMovement.cs` | **Kinematic** — a Rigidbody driven with `MovePosition`, letting the physics engine resolve depenetration. |
| `PipeMovement.cs` | **Force-based** — `AddForce` / `MoveRotation` for momentum-driven scope movement. |

`CameraCollision` is the most robust of the three: because it validates the
destination *before* committing to it, the camera cannot tunnel through a thin
bowel wall at speed — the failure mode that makes naive collider-based navigation
unusable inside a hollow mesh. It maintains a `lastSafePosition` and shows an
on-screen "Collision detected" warning via `OnGUI` for 1.5 seconds after contact.

`CollisionAlert.cs` provides the alternative event-driven path, toggling a UI canvas
on `OnCollisionEnter` / `OnCollisionExit` against the colon mesh.

## Controls

| Input | Action |
|---|---|
| `W` / `S` / `A` / `D` | Move the camera forward / back / strafe |
| `Q` / `E` | Move down / up |
| Arrow keys | Look around (pitch and yaw) |

Speeds and the collision offset are serialized fields, tunable in the Inspector.

## Key assets

- **`Assets/Imported/motacolon.blend`** — the source Blender model of the colon
  anatomy (~75 MB), the master mesh the scene is built from.
- **`Assets/Imported/tube.blend`** — simplified tube geometry used for navigation
  and collision testing.
- **`Assets/Materials/NEW COLON.tga`** — the high-resolution colon wall texture (64 MB).
- **`Assets/OutdoorsScene.unity`** — the main scene, with HDRP Global Volume profiles.
- **`Assets/Settings/`** — HDRP quality tiers (Performant / Balanced / High Fidelity)
  and the sky & fog volume profile.

`*.blend`, `*.fbx`, and `*.tga` are stored in **Git LFS** (see `.gitattributes`), so
**Git LFS must be installed before cloning** — otherwise you get small pointer files
instead of the real models.

## Getting started

1. Install **Unity Hub** and **Unity 2022.3.4f1** (LTS).
2. Install Git LFS and initialize it *before* cloning:
   ```bash
   git lfs install
   git clone https://github.com/IlmunIslam/vr-colon.git
   ```
3. Open the project folder in Unity Hub (HDRP packages resolve on first import).
4. Open `Assets/OutdoorsScene.unity` and press **Play**.

## Project status

Actively developed. Current known gaps:

- `EndoscopyVignetteHDRP.cs` is an **empty placeholder** — the endoscope vignette
  post-effect has an HDRP material (`EndoscopyVignetteMat.mat`) but the HDRP custom-pass
  implementation is not yet written. (A working URP version of this effect exists in
  [Stomach_URP](https://github.com/IlmunIslam/Stomach_URP).)
- Despite the project name, no XR plugin is currently installed — navigation is
  keyboard-driven on desktop. HDRP + XR is the intended direction.

## Related projects

- [Stomach_URP](https://github.com/IlmunIslam/Stomach_URP) — the URP stomach counterpart, with a working endoscopy vignette shader
- [Stomach_HDRP](https://github.com/IlmunIslam/Stomach_HDRP) / [StomachFolds_HDRP](https://github.com/IlmunIslam/StomachFolds_HDRP) — HDRP stomach variants
- [mitochondria-3d](https://github.com/IlmunIslam/mitochondria-3d) — related biological modelling work

## License

Released under the **MIT License** — see [LICENSE](LICENSE).
