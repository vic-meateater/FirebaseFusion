using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned Local player");
        }
        else
            Debug.Log("Spawned remote player");
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player.IsValid == Object.HasInputAuthority)
            Runner.Despawn(Object);
    }
}