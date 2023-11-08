using UnityEngine;




public class Properties : MonoBehaviour
{

    [SerializeField]
    private Item _ressourceType;
    
    [SerializeField]
    private int _maxRessource = 100;
    [SerializeField]
    private int _currentRessource = 100;


    public int _maxVillagers = 4;

    public int _currentVillagers = 0;

    public string Action;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void AssingVillager()
    {
        _currentVillagers++;
    }

    internal void RemoveVillager()
    {
        _currentVillagers--;
    }

    public void RemoveRessource(int amount)
    {
        _currentRessource -= amount;
        if (_currentRessource < 0)
        {
            _currentRessource = 0;
            Destroy(gameObject);
        }
    }

    public int RessourceID()
    {
        return _ressourceType.id;
    }

}
