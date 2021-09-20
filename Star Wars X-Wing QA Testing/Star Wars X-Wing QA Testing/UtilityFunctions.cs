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
    class UtilityFunctions
    {
        public static void freeplaySetup(ref IWebDriver driver)
        {
            IWebElement new_game_button;
            IWebElement freeplay_button;
            driver = new ChromeDriver("C:/Users/Brandon Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-QA-Testing");
            driver.Url = "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Title-Screen(Main%20Menu)/index.html";
            driver.Navigate();
            System.Threading.Thread.Sleep(3000);
            new_game_button = driver.FindElement(By.Id("new-game-button"));
            new_game_button.Click();
            freeplay_button = driver.FindElement(By.Id("freeplay-button"));
            freeplay_button.Click();
        }

        public static bool CheckIfAlertExists(ref IWebDriver driver)
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

        public static int getRandomNumber(int low, int high)
        {
            Random rnd = new Random();
            return rnd.Next(low, high);
        }

        public static void createFreeplayTeam(ref IWebDriver driver, ref int freeplayTeamNumber, ref List<int> rosterNumbersInUse)
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
            int numberOfUpgrades = UtilityFunctions.getRandomNumber(0, 12);
            for (int i = 0; i < numberOfUpgrades; i++)
            {
                driver.FindElement(By.Id("next-selection")).Click();

                //Upgrade type selection screen
                IList<IWebElement> upgrade_types = driver.FindElements(By.ClassName("type-clicker"));
                upgrade_types[UtilityFunctions.getRandomNumber(0, upgrade_types.Count)].Click();

                //Upgrade Options screen
                IList<IWebElement> upgrades = driver.FindElements(By.ClassName("upgrade"));
                upgrades[UtilityFunctions.getRandomNumber(0, upgrades.Count)].Click();
            }
            driver.FindElement(By.Id("done-button")).Click();
            int roster_number = UtilityFunctions.getRandomNumber(1, 999);
            while (rosterNumbersInUse.Contains(roster_number))
            {
                roster_number = UtilityFunctions.getRandomNumber(1, 999);
            }
            driver.FindElement(By.Id("roster-number-input")).SendKeys(roster_number.ToString());
            driver.FindElement(By.Id("ok-button")).Click();
            freeplayTeamNumber = driver.FindElements(By.ClassName("team-summary")).Count + 1;
        }

        public static void addShipToExistingTeam()
        {

        }

        public static bool assertPageValidation(string page, string url)
        {
            if(page == "Main Page" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Title-Screen(Main%20Menu)/index.html")
            {
                return true;
            }
            else if(page == "Team Page" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Team-Screen/Team-Screen.html")
            {
                return true;
            }
            else if(page == "New Team Ship Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Selection-Screen/Selection-Screen.html")
            {
                return true;
            }
            else if(page == "New Team Pilot Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Pilot-Screen/Pilot-Screen.html")
            {
                return true;
            }
            else if (page == "New Team Upgrade Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Upgrade-Screen/Upgrade-Screen.html")
            {
                return true;
            }
            else if (page == "New Team Upgrade Type Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Upgrade-Screen/Upgrade-Type-Selection-Screen/upgrade-type-selection-screen.html")
            {
                return true;
            }
            else if (page == "New Team Upgrade Options" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Upgrade-Screen/Upgrade-Selection-Screen/Upgrade-Selection-Screen.html")
            {
                return true;
            }
            else if (page == "Add Ship Ship Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Add-New-Ship-Screens/Selection-Screen/New-Ship-Selection-Screen.html")
            {
                return true;
            }
            else if (page == "Add Ship Pilot Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Add-New-Ship-Screens/Pilot-Screen/New-Ship-Pilot-Screen.html")
            {
                return true;
            }
            else if (page == "Add Ship Upgrade Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Add-New-Ship-Screens/Upgrade-Screen/New-Ship-Upgrade-Screen.html")
            {
                return true;
            }
            else if (page == "Add Ship Upgrade Type Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Add-New-Ship-Screens/Upgrade-Screen/Upgrade-Type-Selection-Screen/upgrade-type-selection-screen.html")
            {
                return true;
            }
            else if (page == "Add Ship Upgrade Otions Selection" && url == "file:///C:/Users/Brandon%20Danielski/Documents/FullStackWebClass/Final-Project/Final-Project-Front-End/Front-End-Files/Add-New-Ship-Screens/Upgrade-Screen/Upgrade-Selection-Screen/Upgrade-Selection-Screen.html")
            {
                return true;
            }
            return false;
        }
    }
}
