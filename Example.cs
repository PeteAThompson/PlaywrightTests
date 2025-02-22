using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests
{
    [TestClass]
    public class ExampleTest : PageTest
    {
        [TestMethod]
        public async Task HasTitle()
        {
            await Page.GotoAsync("https://playwright.dev");

            //Expect a title to contain a substring

            await Expect(Page).ToHaveTitleAsync(new System.Text.RegularExpressions.Regex("Playwright"));

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
    }
}