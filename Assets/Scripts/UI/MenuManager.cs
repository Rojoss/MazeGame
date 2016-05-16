using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance {get; private set;}

    public string menuOnStart = "Main";
    private List<string> stack = new List<string>();

    private Dictionary<string, Menu> menus = new Dictionary<string, Menu>();

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        //Load menus
        Menu[] menus = FindObjectsOfType<Menu>();
        for (int i = 0; i < menus.Length; i++) {
            registerMenu(menus[i]);
        }

        //Open first menu
        Open(menuOnStart);
    }


    /// <summary>
    /// Register a menu to the menu manager so that it can be loaded.
    /// All menus that have the Monobehaviour of Menu will be loaded on startup.
    /// </summary>
    /// <param name="menu">The menu container/transform to register.</param>
    public void registerMenu(Menu menu) {
        if (menus.ContainsKey(menu.name.ToLower())) {
            Debug.LogWarning("Trying to register a menu that's already registered. [name=" + menu.name + "]");
            return;
        }
        menus.Add(menu.name.ToLower(), menu);
        menu.gameObject.SetActive(false);
    }

    /// <summary>
    /// Unregister a menu from the menu manager.
    /// </summary>
    /// <param name="menu">The menu container/transform to unregister.</param>
    public void unregisterMenu(Menu menu) {
        unregisterMenu(menu.name.ToLower());
    }

    /// <summary>
    /// Unregister a menu from the menu manager.
    /// </summary>
    /// <param name="name">The name of the menu to unregister.</param>
    public void unregisterMenu(string name) {
        name = name.ToLower();
        if (menus.ContainsKey(name.ToLower())) {
            Debug.LogWarning("Trying to unregister a menu that isn't registered. [name=" + name + "]");
            return;
        }
        menus[name].gameObject.SetActive(false);
        menus.Remove(name.ToLower());
    }

    /// <summary>
    /// Get a list of all the menu instances.
    /// </summary>
    /// <returns>List of menu instances</returns>
    public List<Menu> getMenus() {
        return new List<Menu>(menus.Values);
    }

    /// <summary>
    /// Get a list of all the menu names.
    /// </summary>
    /// <returns>List of menu names</returns>
    public List<string> getMenuNames() {
        return new List<string>(menus.Keys);
    }


    /// <summary>
    /// Close the current menu.
    /// </summary>
    public void Close() {
        if (stack.Count < 1) {
            Debug.LogWarning("Trying to close the menu but there is no menu on the stack.");
            return;
        }
        menus[stack[stack.Count-1]].gameObject.SetActive(false);
    }

    /// <summary>
    /// Open a menu with the specified name.
    /// The current menu (if there is one) will be closed.
    /// </summary>
    /// <param name="menu">The name of the menu to open.</param>
    public void Open(string menu) {
        menu = menu.ToLower();

        //Deactivate previous menu
        if (stack.Count > 0) {
            menus[stack[stack.Count-1]].gameObject.SetActive(false);
        }

        //Activate menu
        menus[menu].gameObject.SetActive(true);

        //Add to stack
        stack.Add(menu);
    }
    
    /// <summary>
    /// Go back to the previous menu on the stack.
    /// </summary>
    public void Back() {
        if (stack.Count < 2) {
            Debug.LogWarning("Trying to back to the previous menu but there is none.");
            return;
        }
        Open(stack[stack.Count-2]);
    }


    /// <summary>
    /// Wrapper method to start the game.
    /// </summary>
    public void StartGame() {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Wrapper method to quit the application.
    /// </summary>
    public void QuitGame() {
        Application.Quit();
    }

}