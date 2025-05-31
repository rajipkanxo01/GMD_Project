## Apurva Mishra

---

## Pramesh Shrestha (325833)

---

## Rajib Paudyal (325836)

For milestone 3, my main focus was improving the player experience through UI additions, audio integration and visual
polish.

### Loading Screen and Scene Transitions

I started by creating a dedicated loading screen that appears between scene transitions. It includes a
background image and a progress bar that fills as the next scene loads, providing feedback and keeping the game flow
smooth.

![Loading Screen](./loading%20screen.png)

Using Unity’s `AsyncOperation`, I created a coroutine in the `SceneLoader` script that displays a loading screen with a
progress bar while the next scene loads in the background. Once loading completes, the screen fades out and the new
scene begins.

```csharp
IEnumerator LoadSceneAsync(int sceneId)
{
    loadingScreen.SetActive(true);

    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
    
    while (!operation.isDone)
    {
        float progress = Mathf.Clamp01(operation.progress / 0.9f);
        loadingBarFill.fillAmount = progress;
        yield return null;
    }
    
    PlayLevelMusic(sceneId);
    loadingScreen.SetActive(false);
}
```

I also connected scene changes with level-specific audio by calling `PlayLevelMusic(sceneId)` after loading
finishes. This method plays different background tracks depending on the level using the `AudioManager`, enhancing the
atmosphere of each stage. This setup not only improves player feedback during transitions but also ensures audio starts
at the right time, creating a smoother and more immersive experience.

```csharp
private void PlayLevelMusic(int sceneId)
{
    switch (sceneId)
    {
        case 1:
            AudioManager.Instance.PlayLevelAudio(LevelType.Level1);
            break;
        case 2:
            AudioManager.Instance.PlayLevelAudio(LevelType.Level2);
            break;
        case 3:
            AudioManager.Instance.PlayLevelAudio(LevelType.Boss);
            break;
        default:
            Debug.LogWarning("No music assigned for this scene.");
            break;
    }
}
```

I also implemented a controls screen accessible from the main menu. This screen displays the input layout for both
keyboard and gamepad, helping players understand how to interact with the game. Navigation within this UI was set up
using Unity’s EventSystem to ensure proper button selection and input handling.

In addition to UI work, I added audio to various levels and the player character. Background music now plays per level,
and the player has sound effects for jumping and other actions, enhancing immersion. I also fixed several bugs from
Level 2, including visual glitches, misaligned tiles, and small lighting issues. Overall, I focused on polishing Level
2’s visuals to bring consistency and improve the overall quality of the gameplay experience.

### Controls Screen

![Controls Screen](./controls%20screen.png)
I also created a dedicated controls screen to help players understand the input layout. The screen clearly displays the
actions such as attack, jump, interact, UI select and pause—mapped to both keyboard and gamepad buttons. I designed the
layout to match the game’s visual style, using custom icons, color-coded buttons and a wooden frame for consistency.
The screen is accessible from the main menu and includes a "Go Back" button, with proper input navigation using Unity’s
EventSystem for smooth interaction with both keyboard and controller. This screen improves player onboarding and makes
the controls easy to reference.

### Audio Integration

To enhance feedback and immersion, I integrated audio into both player actions and level environments. I organized all
sound assets into clear categories—Levels, Player and Misc—making them easier to manage. Each level has its own
background music and the player has distinct sound effects for jumping, attacking, taking damage, dying and picking up
health. These were triggered through code and tied into gameplay events, helping reinforce actions and improve the
game’s overall feel. A "Level End" sound was also added under the Misc section to mark scene transitions.

![Audio Heirachy](./audio%20hierachy.png)

---



