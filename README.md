
# UnityHelpers
Some scripts that I've written during Unity dev that help a bit with productivity.

# Find Missing Scripts Recursively

Looks through open scenes or a selected gameObject and finds any missing script components. Allowing you to quickly remove the annoying associated warning.
Originally authored by: https://gigadrillgames.com/2020/03/19/finding-missing-scripts-in-unity/

# Label Icon Matcher

Hieracrchy item colour matches label icon for any prefab. 

# Animation Object Property Remover

Removes an animated property from all clips in an animator.

Useful when you've refactored your animation hierarchy and want to remove dead references.

Given an animator, path to specific object and property value this tool walks all curves (keyframes) in all clips within an animator and removes the specificed object's property by removing all associated keyframes.

NOTE: Please back up your work before running this.

# Edge Joiner

Simple script that allows you to select two 2D edge colliders and have their last and first points joined together. The point from the edge to the right is used as the snap reference point.

## Adding to Unity

1, Download EdgeJoiner.cs 

2, In Unity under Assets / Editor, add EdgeJoiner.cs

## Using

1, Select Window > Edge Joiner

2, Select two GameObjects containing 2D edge colliders you'd like to join

3, Press 'Join'

