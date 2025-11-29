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
        _particles = GetComponent<ParticleSystem>();
        Close();
        _passable = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (_passable && other.CompareTag("Player"))
        {
            _nextRoomBuilder.ConstructRoom();
            Close();
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _particles.Play();
        _passable = true;
    }

    void Close()
    {
        _passable = false;
        _particles.Stop();
        gameObject.SetActive(false);
    }
}
