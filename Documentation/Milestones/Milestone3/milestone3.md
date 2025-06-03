## Apurva Mishra

In this milestone, I focused on adding audio feedback for all enemies and the final level boss. I also implemented the victory condition for the final level, and fixed some gameplay bugs.

---

### Enemy & Boss Audio Integration

I began my task by adding methods the `AudioManager` for enemy and boss sound effects. These included `PlayEnemyAttackSound()`, `PlayEnemyHurtSound()`, `PlayEnemyDeathSound()` and vice versa for the boss. Then I created boss and enemy audio controller scripts to act as bridges between gameplay logic and the audio system.

I added `EnemyAudioController` to all enemy prefabs. Then I updated the scripts to trigger sounds during combat. In `EnemyCombat`, attack sound was played during `TryAttack()`:

```csharp
public void TryAttack()
        {
            if (!IsCooldownComplete()) return;
            _animator.SetTrigger(Attack1);
            _audioController.PlayAttackSound();
            ResetCooldown();
        }
```

In `EnemyHealth`, hurt and death sound was triggered when taking damage:

```csharp
public void DecreaseEnemyHealth(int damage)
        {
            _currentHealth -= damage;
            EnemyAudioController.PlayHurtSound();
            healthBar.SetHealthLevel(_currentHealth);

            if (_currentHealth <= 0)
            {
                EnemyAudioController.PlayDeathSound();
                Die();
            }
        }
```

For enemies using collision-based damage,aka the rock enemy, I called sound method directly inside collision triggers to match their custom behavior.

The boss followed the same pattern. I attached a `BossAudioController` to the boss prefab and added calls in the `BossController` to play attack, hurt, and death sounds based on the boss's current state:

```csharp
audioController.PlayAttackSound();
audioController.PlayHurtSound();
audioController.PlayDeathSound();
```

All sound logic was kept modular. This kept the controller scripts simple, while all actual playback and sound asset management remained in the `AudioManager`.

---

### Final Level Victory Conditions

I also implemented the **victory condition** for the final boss fight. After the boss is defeated, the game plays the level-complete sound and returns to the main menu after a short delay. This was handled by a separate `FinalLevelEnd` script placed on a GameObject in the scene. The `BossController` calls `HandleBossDefeat()` when the boss dies, which plays the level completed sound and switches to main menu.
```csharp
public void HandleBossDefeat()
    {
        AudioManager.Instance.PlayLevelCompleteAudio();
        Invoke(nameof(GoToMainMenu), delayBeforeMenu);
    }
```

---

### Bug Fixes

To wrap up the milestone, I fixed some gameplay issues in the final level:

* **Water death:** Player dies when touching tiles marked with the water layer.
* **Delay before changing scenes:** Ensured the scene only changes after the boss finishes dying.
* **Tilemap cleanup:** Removed unnecessary/stray tiles and adjusted sorting layers.

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



