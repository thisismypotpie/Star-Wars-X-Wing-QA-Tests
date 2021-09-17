using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            }
            else
            {

            }
        }
        [Test]
        public void CreateSingleTeamShip()
        {
            
        }
    }
  
}
