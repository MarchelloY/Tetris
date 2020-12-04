using System;
using System.Collections;
using Mediators;
using Models;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayModeTests
    {
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        
        [UnityTest]
        [TestCase(1, "000000100", ExpectedResult = null)]
        [TestCase(2, "000000300", ExpectedResult = null)]
        [TestCase(3, "000000700", ExpectedResult = null)]
        [TestCase(4, "000001500", ExpectedResult = null)]
        public IEnumerator ScoreEqualsWithUITest(int lines, string result)
        {
            yield return null;
            var uiMediator = GameObject.Find("RootGame").GetComponent<UIMediator>();
            var scoreModel = new ScoreModel {MainUIUpdatedSignal = uiMediator.MainUIUpdatedSignal};
            scoreModel.TestScore(lines);

            Assert.AreEqual(GameObject.Find("Score").GetComponent<TMP_Text>().text, result);
        }
        
        [UnityTest]
        [TestCase(1, "1", ExpectedResult = null)]
        [TestCase(2, "2", ExpectedResult = null)]
        [TestCase(3, "3", ExpectedResult = null)]
        [TestCase(4, "4", ExpectedResult = null)]
        public IEnumerator LinesEqualsWithUITest(int lines, string result)
        {
            yield return null;
            var uiMediator = GameObject.Find("RootGame").GetComponent<UIMediator>();
            var scoreModel = new ScoreModel {MainUIUpdatedSignal = uiMediator.MainUIUpdatedSignal};
            scoreModel.TestScore(lines);

            Assert.AreEqual(GameObject.Find("Lines").GetComponent<TMP_Text>().text, result);
        }
    }
}
