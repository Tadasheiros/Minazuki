using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public EnemyController enemy;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy = other.GetComponent<EnemyController>();
        enemy.hit = true;
    }
}
