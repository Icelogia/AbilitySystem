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
---

## Introduction

The Ability System is used to manage entities’ data and abilities.  
Its core concept is based on Unreal’s GAS (Gameplay Ability System). The Ability System serves as a foundation of the project's abilities, enabling other features to be managed through abilities while avoiding unnecessary dependencies and in the result, complexity.

---

## Installation

**Package Manager Git URL:** https://github.com/Icelogia/AbilitySystem.git?path=Assets/AbilitySystem 

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


---

## Usage
