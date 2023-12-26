open System
open System.IO
open System.Net.Http

// Publish command: dotnet publish -c Release -r win10-x64
// Exe is in bin\Release\net7.0\win10-x64\publish\MinecraftPrivateServerModsUpdater.exe
try
  let modpackUrl = "http://78.156.125.32/modpack"
  let userDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
  let userDirPath = Path.GetFullPath($"{userDir}")
  let modDirName = "flurry97_private_server_mods"

  let httpClient = new HttpClient()

  printfn $"Downloading modpack info from {modpackUrl}..."
  let modpackResponse = httpClient.GetAsync(modpackUrl) |> Async.AwaitTask |> Async.RunSynchronously

  printfn "Reading modpack info..."
  let modpackBytes = modpackResponse.Content.ReadAsByteArrayAsync() |> Async.AwaitTask |> Async.RunSynchronously

  printfn $"Writing modpack info to {userDirPath}\{modDirName}.zip..."
  File.WriteAllBytes($"{userDirPath}\{modDirName}.zip", modpackBytes)

  printfn "Press any key to exit"
  Console.ReadKey() |> ignore
with ex ->
  printfn $"Error: {ex.Message}\n"
  printfn "Press any key to exit"
  Console.ReadKey() |> ignore
