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
        
        [OneTimeSetUp]
        public void Setup()
        {
            UtilityFunctions.freeplaySetup(ref driver);
        }

        [Test]
        //Happy Path Test
        //Create a single team with a single ship.
        public void CreateNewTeam()
        {
            //Main team screen
            IWebElement okTeamCreationButton;
            IWebElement newTeamButton = driver.FindElement(By.Id("new-team-button"));
            IWebElement teamNameTextField;
            int test_at_beginning = driver.FindElements(By.ClassName("team-summary")).Count;
            newTeamButton.Click();
            okTeamCreationButton = driver.FindElement(By.Id("ok-button"));
            teamNameTextField = driver.FindElement(By.Id("team-name-input"));
            teamNameTextField.SendKeys("Test Team " + UtilityFunctions.freeplayTeamNumber);
            okTeamCreationButton.Click();

            //Ship selection screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("New Team Ship Selection", driver.Url));
            
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
            Assert.IsTrue(UtilityFunctions.assertPageValidation("New Team Pilot Selection", driver.Url));
            IWebElement direction_button = null;
            bool pilot_selected = false;
            while(pilot_selected == false)
            {
                element_counter = UtilityFunctions.getRandomNumber(1, 2);
                if (element_counter == 1)//Go forward
                {
                    direction_button = driver.FindElement(By.Id("next-btn"));
                }
                else if (element_counter == 2)//Go backward
                {
                    direction_button = driver.FindElement(By.Id("previous-btn"));
                }
                else
                {
                    Assert.Fail("Pilot Selection screen failed. Random number to determine foward and backward motion returned out of bounds.");
                }
                element_counter = UtilityFunctions.getRandomNumber(0, 50);
                for(int i=0; i < element_counter;i++)
                {
                    direction_button.Click();
                }
                if(!driver.FindElement(By.Id("unavailable")).Displayed)
                {
                    pilot_selected = true;
                }
            }
            driver.FindElement(By.Id("select-button")).Click();

            //Upgrade selection screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("New Team Upgrade Selection", driver.Url));
            int numberOfUpgrades = UtilityFunctions.getRandomNumber(0,12);
            for(int i=0; i < numberOfUpgrades;i++)
            {
                driver.FindElement(By.Id("next-selection")).Click();

                //Upgrade type selection screen
                Assert.IsTrue(UtilityFunctions.assertPageValidation("New Team Upgrade Type Selection", driver.Url));
                IList<IWebElement> upgrade_types = driver.FindElements(By.ClassName("type-clicker"));
                upgrade_types[UtilityFunctions.getRandomNumber(0, upgrade_types.Count)].Click();

                //Upgrade Options screen
                Assert.IsTrue(UtilityFunctions.assertPageValidation("New Team Upgrade Options", driver.Url));
                IList<IWebElement> upgrades = driver.FindElements(By.ClassName("upgrade"));
                upgrades[UtilityFunctions.getRandomNumber(0, upgrades.Count)].Click();
            }
            driver.FindElement(By.Id("done-button")).Click();
            int roster_number = UtilityFunctions.getRandomNumber(1, 999);
            while(UtilityFunctions.rosterNumbersInUse.Contains(roster_number))
            {
                roster_number = UtilityFunctions.getRandomNumber(1, 999);
            }
            UtilityFunctions.rosterNumbersInUse.Add(roster_number);
            driver.FindElement(By.Id("roster-number-input")).SendKeys(roster_number.ToString());
            driver.FindElement(By.Id("ok-button")).Click();

            //Back to team screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("Team Page", driver.Url));
            Assert.IsTrue(driver.FindElements(By.ClassName("team-summary")).Count == (test_at_beginning+1));
            UtilityFunctions.freeplayTeamNumber = driver.FindElements(By.ClassName("team-summary")).Count + 1;
        }

        [Test]
        //Happy Path Test
        //Add a single ship to an already existing team.
        public void addNewShipToExistingTeam()
        {
            UtilityFunctions.createFreeplayTeam(ref driver);
            int team_count = driver.FindElements(By.ClassName("team-summary")).Count;
            driver.FindElement(By.Id("Test Team 1")).Click();
            driver.FindElement(By.Id("add-button")).Click();

            //Ship selection screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("Add Ship Ship Selection",driver.Url));

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
            Assert.IsTrue(UtilityFunctions.assertPageValidation("Add Ship Pilot Selection", driver.Url));
            IWebElement direction_button = null;
            bool pilot_selected = false;
            while (pilot_selected == false)
            {
                element_counter = UtilityFunctions.getRandomNumber(1, 2);
                if (element_counter == 1)//Go forward
                {
                    direction_button = driver.FindElement(By.Id("next-btn"));
                }
                else if (element_counter == 2)//Go backward
                {
                    direction_button = driver.FindElement(By.Id("previous-btn"));
                }
                else
                {
                    Assert.Fail("Pilot Selection screen failed. Random number to determine foward and backward motion returned out of bounds.");
                }
                element_counter = UtilityFunctions.getRandomNumber(0, 50);
                for (int i = 0; i < element_counter; i++)
                {
                    direction_button.Click();
                }
                if (!driver.FindElement(By.Id("unavailable")).Displayed)
                {
                    pilot_selected = true;
                }
            }
            driver.FindElement(By.Id("select-button")).Click();

            //Upgrade selection screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("Add Ship Upgrade Selection", driver.Url));
            int numberOfUpgrades = UtilityFunctions.getRandomNumber(0, 12);
            for (int i = 0; i < numberOfUpgrades; i++)
            {
                driver.FindElement(By.Id("next-selection")).Click();

                //Upgrade type selection screen
                Assert.IsTrue(UtilityFunctions.assertPageValidation("Add Ship Upgrade Type Selection", driver.Url));
                IList<IWebElement> upgrade_types = driver.FindElements(By.ClassName("type-clicker"));
                upgrade_types[UtilityFunctions.getRandomNumber(0, upgrade_types.Count)].Click();

                //Upgrade Options screen
                Assert.IsTrue(UtilityFunctions.assertPageValidation("Add Ship Upgrade Otions Selection", driver.Url));
                IList<IWebElement> upgrades = driver.FindElements(By.ClassName("upgrade"));
                upgrades[UtilityFunctions.getRandomNumber(0, upgrades.Count)].Click();
            }
            driver.FindElement(By.Id("done-button")).Click();
            int roster_number = UtilityFunctions.getRandomNumber(1, 999);
            while (UtilityFunctions.rosterNumbersInUse.Contains(roster_number))
            {
                roster_number = UtilityFunctions.getRandomNumber(1, 999);
            }
            UtilityFunctions.rosterNumbersInUse.Add(roster_number);
            driver.FindElement(By.Id("roster-number-input")).SendKeys(roster_number.ToString());
            driver.FindElement(By.Id("ok-button")).Click();

            //Back to team screen
            Assert.IsTrue(UtilityFunctions.assertPageValidation("Team Page", driver.Url));
            Assert.IsTrue(driver.FindElements(By.ClassName("team-summary")).Count == team_count);
            UtilityFunctions.freeplayTeamNumber = driver.FindElements(By.ClassName("team-summary")).Count + 1;
        }

    }
}
