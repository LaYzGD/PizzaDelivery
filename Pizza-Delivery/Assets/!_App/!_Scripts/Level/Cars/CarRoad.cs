using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CarRoad : MonoBehaviour
{
    [SerializeField] private Car _carPrefab;
    [SerializeField] private float _carsMovementSpeed;
    [SerializeField] private Transform[] _carsSpawnPoints;
    [SerializeField] private float _spawnDelayMaxTime;
    [SerializeField] private float _spawnDelayMinTime;
    [SerializeField] private Transform _carHolder;

    private float _currentSpawnDelay;

    private ObjectPool<Car> _carPool;

    private void Awake()
    {
        _carPool = new ObjectPool<Car>(OnCreatePrefab, OnGetObject, OnReleaseObject);
    }

    private void OnEnable()
    {
        StartCoroutine(CarSpawnHandler());
    }

    private void OnDisable()
    {
        StopCoroutine(CarSpawnHandler());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            _carPool.Release(car);
        }
    }

    private Car OnCreatePrefab()
    {
        return Instantiate(_carPrefab);
    }

    private void OnGetObject(Car car)
    {
        car.gameObject.SetActive(true);
    }

    private void OnReleaseObject(Car car)
    {
        car.gameObject.SetActive(false);
    }

    private void InitializeCar()
    {
        var car = _carPool.Get();
        car.transform.SetParent(_carHolder);
        var randomPoint = UnityEngine.Random.Range(0, _carsSpawnPoints.Length);
        car.transform.position = _carsSpawnPoints[randomPoint].transform.position;
        var direction = randomPoint == 0 ? Vector3.right : Vector3.left;
        car.SetValues(direction, _carsMovementSpeed);
    }

    private IEnumerator CarSpawnHandler()
    {
        while (true)
        {
            _currentSpawnDelay = UnityEngine.Random.Range(_spawnDelayMinTime, _spawnDelayMaxTime);
            InitializeCar();
            yield return new WaitForSecondsRealtime(_currentSpawnDelay);
        }
    }
}
