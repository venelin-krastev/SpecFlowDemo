# Interview Prep — SpecFlowDemo

Q&A a senior QA interviewer would ask about this project.

---

**Q: Why `[BeforeScenario]` instead of `[BeforeFeature]` for driver setup?**
A: Each scenario needs a fresh, isolated browser instance. `[BeforeFeature]` runs once for the entire feature file — a single shared driver across all scenarios causes state bleed (cookies, session, URL carrying over). `[BeforeScenario]`/`[AfterScenario]` create and destroy a driver per scenario, mirroring NUnit's `[SetUp]`/`[TearDown]` pattern.

**Q: Why does every step definition class need `[Binding]`?**
A: SpecFlow scans the assembly at runtime for classes marked `[Binding]` to discover step definitions. Without it, `[Given]`/`[When]`/`[Then]` methods are invisible to the SpecFlow runner — it reports "No matching step definition" even though the method exists and compiles fine.

**Q: How does a `Scenario Outline` turn into multiple test cases?**
A: Each row in the `Examples` table produces its own, independently reported test. In `Login.feature`, one `Scenario Outline` with two rows (`locked_out_user` / `standard_user` + wrong password) becomes two separate test results in the runner — one step definition method handles both, with `<placeholder>` values substituted per row.

**Q: Why is `Given user is logged in` reused across scenarios instead of repeated in every feature?**
A: Login is a precondition for the Products scenarios, not the thing under test there. Extracting it as a shared step keeps each `Products.feature` scenario focused on what it's actually verifying (product listing, add-to-cart), and means a change to the login flow only requires updating one step definition, not every feature file that logs in first.

**Q: What's the actual difference between this and a plain NUnit Selenium test?**
A: The mechanics underneath are identical — same `IWebDriver`, same Page Objects, same `WebDriverWait`. The difference is the layer on top: Gherkin scenarios are readable by non-technical stakeholders (PO, business analyst) and describe behavior in plain language, while the C# binding classes are where the actual Selenium code lives. BDD's value is that shared vocabulary between test and requirement, not a different testing mechanism.
