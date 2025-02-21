namespace JoseApiRest.Infrastructure.Services.EntityFramework;

public class Context
{
    private DataContext context;
    private static Context controll;

    public static Context GetInstance
    {
        get
        {
            if (controll == null)
                controll = new Context();

            return controll;
        }
    }

    public DataContext GetContext()
    {
        if (context == null)
            context = new DataContext();

        return context;
    }
}
