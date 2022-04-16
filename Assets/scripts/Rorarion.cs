using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{

    [SerializeField] private MovementCharacteristics characteristics;
    public GameObject player;
    private float vertical, horizontal;
    private readonly string STR_VERTICAL = "Vertical";
    private readonly string STR_HORIZONTAL = "Horizontal";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis(STR_HORIZONTAL);
        vertical = Input.GetAxis(STR_VERTICAL);
        transform.rotation = Quaternion.Euler(player.transform.rotation.y * 5, 0, player.transform.rotation.x *5);
    }
}
