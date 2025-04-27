## Apurva Misha

---

## Pramesh Shrestha
### Implementation of Health System

As part of the first development milestone, I created a health system for both the main player and the enemies in the game. This system shows health clearly on the screen and is easy to understand for players. I also made sure the code is organized and easy to update in the future, by keeping different parts of the health system in separate scripts.

**Health Bar UI**

To provide visual feedback on health status, a **canvas-based UI** was used to display health bars:

- **Main Player Health Bar:** The player's health bar is placed in the top-left corner of the screen and stays there throughout the game. This makes it easy for players to always keep an eye on their health while playing.
  
- **Enemy Health Bars:** For enemies, health bars were implemented as HUDs that appear above their heads. These bars update as they take damage and use the same style as the player’s bar, so everything looks consistent and easy to understand.
```csharp
    private void HandleInvincibility()
        {
            if (!_isInvincible) return;

            _invincibilityCooldown -= Time.deltaTime;
            if (_invincibilityCooldown <= 0)
            {
                spriteRenderer.color = normalColor;
                _isInvincible = false;
            }
        }
```

The health bar uses gradient colors to give players a quick visual cue about their health — shifting shades help them instantly see how much health they have left.


**Health Logic & Interactions**
- **Damage Handling**: The player’s health decreases upon colliding with hazardous objects or when attacked by enemies. This mechanic adds risk to exploration and combat, encouraging more strategic gameplay.
  
- **Health Recovery:** Players can regain health by collecting health collectibles, which are represented as logs — aligning with the game’s theme of a lumberjack warrior. The system ensures that health is only restored if the player's current health is below the maximum, avoiding over-healing.
- **Damage Zone Behavior**: When the player remains within a hazardous zone, health gradually decreases over time. To prevent the health from depleting too rapidly, an **invincibility window** is introduced after each hit. This implementation was inspired by best practices observed in a YouTube tutorial and adds fairness to gameplay while still maintaining challenge.



**Code Structure & Principles**
To follow software development best practices:

- A **modular structure** was adopted by separating concerns into individual scripts:
  - The **Health Bar** UI script
  - The **Player Health** logic script
  - A script for **Damage Zones**
  - A script for **Health Collectibles**
- This separation supports the **Single Responsibility Principle (SRP)** from the SOLID principles, ensuring each script handles only one aspect of the system. Notably, the player controller remains focused on player-specific behavior without being overloaded with health logic.
  
- To manage the player's health consistently throughout the game, I used the **Singleton** design pattern for the PlayerHealthController class. Since there is only one player in the game, this pattern makes it easy to access the health system from any other script without having to manually pass references. This approach keeps the code organized and avoids repetitive lookups. However, I’m also aware of the limitations of singletons, such as potential issues with testing or scalability. For the current scope of the game - with one player and three levels, using a singleton is a simple and effective solution.

---

## Rajib Paudyal (325836)

---

### Created Basic Enemy Animations for Prototyping

For the first task, I set up basic animations for a prototype enemy character—a goblin. I used Unity’s Animator to
create a state machine that controls how the goblin looks and behaves during gameplay.

![Goblin Animator Window](./Goblin_Animator.png)

The animation system includes the following key states:

- **Goblin_Idle** – the default idle animation.
- **G_Run** – plays when the goblin is moving.
- **G_Attack1** and **G_Attack2** – two different attack animations.
- **G_Hurt** – shows when the goblin takes damage.
- **G_Death** – plays when the goblin dies.

---

### Basic AI System for All Enemies

In the second task, I created a basic AI system that all enemy types can use. The enemies patrol between two points and
switch to attack mode when the player is detected.

This AI system is a **quick prototype**, and although it works well for now, I plan to refactor and improve the
structure in future milestones for better performance and flexibility.

---

#### State Management

The enemy uses a simple state machine with two states: `Patrol` and `Attack`.

```csharp
public enum EnemyState { Patrol, Attack }
```

Inside the Update() method, we check if the player is nearby using a detection function, then decide what to do:

```csharp
bool playerDetected = PlayerInRange();
UpdateState(playerDetected);

switch (_currentState)
{
    case State.Patrol:
        HandlePatrol();
        break;
    case State.Attack:
        HandleAttack();
        break;
}
```

#### Player Detection (Raycasting)

The enemy checks for the player using a BoxCast, which acts like a short-range vision field in front of the enemy.

```csharp
Vector3 origin = boxCollider.bounds.center + transform.right * enemyAttackRange * transform.localScale.x * colliderDistance;
Vector3 size = new Vector3(boxCollider.bounds.size.x * enemyAttackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);

RaycastHit2D hit = Physics2D.BoxCast(origin, size, 0, Vector2.left, 0, playerLayer);
```

This method was inspired by an online tutorial. Although raycasting can affect performance when overused, it's only
called once per enemy here, so it shouldn't cause any major issues at this stage.

#### Patrolling Behaviour

If the player is not detected, the enemy patrols between two points using the HandlePatrol() method:

```csharp
Transform target = patrolPoints[_currentPatrolIndex];
transform.position = Vector2.MoveTowards(transform.position, target.position, patrolSpeed * Time.deltaTime);
```

Once the enemy reaches a patrol point, it waits briefly before moving again. The patrol point is then switched, and the
enemy flips its sprite to face the other direction.
---
