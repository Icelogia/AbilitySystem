# Ability System

## Table of Contents
1. [Introduction](#introduction)
2. [Installation](#installation)
3. [Concepts](#concepts)
   1. [Effectors](#1-effectors)
   2. [Modificators](#2-modificators)
   3. [Tags](#3-tags)
   4. [Attributes](#4-attributes)
   5. [Attribute Set](#5-attribute-set)
   6. [Ability Controller](#6-ability-controller)
3. [Usage](#usage)
   1. [AttributeSet setup](#1-attributeset)
   2. [Effectors](#2-effectors)
   3. [Debugging](#3-debugging)
   4. [Abilities](#4-abilities)
   5. [Attribute Changes](#5-attribute-changes)
---

## Introduction

The Ability System is used to manage entities’ data and abilities.  
Its core concept is based on Unreal’s GAS (Gameplay Ability System). The Ability System serves as a foundation of the project's abilities, enabling other features to be managed through abilities while avoiding unnecessary dependencies and in the result, complexity.

---

## Installation

1. Donwload unity package
[Download AbilitySystem v1.0.3](https://raw.githubusercontent.com/Icelogia/AbilitySystem/main/AbilitySystem%20v1.0.3.unitypackage)

2. Import unity package to the project by either dragging unity package file to the unity window or by importing file.

## Concepts

### 1. Effectors

Effectors serve as data holders of the applied effect. They pass data as:

- **Timing** - defines when and for how long effect’s modificators will be applied applied.  
  Timing types:
  - **Instantly** - modificators are applied immediately with the effector,
  - **Delayed** - modificators are applied after the delay defined in the Delay property,
  - **Period** - modificators are applied for a defined number of Ticks. Duration is determined by the number of Ticks and the time period between them, defined in the PeriodBetweenTicks property.
- **Tag** - flag that can be used to define custom functionality. By checking Stackable property, tags count will increase and decrease for AttributeSet.
- **Modificators** - a list of modifications that will be applied to the entity.

---

### 2. Modificators

Modifiers are data classes that define calculations performed on targeted attributes.

For simple arithmetic operations such as addition or multiplication, the system provides AttributeOperationModifier.

The AttributeSet passed into Modifier.ApplyModification() must be the one assigned to the ability holder/caster. This allows calculations to depend on the caster’s parameters. For example:
- Damage based on a percentage of the caster’s health
- Effects scaling with strength or other attributes

---

### 3. Tags

Tags serve as flags for custom functionality defined by the developer. They can:
- Represent states such as “Stunned”
- Classify effects for special interactions (e.g., healing dealing damage to undead)

Tags add flexibility for implementing conditional or character-specific behaviors.

---

### 4. Attributes

Attributes represent individual entity parameters such as health, speed, etc.  
Each attribute is defined by:
- Value
- Getter
- Setter
- Change event callback

The double inheritance structure (Attribute<T> : Attribute) exists because generic classes cannot be stored in a unified collection. The non-generic Attribute base class enables this consistency.

Supported attribute types are:
- IntAttribute
- FloatAttribute
- Vector2Attribute

Types are intentionally kept simple to avoid increasing complexity in modifications.  
Vector2Attribute should be used only when specifically required.

---

### 5. Attribute Set

The AttributeSet is the main container for all entity data—attributes and tags. It is divided into four partial classes:

- **MonoBehaviour** - Component representation managing the cancellation token used for timing behavior driven by Effectors.
- **Attributes** - Contains all base attributes, including a dictionary for simplified access by Effectors.
- **Tags** - Stores tags with stack counts and provides tag-management functionality.
- **Modifications** - Manages timing behavior for effectors and invokes modifiers.

---

## 6. Ability Controller

Base class of all future abilities. Handles cooldown and presents common methods that will be used in other scripts as Aim, Cast, CanCast and SetAbilityHolder.

## Usage

### 1. AttributeSet

1. Add new attribute type
First of all, if we want to create a new attribute, we need to add it to AttributeType enumeration. It is required for easier mapping of attributes via Attributes dictionary.
For example we will add Speed attribute:
```cs
public enum AttributeType
{
    ...,
    Speed
}
```
2. Create attribute object
Now when we define a new type, we can go to AttributeSet.Attributes to add our new attribute.

To add it we need to do only two steps: Create attributes object and Add it to Attributes dictionary.

We will define speed as a float attribute to make it easier to use with rigidbody components, however, if you need, you can define it as int or vector2 attribute too depending on your needs.
```cs
public partial class AttributeSet
{
   ...
   [Space]
   [Header("Attributes")]
   public FloatAttribute Speed;

   ...

   protected virtual void InitAttributes()
   {
      ...
      Attributes.Add(AttributeType.Speed, Speed);
      ...
   }
}
```

And as mentioned earlier, we need to add attributes to the dictionary. It makes mapping attributes easier in the case when you want to make access more dynamic and change values only by defined type in the inspector. The example of this you can find in AttributeOperationModificator.

```cs
public class AttributeOperationModificator : Modificator
{
   ...
   [SerializeField] private AttributeType attributeType;
   ...

   public override void ApplyModification(AttributeSet attributeSet, AttributeSet owner)
   {
      Attribute attribute = attributeSet.Attributes[attributeType];
      ...
   }
}
```

3. Add AttributeSet component to the GameObject
ter adding component you can see its properties.
There are two ways of setting initial attributes data. You can either set their properties values or set them via effector during initialization. Init Effector takes priority and will override targeted attributes during runtime.

More about effectors and debugging sections in [Effectors](#2-effectors) and [Debugging](#3-debugging).

---

### 2. Effectors
1. Create modificator
Before we jump to effectors, let’s start with modificators. As mentioned, effectors are only holding information about modifications while modificators are the ones that modifies attributes.
- Add/Multiply
This modificator has been already implemented due to its simplicity. 
_Right click in Project window -> Create -> Ability System -> Modificators -> Attribute Operation_

This will create our first modificator. There are two operations: Add and Multiply. Subtraction and Division is not present due to being an element of Addition and Multiplication.

Now you can select the operation you need, select the attribute you want to target and write modification value.

Modifier Value is a float field but for integer attributes, the value will be rounded. For Vector2 a separate modifier property was created.

**This is only a preference setup.** You are welcome to change it or separate modificators for specific attributes.

![Add Modificator](https://github.com/Icelogia/AbilitySystem/blob/main/Assets/Docs/Images/AS_add_modificator.png)

- Custom Modificator
To make more mathematical changes or based on other statistics you can create a Custom Modificator. To present an example, there was HealthBasedDamageModificator created in samples:

```cs
[CreateAssetMenu(fileName = "Health Based Damage Modificator", menuName = "Ability System/Modificators/Health Based Damage")]
public class HealthBasedDamageModificator : Modificator
{
    [SerializeField] private int baseDamage = 1;
    [Range(0f, 1f), SerializeField] private float ownerMaxHealthPrecentage;
    [Range(0f, 1f), SerializeField] private float targetMaxHealthPrecentage;

    public override void ApplyModification(AttributeSet attributeSet, AttributeSet owner)
    {
        var currentHealth = attributeSet.Health.Get();
        int damage = MathExtensions.Round(
            baseDamage 
            + ownerMaxHealthPrecentage * attributeSet.MaxHealth.Get() 
            + targetMaxHealthPrecentage * owner.MaxHealth.Get());

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, currentHealth);
        attributeSet.Health.Set(currentHealth);
    }
}
```

You can see here how you can retrieve attributes and change it based on effector’s owner and targets’ attributes.

![Custom Modificator](https://github.com/Icelogia/AbilitySystem/blob/main/Assets/Docs/Images/AS_projectile_modificator.png)

2. Create effector
Now we can create our effector. 
_Right click in Project window -> Create -> Ability System -> Effector_

![Projectile Effector](https://github.com/Icelogia/AbilitySystem/blob/main/Assets/Docs/Images/AS_projectile_effector.png)

3. Applying modifications to AttributeSet
Now we just need to apply an effector to AttributeSet. We can see an example in SimpleProjectile from a sample. Projectile detected AttributeSet and applied held effector. Additionally it passed the owner AttributeSet for custom calculations. Note that you will need to pass this object in some way.

```cs
public class SimpleProjectile : MonoBehaviour
{
      ...
      private void OnTriggerEnter(Collider other)
      {
         var attributeSet = other.GetComponent<AttributeSet>();

         if (attributeSet != null)
            attributeSet.Apply(data.Effector, owner);
            ...
      }
      ...
}
```

---

### 3. Debugging
AttributeSet contains a Debugging tab with 
- ActiveEffectors - list of currently active effectors. This list is specifically to track effectors that have timing set to different values than instant.
- EffectorsHistory - list of 10 recent effectors applied to the AttributeSet. Threshold is defined inside AttributeSet.Modifications with historyThreshold const value.

![AttributeSet Debug](https://github.com/Icelogia/AbilitySystem/blob/main/Assets/Docs/Images/AS_debugging.png)

---

### 4. Abilities
1. Create new ability
To create a new ability firstly you need to create a class inheriting from AbilityController. In the sample there is an ACSimpleProjectile. Main part of ability are Aim, Cast and Update methods where you define abilities behaviour as you please.

For example ACSimpleProjectile:
- Aim - creates indicator
- Cast - recycles indicator and creates projectile
- Update - controls indicator direction and updates cooldown of the ability after it has been casted

```cs
public class ACSimpleProjectile : AbilityController
{
    [SerializeField] private DirectionalIndicator indicatorPrefab;
    [SerializeField] private SimpleProjectile projectilePrefab;

    private Vector3 aimDirection;
    private DirectionalIndicator indicator;

    public override void Aim()
    {
        base.Aim();
        indicator = indicatorPrefab.gameObject.Spawn<DirectionalIndicator>(transform.position);
    }

    public override void Cast()
    {
        base.Cast();

        indicator.Recycle();
        indicator = null;

        var projectile = projectilePrefab.gameObject.Spawn<SimpleProjectile>(transform.position);
        projectile.SetDirection(aimDirection);
        projectile.SetOwner(holder.GetAttributeSet());
    }

    protected override void Update()
    {
        base.Update();

        if(holder != null)
            aimDirection = (holder.GetTargetPosition() - transform.position).normalized;

        if (indicator != null)
            indicator.SetDirection(aimDirection);
    }
}
```

2. Ability Holder
However, before going into play mode and having fun with your abilities there is one more recommendation. It is good  to set the IAbilityHolder holder with the SetAbilityHolder method. You can do this in any script or controller. Holder is a context object that allows retrieval of data by ability to affect in some way the ability holder. For example you can use it for animation or to apply custom modificators.

---

### 5. Attribute Changes
To track attribute's value change you can either use Get method or OnAttributeChanged event. To do this you need to directly retrive attribute you want to target and apply read functionality. In samples it has been done for target's health bar in HUD.

```cs
public class TargetController : MonoBehaviour
{
    [SerializeField] private AttributeSet attributeSet;

    private void OnEnable()
    {
        attributeSet.Health.OnAttributeChanged += SetHPBar;
    }

    private void OnDisable()
    {
        attributeSet.Health.OnAttributeChanged -= SetHPBar;
    }

    private void SetHPBar(int oldHp, int newHp)
    {
        float normalizedHP = (float)newHp / attributeSet.MaxHealth.Get();
        HUDManager.Instance.TargetHPBar.SetSize(normalizedHP);
    }
```
