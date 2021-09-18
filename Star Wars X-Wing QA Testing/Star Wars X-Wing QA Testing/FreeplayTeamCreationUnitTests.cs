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
    class TeamCreationUnitTests
    {
        IWebDriver driver;
        int freeplayTeamNumber = 1;

        [OneTimeSetUp]
        public void Setup()
        {
            IWebElement new_game_button;
            IWebElement freeplay_button;
            driver = new ChromeDriver("C:/Users/Brandon Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-QA-Testing");
            driver.Url = "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Title-Screen(Main%20Menu)/index.html";
            driver.Navigate();
            System.Threading.Thread.Sleep(5000);
            new_game_button = driver.FindElement(By.Id("new-game-button"));
            new_game_button.Click();
            freeplay_button = driver.FindElement(By.Id("freeplay-button"));
            freeplay_button.Click();

        }

        public bool CheckIfAlertExists()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
        }

        public int getRandomNumber(int low, int high)
        {
            Random rnd = new Random();
            return rnd.Next(low, high);
        }

        [Test]
        //This test is to confirm that when a player attempts to start a game wtih no teams that an error message shows up.
        public void ConfirmStartGameWarningNoTeams()
        {
            bool does_alert_exist = false;
            IWebElement startGameButton = driver.FindElement(By.Id("start-game-button"));
            startGameButton.Click();
            does_alert_exist = CheckIfAlertExists();
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
            does_alert_exist = CheckIfAlertExists();
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
            teamNameTextField.SendKeys("Test Team "+freeplayTeamNumber);
            okTeamCreationButton.Click();
            //Ship selection screen
            int faction = getRandomNumber(1,3);
            IWebElement factionClick = driver.FindElement()
         }
    }
  
}
