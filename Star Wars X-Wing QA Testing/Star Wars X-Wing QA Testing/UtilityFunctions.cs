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
    }
}
