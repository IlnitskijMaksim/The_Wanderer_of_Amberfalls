using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

[TestFixture]
public class MelleEnemyTest
{
    private GameObject testGameObject;
    private test testScript;

    [SetUp]
    public void SetUp()
    {
        // Create a test GameObject and add the test script to it
        testGameObject = new GameObject();
        testScript = testGameObject.AddComponent<test>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up the test GameObject
        Object.Destroy(testGameObject);
    }

    [Test]
    public void RandomNavMeshLocation_ReturnsValidPosition()
    {
        // Call the method you want to test
        Vector3 randomPosition = testScript.RandomNavMeshLocation();

        // Assert that the returned position is not the default Vector3.zero
        Assert.AreNotEqual(Vector3.zero, randomPosition);
    }

    // Add more tests as needed for other methods and behaviors in your test class
}
