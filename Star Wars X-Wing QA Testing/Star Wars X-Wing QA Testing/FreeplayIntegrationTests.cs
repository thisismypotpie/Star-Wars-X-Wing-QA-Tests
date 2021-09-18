using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Star_Wars_X_Wing_QA_Testing
{
    class FreeplayIntegrationTests
    {
        IWebDriver driver;
        int freeplayTeamNumber = 1;
        [OneTimeSetUp]
        public void Setup()
        {
            UtilityFunctions.freeplaySetup(ref driver);
        }

        [Test]
        //Happy Path Test
        //Create a single team with a single ship.
        public void CreateSingleTeamShip()
        {
            //Main team screen
            IWebElement okTeamCreationButton;
            IWebElement newTeamButton = driver.FindElement(By.Id("new-team-button"));
            IWebElement teamNameTextField;
            newTeamButton.Click();
            okTeamCreationButton = driver.FindElement(By.Id("ok-button"));
            teamNameTextField = driver.FindElement(By.Id("team-name-input"));
            teamNameTextField.SendKeys("Test Team " + freeplayTeamNumber);
            okTeamCreationButton.Click();

            //Ship selection screen
            Assert.IsTrue(driver.Url.Equals("file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Selection-Screen/Selection-Screen.html"));
            IList<IWebElement> factionList = driver.FindElements(By.ClassName("faction-option"));
            IList<IWebElement> shipSizeList;
            IList<IWebElement> shipList;
            int element_counter = UtilityFunctions.getRandomNumber(0, factionList.Count);
            factionList[element_counter].Click();
            shipSizeList = driver.FindElements(By.ClassName("ship-size-option"));
            element_counter = UtilityFunctions.getRandomNumber(0, shipSizeList.Count);
            shipSizeList[element_counter].Click();
            shipList = driver.FindElements(By.ClassName("ship-option"));
            element_counter = UtilityFunctions.getRandomNumber(0, shipList.Count);
            shipList[element_counter].Click();

            //Pilot selection screen
            Assert.IsTrue(driver.Url.Equals("file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Pilot-Screen/Pilot-Screen.html"));
        }
    }
}
