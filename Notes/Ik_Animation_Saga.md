# Animation + IK + Mirror = Learning Opportunity

UPDATE: I found a solution, [see here](#Solution-Found)

In this screenshot there is a single player who is the Host of the game.

![pic](Images/Host_With_IK.png)

The raytraces seen were executed in:
- Update = Green
- FixedUpdate = Cyan
- Command = Red
- LateUpdate = Yellow

You'll notice LateUpdate is NOT in the direction that the gun is pointing visually.

---
What happens if we disable Inverse Kinematics? Below you'll see all raytraces match up with the mecanim aiming animation.

![pic](Images/Host_Without_IK.png)



---
Okay, but we want more than one player. What if we host a server and have a separate client connect to it, what does the server view look like?

![pic](Images/Server_View_____Separate_Client_Connected.png)

The left side of the picture is the client's view. The right side is the server in Unity where we can see the raytraces. We see only the Command ray trace, and unfortunately it is not pointing in the direction that the weapon is facing on the client OR the server.

---
Let's take a look at the client's raytraces

![pic](Images/Client_View_____Connected_To_Separate_Server.png)

Similar to the original screenshot (Single player as a Host), we see that the Yellow raytrace from LateUpdate does not match the Green (Update) and Cyan (FixedUpdate) traces. The yellow trace is pointing in the direction that the gun would face if inverse kinematics was turned off.

---
So what does all of this mean? I don't know yet. But I hope this helps if you're like me and running into problems getting your animated character to shoot at a target w/ IK locally & over the network.

## Useful Links
- https://docs.unity3d.com/Manual/ExecutionOrder.html
- https://mirror-networking.com/docs/Guides/Communications/RemoteActions.html

---

## SOLUTION FOUND
If you're using Final IK Aim IK like me, you can manually call for the IK solvers to update in LateUpdate to guarantee that everything matches up:
```csharp
private void LateUpdate()
{
    aimIK.solver.Update();
}
```

This seemingly simple line of code took an incredible amount of time to find. I've watched a dozen videos and read through I'd estimate 100 articles and forum posts to find the solution.

https://forum.unity.com/threads/final-ik-full-body-ik-aim-look-at-fabrik-ccd-ik-1-0-released.222685/page-20#post-1976210