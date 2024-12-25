// See https://aka.ms/new-console-template for more information

var tasks = new List<Task<SemaphoreSingleton>>();
for (int i = 0; i < 10; i++)
{
    tasks.Add(new Task<SemaphoreSingleton>(() => SemaphoreSingleton.Instance));
    //tasks[i].Start();
    //Thread.Sleep(10);
}

Parallel.ForEach(tasks, t => t.Start());

var semaphoreSingletons = await Task.WhenAll(tasks);


//Console.WriteLine(string.Join(",", tasks.Select(x => x.Result.GetHashCode())));
Console.WriteLine(string.Join(",", semaphoreSingletons.Select(x => x.GetHashCode())));
public class DocumentStorage
{
    private Dictionary<Guid, Document> _documents;
    
}

public class Singleton
{
    private static object _singletonLock = new object();
    private static Singleton? _instance;
    public static Singleton Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            lock (_singletonLock)
            {
                _instance ??= new Singleton();
                return _instance;
            }
            
        }
    }
}
public class SemaphoreSingleton
{
    private static SemaphoreSlim dataLock = new SemaphoreSlim(1,1);
    private static SemaphoreSingleton? _instance;
    public static SemaphoreSingleton Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            dataLock.Wait();
            _instance ??= new SemaphoreSingleton();
            dataLock.Release();
            return _instance;
        }
    }
}
public class LazySingleton
{
    private static object _singletonLock = new object();
    private static Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton());
    public static LazySingleton Instance => _instance.Value;
}

public class Document
{
    public Guid Id { get; set; }
    public string Content { get; set; }
}