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
            UtilityFunctions.createFreeplayTeam(ref driver);
            int element_chosen = UtilityFunctions.getRandomNumber(0, driver.FindElements(By.ClassName("team-summary")).Count);
            int number_of_ships_before = int.Parse(driver.FindElements(By.ClassName("team-summary"))[element_chosen].FindElement(By.Id("Test Team "+(element_chosen+1)+"-size")).Text);
            int current_roster = 0;
            int number_to_delete_space = 0;
            int forward_or_backwards = 0;
            int roster_of_the_deleted = 0;
            int base_roster = 0;
            int team_size_after_deletion = 0;
            IWebElement next_previous_button = null;

            //Create a multi ship team.
            for(int i=0; i < UtilityFunctions.getRandomNumber(1,4);i++)
            {
                UtilityFunctions.addShipToExistingTeam(ref driver, "Test Team 1");
            }

            driver.FindElements(By.ClassName("team-summary"))[element_chosen].Click();
            driver.FindElement(By.Id("remove-button")).Click();

            current_roster = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1));
            while (!list_of_rosters.Contains(current_roster))
            {
                list_of_rosters.Add(current_roster);
                driver.FindElement(By.Id("next-button")).Click();
                current_roster = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1));
            }
            number_to_delete_space = UtilityFunctions.getRandomNumber(0, (list_of_rosters.Count * 5));
            forward_or_backwards = UtilityFunctions.getRandomNumber(1, 2);
            if(forward_or_backwards == 1)
            {
                next_previous_button = driver.FindElement(By.Id("next-button"));
            }
            else if(forward_or_backwards == 2)
            {
                next_previous_button = driver.FindElement(By.Id("prev-button"));
            }
            else
            {
                Assert.Fail("Could not determine which button to press.");
            }
            for(int i=0; i < number_to_delete_space;i++)
            {
                next_previous_button.Click();
            }

            roster_of_the_deleted = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1));
            driver.FindElement(By.Id("upgrade-button")).Click();
            Assert.IsTrue(UtilityFunctions.CheckIfAlertExists(ref driver));
            driver.SwitchTo().Alert().Accept();
            current_roster = -1;
            base_roster = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1)); ;
            while(current_roster != base_roster)
            {
                team_size_after_deletion++;
                if(current_roster == roster_of_the_deleted || base_roster == roster_of_the_deleted)
                {
                    Assert.Fail("Roster number of the deleted detected!");
                }
                driver.FindElement(By.Id("next-button")).Click();
                current_roster = int.Parse(driver.FindElement(By.Id("roster-number-stat")).Text.Remove(0, 1));
            }
            //Confirm that the roster number of the removed is not present.

            Assert.IsTrue((team_size_after_deletion+1) == list_of_rosters.Count);
            driver.FindElement(By.Id("back-button")).Click();
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
