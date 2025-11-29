using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject nextRoom;

    private bool _passable = false;
    private RoomConstructionEffect _nextRoomBuilder;
    private ParticleSystem _particles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _nextRoomBuilder = nextRoom.GetComponent<RoomConstructionEffect>();
        Close();
        _passable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _passable)
        {
            _nextRoomBuilder.ConstructRoom();
            Close();
        }
    }

    void Open()
    {
        _passable = true;
        _particles.Play();
    }

    void Close()
    {
        _passable = false;
        // _particles.Stop();
    }
}
