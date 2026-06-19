using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    public void SpawnProjectile(Transform target)
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        newProjectile.GetComponent<Projectile>().Target = target;
    }
}
