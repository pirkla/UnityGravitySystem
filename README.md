# UnityGravitySystem

A simple gravity system for Unity that allows objects to change their rotation to match surfaces they collide with. So think running into a wall and automatically orienting the new wall to down.

It also includes functionality for localized slowing of gravity. The slowing mechanism is event driven, so it would be easy to create additional modules to use the same mechanism when they enter the zone of influence and subscribe to the event.

Setup: Add all the files to the project. Add the ObjectGravity script to an object with a collider and rigidbody. Turn off gravity for the rigidbody. Set the gravity origin to 0,0,0 and the gravity direction to 0,1,0 (or to whatever you want it to be)
Add the tag "Gravitates" and set the tag for any walls that gravity should reorient to to "Gravitates"
The tag could be customized via the serializable field "Grav Object Tag Name" on the ObjectGravity script on the obect.
Note the walls will need a collider for this to work.

Then just add a motive force to the object. When it collides with another object with a collider and the "Gravitates" tag it will automatically reorient to the normal of the collision point. It largely behaves like regular gravity, so it should integrate with other Unity systems fine.



The script does this by checking on collision enter to find objects it collides with, then set the gravity direction based on the normal. It then applies the rotation and gravity during the fixed update.
