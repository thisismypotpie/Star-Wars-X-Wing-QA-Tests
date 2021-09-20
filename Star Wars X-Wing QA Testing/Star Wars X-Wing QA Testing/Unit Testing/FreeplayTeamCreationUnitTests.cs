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
    class FreeplayTeamCreationUnitTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            UtilityFunctions.freeplaySetup(ref driver);
        }

        /*********************************************************TEAM SCREEN****************************************************************/

        [Test]
        //Boundry Test
        //This test is to confirm that when a player attempts to start a game wtih no teams that an error message shows up.
        public void ConfirmStartGameWarningNoTeams()
        {
            bool does_alert_exist = false;
            IWebElement startGameButton = driver.FindElement(By.Id("start-game-button"));
            startGameButton.Click();
            does_alert_exist = UtilityFunctions.CheckIfAlertExists(ref driver);
            if(does_alert_exist == false)
            {
                Assert.Fail("No alert message was present");
            }
            else if(driver.SwitchTo().Alert().Text != "You must have at least one team created before starting the game.")
            {
                Assert.Fail("The incorrect alert was displayed.");
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                driver.SwitchTo().Alert().Accept();
            }
        }

        [Test]
        //Boundry Test
        //Test if putting no name in the team prompt will result in the correct error.
        public void ConfirmNoTeamNameEntry()
        {
            IWebElement okTeamCreationButton;
            IWebElement newTeamButton = driver.FindElement(By.Id("new-team-button"));
            IWebElement closeTeamButton;
            bool does_alert_exist = false;
            newTeamButton.Click();
            okTeamCreationButton = driver.FindElement(By.Id("ok-button"));
            okTeamCreationButton.Click();
            does_alert_exist = UtilityFunctions.CheckIfAlertExists(ref driver);
            if (does_alert_exist == false)
            {
                Assert.Fail("No alert message was present");
            }
            else if (driver.SwitchTo().Alert().Text != "team name cannot be only whitespace.")
            {
                Assert.Fail("The incorrect alert was displayed.");
            }
            else
            {
                System.Threading.Thread.Sleep(1000);
                driver.SwitchTo().Alert().Accept();
            }
            closeTeamButton = driver.FindElement(By.Id("close-team-name-button"));
            closeTeamButton.Click();
        }

        [Test]
        //Happy Path Test
        //Test if going from team screen, to main screen, back to team screen removes all teams.
        public void TeamRemovalMainScreen()
        {
            int number_of_teams = UtilityFunctions.getRandomNumber(1, 5);
            for(int i=0; i < number_of_teams;i++)
            {
                UtilityFunctions.createFreeplayTeam(ref driver);
            }
            driver.FindElement(By.Id("back-button")).Click();
            driver.FindElement(By.Id("new-game-button")).Click();
            driver.FindElement(By.Id("freeplay-button")).Click();
            Assert.IsTrue(driver.FindElements(By.ClassName("team-summary")).Count ==0);
        }

        [Test]
        //Happy Path Test
        //Test to confirm that removing a single ship from an existing team with more than one ship.
        public void SingleShipRemovalFromMultiShipTeam()
        {
            List<int> list_of_rosters = new List<int>();
            UtilityFunctions.SetupFreeplayGame(ref driver);
            int element_chosen = UtilityFunctions.getRandomNumber(0, driver.FindElements(By.ClassName("team-summary")).Count);
            int number_of_ships_before = int.Parse(driver.FindElements(By.ClassName("team-summary"))[element_chosen].FindElement(By.Id("Test Team "+(element_chosen+1)+"-size")).Text);
            int current_roster = 0;

            driver.FindElements(By.ClassName("team-summary"))[element_chosen].Click();
            driver.FindElement(By.Id("remove-button")).Click();

            while(!list_of_rosters.Contains(current_roster))
            {
                current_roster = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1));
                list_of_rosters.Add(current_roster);
                driver.FindElement(By.Id("next-button")).Click();
            }

        }

        [Test]
        //Happy Path Test
        //Test to confirm that removing a single ship from a team with one ship will remove the team.
        public void SingleShipRemovalFromSingleShipTeam()
        {

        }

        /*********************************************************SHIP SELECTION SCREEN****************************************************************/


    }

}
