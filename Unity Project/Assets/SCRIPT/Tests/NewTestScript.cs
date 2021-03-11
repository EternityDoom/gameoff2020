using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BandAid;

namespace Tests
{
    public class MoonKindTests
    {
        private BuildingSpot bs;
        // A Test behaves as an ordinary method
        [Test]
        public void NewTestScriptSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        [Test]
        public void TestConnectionGetOther()
        {
            Connection c = new Connection();
            BuildingSpot s = new BuildingSpot();
            c.spotStart = new BuildingSpot();
            c.spotEnd = new BuildingSpot();

            BuildingSpot bs = c.GetOther(s);
            Assert.AssertEquals(bs, c.spotStart);
        }

        [Test]
        public void TestConnectionGetOtherEnd()
        {
            Connection c = new Connection();
            BuildingSpot s = new BuildingSpot();
            c.spotStart = s;
            c.spotEnd = new BuildingSpot();

            BuildingSpot bs = c.GetOther(s);
            Assert.AssertEquals(bs, c.spotEnd);
        }

        [Test]
        public void CityManagerHousingSpace()
        {
            CityManager cm = new CityManager();
            cm.buildings = new List<BuildingSpot>() { bs };
            int expected = 1;
            int res = cm.HousingSpace();
            Assert.AssertEquals(expected, res);
        }

        [Test]
        public void CityManagerCulture()
        {
            CityManager cm = new CityManager();
            cm.buildings = new List<BuildingSpot>() { bs };
            int expected = 3;
            int res = cm.Culture();
            Assert.AssertEquals(expected, res);
        }

        [Test]
        public void CityManagerHousing()
        {
            CityManager cm = new CityManager();
            cm.buildings = new List<BuildingSpot>() { bs };
            int expected = 1;
            int res = cm.Housing();
            Assert.AssertEquals(expected, res);
        }

        [Test]
        public void CityManagerWorkplace()
        {
            CityManager cm = new CityManager();
            cm.buildings = new List<BuildingSpot>() { bs };
            cm.buildings[0].currentBuilding.housing = false;
            cm.buildings[0].currentBuilding.research = false;
            cm.buildings[0].producing = true;
            cm.buildings[0].maintainence = false;
            cm.buildings[0].research = false;
            cm.buildings[0].currentProject = null;
            int expected = 1;
            int res = cm.Workplace();
            Assert.AssertEquals(expected, res);
        }

        [Test]
        public void CityManagerStorage()
        {
            CityManager cm = new CityManager();
            cm.buildings = new List<BuildingSpot>() { bs };
            cm.buildings[0].currentBuilding.housing = false;
            int expected = 4;
            int res = cm.Storage(2);
            Assert.AssertEquals(expected, res);
        }

        [Test]
        public void BuildingSpotResourcePortionThis()
        {
            float expected = 4.0;
            int res = bs.ResourcePortionThis();
            Assert.AssertEquals(expected, res);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }

        [SetUp]
        public void ResetScene()
        {
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        }

        [SetUp]
        public void BuildingSpotCreate()
        {
            bs = new BuildingSpot();
            bs.district = "";
            bs.discovered = true;
            bs.mode = BuildingSpotMode.Building;
            bs.status = BuildingStatus.Operating;
            bs.currentBuilding = makeBuildingObject();
            bs.connections = null;

            bs.built = true;
            bs.storage = 4;
        }

        private BuildingObject makeBuildingObject()
        {
            BuildingObject bo = new BuildingObject();
            bo.buildingName = "";
            bo.description = "";
            bo.color = Color.Blue;
            bo.sprite = null;
            bo.prefab = new GameObject();
            bo.populationRequirement = 1;
            bo.populationName = "";
            bo.resourceType = 2;
            bo.projects = null;
            bo.production = null;
            bo.constructionMonthlyCost = null;
            bo.storageIncreaseMonthlyCost = null;
            bo.constructionTime = 0;
            bo.control = false;
            bo.culture = 1;
            bo.baseStorage = 2.3;
            bo.decay = 0.2;
            bo.decayEffect = "stop";
            bo.maintainenceEffect = "stop";
            bo.costInfo = "";
            bo.shortageEffect = "";
            bo.constructionSound = null;

            return bo;
        }
    }
}
