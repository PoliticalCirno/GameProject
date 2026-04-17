using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private float projectileForce = 0f;
    private InputAction fire;

    private void Awake()
    {
        fire = InputSystem.actions.FindAction("Player/Attack");
    }
    private void OnEnable()
    {
        fire.started += ShootPooledBullet;
    }
    private void OnDisable()
    {
        fire.started -= ShootPooledBullet;
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        GameObject projectile = GameObject.Instantiate(bullet, projectileSpawn.position, projectileSpawn.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * projectileForce, ForceMode.Impulse);
        Destroy(projectile, 1.5f);
    }
    private void ShootPooledBullet(InputAction.CallbackContext context)
    {
        Bullet bullet = BulletObjectPool.Instance.Get();
        bullet.transform.SetPositionAndRotation(projectileSpawn.position, projectileSpawn.rotation);
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileForce, ForceMode.Impulse);
    }
}
