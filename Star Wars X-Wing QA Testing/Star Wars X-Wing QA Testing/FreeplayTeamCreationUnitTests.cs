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
        int freeplayTeamNumber = 1;

        [OneTimeSetUp]
        public void Setup()
        {
            UtilityFunctions.freeplaySetup(ref driver);
        }

        /*********************************************************MAIN TEAM SCREEN****************************************************************/

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

        /*********************************************************SHIP SELECTION SCREEN****************************************************************/


    }

}
