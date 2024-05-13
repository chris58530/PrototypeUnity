using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5f; // 爆炸半徑
    public float force = 700f; // 推力

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); // 在爆炸半徑內找到所有碰撞器

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>(); // 如果物體有剛體，就給它一個推力
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, 1f, ForceMode.Impulse);
            }
        }
    }

    void Start()
    {
        Explode(); // 在腳本啟動時爆炸
    }
}
