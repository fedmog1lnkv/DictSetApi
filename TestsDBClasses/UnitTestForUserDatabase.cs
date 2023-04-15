namespace Database;

[TestClass]
public class UnitTestRegister
{
    UsersDatabase db = new UsersDatabase();
    string? username = "fedm";
    string email = "fedm@yandex.ru";
    string password = "1234";

    [TestMethod]
    public void TestRegister()
    {
        db.DeleteUser(email);
        bool actual = db.Register(username, email, password);
        Assert.AreEqual(true, actual);
        db.DeleteUser(email);
    }

    [TestMethod]
    public void TestRegisterWithExistUser()
    {
        bool actual = db.Register(username, email, password);
        actual = db.Register(username, email, password);
        Assert.AreEqual(false, actual);
    }
}

[TestClass]
public class UnitTestDelete
{
    UsersDatabase db = new UsersDatabase();
    string? username = "fedm";
    string email = "fedm@yandex.ru";
    string password = "1234";

    [TestMethod]
    public void TestDelete()
    {
        db.Register(username, email, password);
        bool actual = db.DeleteUser(email);
        Assert.AreEqual(true, actual);
    }

    [TestMethod]
    public void TestDeleteWithNotExistUser()
    {
        bool actual = db.DeleteUser(email);
        Assert.AreEqual(false, actual);
    }
}


[TestClass]
public class UnitTestAuthorization
{
    UsersDatabase db = new UsersDatabase();
    string? username = "fedm";
    string email = "fedm@yandex.ru";
    string password = "1234";
    
    [TestMethod]
    public void TestAuthorization()
    {
        db.Register(username, email, password);
        bool actual = db.Authorization(email, password);
        Assert.AreEqual(true, actual);
        db.DeleteUser(email);
    }

    [TestMethod]
    public void TestAuthorizationWithNotExistUser()
    {
        bool actual = db.Authorization(email, password);
        Assert.AreEqual(false, actual);
    }
}