namespace Database;

[TestClass]
public class UnitTestAddToken
{
    UsersDatabase userDB = new UsersDatabase();
    string username = "fedm";
    string email = "fedm@yandex.ru";
    string password = "1234";

    TokenDatabase tokenDB = new TokenDatabase();

    [TestMethod]
    public void TestAddToken()
    {
        userDB.Register(username, email, password);
        bool actual = tokenDB.AddToken(email, "test_token");
        Assert.AreEqual(true, actual);
        tokenDB.DeleteToken(email);
        userDB.DeleteUser(email);
    }

    [TestMethod]
    public void TestAddTokenWithExist()
    {
        userDB.Register(username, email, password);
        tokenDB.AddToken(email, "test_token");
        bool actual = tokenDB.AddToken(email, "test_token");
        Assert.AreEqual(false, actual);
        tokenDB.DeleteToken(email);
        userDB.DeleteUser(email);
    }
}

[TestClass]
public class UnitTestGetTokenByEmail
{
    UsersDatabase userDB = new UsersDatabase();
    string username = "fedm";
    string email = "fedm@yandex.ru";
    string password = "1234";

    TokenDatabase tokenDB = new TokenDatabase();

    [TestMethod]
    public void TestGetTokenByEmail()
    {
        userDB.Register(username, email, password);
        tokenDB.AddToken(email, "test_token");
        string actual = tokenDB.GetTokenByEmail(email);
        Assert.AreEqual("test_token", actual);
        tokenDB.DeleteToken(email);
        userDB.DeleteUser(email);
    }
}