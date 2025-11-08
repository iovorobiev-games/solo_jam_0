using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using DG.Tweening;
using Game;
using Game.data;
using Game.ui;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class HintUI : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text dweller;
    public TMP_Text damage;
    public TMP_Text respawn;
    public TMP_Text price;
    public TMP_Text ability;

    private void Awake()
    {
        DI.sceneScope.register(this);
        gameObject.SetActive(false);
    }

    public async UniTask showHint(RoomVM room, Vector2 coords)
    {
        gameObject.SetActive(true);
        name.text = room.Room.name;
        dweller.text = room.Room.dweller;
        damage.text = room.getDamage() + RTHelper.SWORD;
        respawn.text = room.getCooldown() + RTHelper.RESP;
        price.text = room.Room.price + RTHelper.TIME;
        ability.text = room.Room.Skill.Description;
        transform.position = coords;
        await transform.DOScale(Vector3.one, 0.25f).From(0).ToUniTask();
        
    }

    public async UniTask hideHint()
    {
        transform.DOKill();
        gameObject.SetActive(false);
    }
}
