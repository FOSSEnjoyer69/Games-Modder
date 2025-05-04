using UnityEngine;
using UnityEngine.UIElements;

using UnityAlgorithms.External.Steam;
using System.IO;
using CSharpAlgorithms;

public class UIManager : MonoBehaviour
{
    private UIDocument ui;

    //The Walking Dead Definitive Edition
    private Toggle enableRelightToggle;

    private Button setModsButton;
    private SteamItem[] steamItems;


    private void Start()
    {
        ui = GetComponent<UIDocument>();
        steamItems = SteamLibrary.GetInstalled();
        UnityAlgorithms.Debug.Log(steamItems);

        enableRelightToggle = ui.rootVisualElement.Q<Toggle>("enable-relight-toggle");

        setModsButton = ui.rootVisualElement.Q<Button>("set-btn");
        setModsButton.RegisterCallback<ClickEvent>(ev => SetMods());
    }

    private void SetMods()
    {
        SetRelight();

        void SetRelight()
        {
            DirectoryInfo gameFilesFolder = new DirectoryInfo("Assets/App/Mods/Telltale/The Walking Dead/Definitive Edition/RelightV0_2_1/MOVE_FOLDERS_INSIDE_TO_GAME_DIRECTORY");
            DirectoryInfo archiveFilePaths = new DirectoryInfo("Assets/App/Mods/Telltale/The Walking Dead/Definitive Edition/RelightV0_2_1/MOVE_CONTENTS_TO_GAME_ARCHIVES_FOLDER");
            
            SteamItem theWalkingDeadDefinitiveEdition = steamItems.FindWithName("The Walking Dead The Telltale Definitive Series");

            DirectoryInfo gameDirectory = new DirectoryInfo(theWalkingDeadDefinitiveEdition.Path);
            DirectoryInfo gameArchiveDirectory = new DirectoryInfo($"{theWalkingDeadDefinitiveEdition.Path}/Archives");

            if (enableRelightToggle.value)
            {    
                DirectoryTools.CopyContents(gameFilesFolder.FullName, gameDirectory.FullName);
                DirectoryTools.CopyContents(archiveFilePaths.FullName, gameArchiveDirectory.FullName);    
            }
            else
            {
                DirectoryTools.DeleteUsingTemplate(gameDirectory.FullName, gameFilesFolder.FullName);
                DirectoryTools.DeleteUsingTemplate(gameArchiveDirectory.FullName, archiveFilePaths.FullName);
            }
        }
    }
}
