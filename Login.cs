using Microsoft.Playwright;

using Microsoft.Playwright.MSTest;
using PlaywrightTest.PageObjects;
using System.Text.RegularExpressions;

namespace PlaywrightTests

{

    [TestClass]

    public class LoginTest : PageTest

    {

        [TestMethod]

        public async Task SuccessfulTitle()

        {

            //Navigate to the login page

            await Page.GotoAsync("https://the-internet.herokuapp.com/login");

            //Enter username

            await Page.FillAsync("input#username", "tomsmith");

            //Enter password

            await Page.FillAsync("input#password", "SuperSecretPassword!");

            //Click login buton

            await Page.ClickAsync("button[type='submit']");

            //Verify successfull login message

            var flashMessage = Page.Locator("#flash");

            await Expect(flashMessage).ToContainTextAsync(new Regex("You logged into a secure area!"));

            //Verify url has changed to a secure area

            await Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/secure");



        }

        [TestMethod]

        public async Task GetStartedLink()

        {

            await Page.GotoAsync("https://playwright.dev");

            //Click the get started link

            await Page.GetByRole(AriaRole.Link, new() { Name = "Get Started" }).ClickAsync();

            //Expect the url to contain "intro"

            await Expect(Page).ToHaveURLAsync(new System.Text.RegularExpressions.Regex(".*intro"));


        }

        [TestMethod]
        public async Task SuccessfullLoginUsingPOM()
        {
            //Initialize new Page Object Model
            var loginpage = new LoginPage(Page);

            //Navigate to the Login Page

            await loginpage.NavigateAsync();

            //Perform Login

            await loginpage.LoginAsync("tomsmith", "SuperSecretPassword!");

            //Verify Successful Login

            await Expect(loginpage.FlashMessage).ToContainTextAsync("You logged into a secure area!");

            //
            await Expect(Page).ToHaveURLAsync("https://the-internet.herokuapp.com/secure");

        }

        [TestMethod]
        public async Task UnsuccessfulLoginUsingPOM()
        {
            var loginPage = new LoginPage(Page);
            await loginPage.NavigateAsync();
            await loginPage.LoginAsync("invalidUser", "invalidPass");
            await Expect(loginPage.FlashMessage).ToContainTextAsync("Your username is invalid!");

        }

    }

}