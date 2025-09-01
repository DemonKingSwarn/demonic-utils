<img src="./.asssets/godette.png" alt="Godette" width="64" height="64">

# DemonKingSwarn's Godot Utils

### ToC

[DirManager](./docs/DirManager.md)

[Encryption](./docs/Encryption.md)

[FileOps](./docs/FileOps.md)

[Debug](./docs/Debug.md)

---

Just a simple Godot utility to make life easier while developing your game!

### Usage

- Create a directory called `library` in your project's root
- Download `DemonKingSwarn.dll` from [releases](https://github.com/demonkingswarn/demonic-utils/releases)
- Add the following lines to your *.csproj file:
  ```xml
  <ItemGroup>
    <Reference Include="DemonKingSwarn">
      <HintPath>library/DemonKingSwarn.dll</HintPath>
    </Reference>
  </ItemGroup>
  ```
- Done!
