using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SaveData/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField] private Material[] materials;
    [SerializeField] private GameObject[] playerObjects;
    [SerializeField] private Mesh[] meshes;
    [SerializeField] private Material playerObjectMaterial;

    public Material[] Materials => materials;
    public GameObject[] PlayerObjects => playerObjects;

    public Mesh[] Meshes => meshes;

    public Material PlayerObjectMaterial => playerObjectMaterial;
}
