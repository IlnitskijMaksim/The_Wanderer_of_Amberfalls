using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenuTests
{
    [Test]
    public void PauseMenu_Pause_Functionality()
    {
        // Arrange
        var pauseMenu = new GameObject().AddComponent<PauseMenu>();
        var pauseMenuUI = new GameObject();
        pauseMenu.pauseMenuUI = pauseMenuUI;

        // Act
        pauseMenu.Pause();

        // Assert
        Assert.IsTrue(PauseMenu.GameIsPaused);
        Assert.IsTrue(pauseMenuUI.activeSelf);
        Assert.AreEqual(Time.timeScale, 0f);
    }

    [Test]
    public void PauseMenu_Resume_Functionality()
    {
        // Arrange
        var pauseMenu = new GameObject().AddComponent<PauseMenu>();
        var pauseMenuUI = new GameObject();
        pauseMenu.pauseMenuUI = pauseMenuUI;
        // ”станавливаем GameIsPaused в true, чтобы имитировать паузу
        PauseMenu.GameIsPaused = true;
        Time.timeScale = 0f;

        // Act
        pauseMenu.Resume();

        // Assert
        Assert.IsFalse(PauseMenu.GameIsPaused);
        Assert.IsFalse(pauseMenuUI.activeSelf);
        Assert.AreEqual(Time.timeScale, 1f);
    }


    [UnityTest]
    public IEnumerator PauseMenu_LoadMenu_Functionality()
    {
        // Arrange
        SceneManager.LoadScene("Menu");
        yield return null; // Wait for scene to load

        var pauseMenu = new GameObject().AddComponent<PauseMenu>();

        // Act
        pauseMenu.LoadMenu();

        // Assert
        Assert.IsFalse(PauseMenu.GameIsPaused);
        Assert.AreEqual(Time.timeScale, 1f);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Menu");
    }

    [Test]
    public void PauseMenu_QuitGame_Functionality()
    {
        // Arrange
        var pauseMenu = new PauseMenu(); // —оздание экземпл€ра PauseMenu

        // Act
        pauseMenu.QuitGame();

        // Assert
        // Note: It's challenging to test Application.Quit() directly in unit tests.
        // It's recommended to test this functionality manually during integration or playtesting.
        // You may mock or create abstractions to isolate this functionality for easier testing.
    }
}
